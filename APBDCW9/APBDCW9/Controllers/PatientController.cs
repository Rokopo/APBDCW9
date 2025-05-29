using APBDCW9.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBDCW9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        readonly IPatientService _patientService;
    

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }


        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPatient(int patientId)
        {
            var res = await _patientService.GetPatient(patientId);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
