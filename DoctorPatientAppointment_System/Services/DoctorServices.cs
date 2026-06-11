using DoctorPatientAppointment_System.DTO_Response_;
using DoctorPatientAppointment_System.Model;
using DoctorPatientAppointment_System.Repository;

namespace DoctorPatientAppointment_System.Services
{
    public class DoctorServices
    {
        private readonly DoctorRepository doctorRepository;

        public DoctorServices(DoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }


        public async Task<Doctor_DataResponse> AddDoctor(Doctor doctor)
        {
           var doctorEntity = await doctorRepository.AddDoctor(doctor);
            return new Doctor_DataResponse
            {
                DoctorId = doctorEntity.DoctorId,
                DoctorName = doctorEntity.DoctorName,
                DoctorSpecialization = doctorEntity.DoctorSpecialization,
                Email = doctorEntity.Email
            };
        }

        public async Task<bool> UpdateDoctor(Doctor doctor, int doctorid)
        {
            int result = await doctorRepository.UpdateDoctor(doctor, doctorid);
            if(result == 0)
            return false;
            else return true;
        }

        public async Task<bool> DeleteDoctor(int doctorid)
        {
            return await doctorRepository.DeleteDoctor(doctorid);

        }
         
        public async Task<List<Doctor_DataResponse>> GetAllDoctor()
        {
            var doctorList = await doctorRepository.GetAllDoctors();
            return doctorList.Select(doctor => new Doctor_DataResponse
            {
                DoctorId = doctor.DoctorId,
                DoctorName = doctor.DoctorName,
                DoctorSpecialization = doctor.DoctorSpecialization,
                Email = doctor.Email
            }).ToList();
        }

        public async Task<Doctor_DataResponse> GetDoctorById(int doctorid)
        {
            var doctor = await doctorRepository.GetDoctorById(doctorid);
            if (doctor == null)
            {
                return null;
            }
            return new Doctor_DataResponse
            {
                DoctorId = doctor.DoctorId,
                DoctorName = doctor.DoctorName,
                DoctorSpecialization = doctor.DoctorSpecialization,
                Email = doctor.Email
            };
        }

        public async Task<bool> CheckISExistingEmail(string email)
        {
            return await doctorRepository.IsEmailExist(email);
        }


}
}
