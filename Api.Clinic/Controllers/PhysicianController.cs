using Api.Clinic.Enterprise;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhysicianController : ControllerBase
    {
        private readonly ILogger<PhysicianController> _logger;

        public PhysicianController(ILogger<PhysicianController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PhysicianDTO> Get()
        {
            return new PhysicianEC().Physicians;
        }

        [HttpGet("{id}")]
        public PhysicianDTO? GetById(int id)
        {
            return new PhysicianEC().GetById(id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            new PhysicianEC().Delete(id);
        }

        //[HttpPost("Search")]
        //public List<PhysicianDTO> Search([FromBody] QueryClass q)
        //{
        //    return new PhysicianEC().Search(q?.Content ?? string.Empty)?.ToList() ?? new List<PhysicianDTO>();
        //}

        [HttpPost]
        public PhysicianDTO? AddOrUpdate([FromBody] PhysicianDTO? physician)
        {
            return new PhysicianEC().AddOrUpdate(physician);
        }
    }
}
