using DoctorPatientAppointment_System.Model;
using DoctorPatientAppointment_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorPatientAppointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService patientService;

        public PatientController(PatientService pservice)
        {
            patientService = pservice;
        }

        [HttpPost("addpatient")]
        public async Task<IActionResult> AddPatient([FromBody] Patient patient)
        {
            var exists = await patientService.CheckISExistingEmail(patient.Email);
            if (exists)
            {
                return BadRequest("Email already exists. Please use a different email.");
            }
            else
            {
                var patient01 = await patientService.AddPatient(patient);
                return Ok(patient01);
            }
        }

        [HttpPut("updatepatient/{pid}")]
        public async Task<String> UpdatePatient([FromBody]Patient patient, int pid)
        {
            var result = await patientService.UpdatePatient(patient, pid);
            if (result)
            {
                return "Patient updated successfully.";
            }
            else
            {
                return "Failed to update patient.";
            }
        }

        [HttpDelete("deletepatient/{pid}")]
        public async Task<String> DeletePatient(int pid)
        {
            var result = await patientService.DeletePatientByid(pid);
            if (result)
            {
                return "Patient deleted successfully.";
            }
            else
            {
                return "Failed to delete patient.";
            }
        }


        [HttpGet("getpatientbyname/{patientname}")]
        public async Task<IActionResult> GetPatientByName(String patientname)
        {
            var result = await patientService.GetPatientByName(patientname);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("Patient not found.");
            }
        }

        [HttpGet("getpatientbyid/{patientid}")]
        public async Task<IActionResult> GetPatientByid(int patientid)
        {
            var result = await patientService.GetPatientById(patientid);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("Patient not found.");
            }
        }

        [HttpGet("getallpatient")]
        public async Task<IActionResult> GetAllPatients()
        {
            var result = await patientService.GetAllPatients();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No patients found.");
            }


        }
    }
}


