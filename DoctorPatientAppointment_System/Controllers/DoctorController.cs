using DoctorPatientAppointment_System.DTO_Response_;
using DoctorPatientAppointment_System.Model;
using DoctorPatientAppointment_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorPatientAppointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorServices doctorServices;

        public DoctorController(DoctorServices doctorServices)
        {
            this.doctorServices = doctorServices;
        }

        [HttpPost("adddoctor")]
        public async Task<IActionResult> AddDoctor(Doctor doctor)
        {
           if( await doctorServices.CheckISExistingEmail(doctor.Email) == true)
            {
                return BadRequest("Email already exists. Please use a different email.");
            }
            else
            {
                Doctor_DataResponse doct = await doctorServices.AddDoctor(doctor);
                if (doct != null)
                {
                    return Ok(doct);
                }
                else
                {
                    return BadRequest("Error Occured while Adding the Doctor");
                }
              
            }
        }

        [HttpPut("updatedoctor/{doctorid}")]
        public async Task<IActionResult> UpdateDoctor(Doctor doctor, int doctorid)
        {
           
                bool result = await doctorServices.UpdateDoctor(doctor, doctorid);
                if (result == true)
                {
                    return Ok("Doctor Updated Successfully");
                }
                else
                {
                    return BadRequest("Error Occured while Updating the Doctor");
                }
            
        }

        [HttpDelete("deletedoctor/{doctorid}")]
        public async Task<IActionResult> DeleteDoctor(int doctorid)
        {
            bool result = await doctorServices.DeleteDoctor(doctorid);
            if (result == true)
            {
                return Ok("Doctor Deleted Successfully");
            }
            else
            {
                return BadRequest("Error Occured while Deleting the Doctor");
            }


        }

        [HttpGet("getalldoctors")]
        public async Task<IActionResult> GetAllDoctor()
        {
            List<Doctor_DataResponse> doctorList = await doctorServices.GetAllDoctor();
            return Ok(doctorList);
        }

        [HttpGet("getdoctorbyid/{doctorid}")]
        public async Task<IActionResult> GetDoctorById(int doctorid)
        {
            Doctor_DataResponse doctor = await doctorServices.GetDoctorById(doctorid);
            if (doctor != null)
            {
                return Ok(doctor);
            }
            else
            {
                return NotFound("Doctor not found with the provided ID.");
            }

        }
}
}

