using Api.Clinic.Enterprise;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        // Might be better to have PatientEC as a private readonly here instead of making a new EC everytime

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PatientDTO> Get()
        {
            return new PatientEC().Patients;
        }

        [HttpGet("{id}")]
        public PatientDTO? GetById(int id)
        {
            return new PatientEC().GetById(id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            new PatientEC().Delete(id);
        }

        [HttpPost("Search")]
        public List<PatientDTO> Search([FromBody] Query q)
        {
            return new PatientEC().Search(q?.Content ?? string.Empty)?.ToList() ?? new List<PatientDTO>();
        }

        [HttpPost]
        public PatientDTO? AddOrUpdate([FromBody] PatientDTO? patient)
        {
            return new PatientEC().AddOrUpdate(patient);
        }
    }
}
