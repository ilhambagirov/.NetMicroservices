using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.DataContexts;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo repo;
        private readonly IMapper mapper;
        private readonly ICommandDataClient httpDataClient;
        public int a;
        public PlatformsController(IPlatformRepo repo, IMapper mapper, ICommandDataClient httpDataClient)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.httpDataClient = httpDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> Get()
        {
            var platforms = repo.GetAllPlatform();
            return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlatformReadDto>> GetId(int id)
        {
            var platform = await repo.GetPlatformById(id);
            if (platform != null)
            {
                return Ok(mapper.Map<PlatformReadDto>(platform));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> Post([FromBody] PlatformCreateDto platformcreate)
        {
            var platform = mapper.Map<Platform>(platformcreate);

            repo.CreatePlatform(platform);

            var platformReadDto = mapper.Map<PlatformReadDto>(platform);

            try
            {
                await httpDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"--> Could not Send Synchronously {ex}");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
