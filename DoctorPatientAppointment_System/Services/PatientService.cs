using DoctorPatientAppointment_System.DTO_Response_;
using DoctorPatientAppointment_System.Model;
using DoctorPatientAppointment_System.Repository;

namespace DoctorPatientAppointment_System.Services
{
    public class PatientService
    {

        private readonly PatientRepository patientRepository;
        public PatientService(PatientRepository prepository)
        { 
         patientRepository = prepository;
        }

        public async Task<Patient_DataResponse?> AddPatient(Patient patient)
        {
           Patient patientcopy = await patientRepository.AddPatient(patient);
            if (patientcopy != null)
            {
                Patient_DataResponse patient_DataResponse = new Patient_DataResponse
                {
                    PatientName = patientcopy.PatientName,
                    PatientPhone = patientcopy.PatientPhone,
                    Email = patientcopy.Email
                };
                return patient_DataResponse;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdatePatient(Patient patient,int pid)
        {
            var result = await patientRepository.UpdatePatient(patient,pid);
            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckISExistingEmail(string email)
        {
            var result = await patientRepository.CheckISExistingEmail(email);
            return result;
        }

        public async Task<Patient_DataResponse?> GetPatientByName(String patientname)
        {
            var patientcopy = await patientRepository.GetPatientByName(patientname);
            if (patientcopy != null)
            {
                Patient_DataResponse patient_DataResponse = new Patient_DataResponse
                {
                    PatientName = patientcopy.PatientName,
                    PatientPhone = patientcopy.PatientPhone,
                    Email = patientcopy.Email
                };
                return patient_DataResponse;
            }
            else
            {
                return null;
            }
        }

        public async  Task<Patient_DataResponse?> GetPatientById(int patientid)
        {
            var patientcopy = await patientRepository.GetPatientById(patientid);
            if (patientcopy != null)
            {
                Patient_DataResponse patient_DataResponse = new Patient_DataResponse
                {
                    PatientName = patientcopy.PatientName,
                    PatientPhone = patientcopy.PatientPhone,
                    Email = patientcopy.Email
                };
                return patient_DataResponse;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeletePatientByid(int patientid)
        {
         return await patientRepository.DeletePatient(patientid);
        }

        public async Task<List<Patient_DataResponse>> GetAllPatients()
        {
            var patients = await patientRepository.GetAllPatients();
            List<Patient_DataResponse> patientResponses = new List<Patient_DataResponse>();
            foreach (var patient in patients)
            {
                Patient_DataResponse patient_DataResponse = new Patient_DataResponse
                {
                    PatientName = patient.PatientName,
                    PatientPhone = patient.PatientPhone,
                    Email = patient.Email
                };
                patientResponses.Add(patient_DataResponse);
            }
            return patientResponses;

        }


        public async Task<bool> UpdateTelegramChatId(int patientId, long chatId)
        {
            return await patientRepository.UpdateTelegramChatId(patientId, chatId);
        }


    }
}

