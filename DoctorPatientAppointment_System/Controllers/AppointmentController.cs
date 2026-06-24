using DoctorPatientAppointment_System.Enums;
using DoctorPatientAppointment_System.Model;
using DoctorPatientAppointment_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorPatientAppointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

       [HttpPost("addappointment")]
        public async Task<IActionResult> AddAppointment([FromBody] Appointment appointment)
        {
            var result = await appointmentService.AddAppointment(appointment);
            
            if(result == null)
            {
                return BadRequest("Failed to add appointment.");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPut("updateappointment/{id}")]
        public async Task<IActionResult> UpdateAppointment([FromBody] Appointment appointment, int id)
        {
            try
            {
                var result = await appointmentService.UpdateAppointment(appointment, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteappointment/{appointmentId}")]
        public async Task<String> DeleteAppointment(int appointmentId)
        {
            return await appointmentService.DeleteAppointment(appointmentId);
            
        }


        [HttpGet("getallappointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var result = await appointmentService.GetAllAppointments();
            if (result == null || !result.Any())
            {
                return NotFound("No appointments found.");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("getappointmentbyid/{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            var result = await appointmentService.GetAppointmentById(appointmentId);
            if (result == null)
            {
                return NotFound($"No appointment found with ID {appointmentId}.");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("getappointmentsbypatientid/{patientId}")]
        public async Task<IActionResult> GetAppointmentsByPatientId(int patientId)
        {
            var result = await appointmentService.GetAppointmentsByPatientId(patientId);
            if (result == null || !result.Any())
            {
                return NotFound($"No appointments found for patient ID {patientId}.");
            }
            else
            {
                return Ok(result);
            }
        }


        [HttpGet("getappointmentsbydoctorid/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsByDoctorId(int doctorId)
        {
            var result = await appointmentService.GetAppointmentsByDoctorId(doctorId);
            if (result == null || !result.Any())
            {
                return NotFound($"No appointments found for doctor ID {doctorId}.");
            }
            else
            {
                return Ok(result);
            }
        }


        [HttpGet("getappointmentsbystatus/{status}")]
        public async Task<IActionResult> GetAppointmentsByStatus(AppointmentStatus status)
        {
            var result = await appointmentService.GetAppointmentsByStatus(status);
            if (result == null || !result.Any())
            {
                return NotFound($"No appointments found with status {status}.");
            }
            else
            {
                return Ok(result);
            }
        }


        [HttpGet("getappointmentsbydaterange/{startDate}/{endDate}")]
        public async Task<IActionResult> GetAppointmentsByDateRange(DateTime startDate, DateTime endDate)
        {
            var result = await appointmentService.GetAppointmentsByDateRange(startDate, endDate);
            if (result == null || !result.Any())
            {
                return NotFound($"No appointments found between {startDate} and {endDate}.");
            }
            else
            {
                return Ok(result);
            }
        }


        [HttpGet("getappointmentsbydoctorandstatus/{doctorName}/{status}")]
        public async Task<IActionResult> GetAppointmentsByDoctorAndStatus(String doctorName, AppointmentStatus status)
        {
            var result = await appointmentService.GetAppointmentsByDoctorAndStatus(doctorName, status);
            if (result == null || !result.Any())
            {
                return NotFound($"No appointments found for doctor  {doctorName} with status {status}.");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("getappointmentsbypatientandstatus/{patientName}/{status}")]
        public async Task<IActionResult> GetAppointmentsByPatientAndStatus(String patientName, AppointmentStatus status)
        {
            var result = await appointmentService.GetAppointmentsByPatientAndStatus(patientName, status);
            if (result == null || !result.Any())
            {
                return NotFound($"No appointments found for patient {patientName} with status {status}.");
            }
            else
            {
                return Ok(result);
            }
        }

    }
}
