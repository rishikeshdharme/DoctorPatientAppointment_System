using DoctorPatientAppointment_System.DTO_Response_;
using DoctorPatientAppointment_System.Enums;
using DoctorPatientAppointment_System.Model;
using DoctorPatientAppointment_System.Repository;

namespace DoctorPatientAppointment_System.Services
{
    public class AppointmentService
    {
        private readonly AppointmentRepo appointmentRepository;
        public AppointmentService(AppointmentRepo appointmentRepo) { 
        this.appointmentRepository = appointmentRepo;
        }

        public async Task<Appointment_DataResponse> AddAppointment(Appointment appointment)
        {
            Appointment appoint = await appointmentRepository.AddAppointment(appointment);
            Appointment_DataResponse dataResponse = new Appointment_DataResponse()
            {
                AppointmentId = appoint.Id,
                PatientId = appoint.PatientId,
                PatientName = appoint.Patient?.PatientName,
                DoctorId = appoint.DoctorId,
                DoctorName = appoint.Doctor?.DoctorName,
                AppointmentDate = appoint.AppointmentDate,
                RejectionReason = appoint.RejectionReason,
                Status = appoint.Status.ToString()
            };

            return dataResponse;
        }


        public async Task<Appointment> UpdateAppointment(Appointment appointment, int id)
        {
            var updatedAppointment = await appointmentRepository.UpdateAppointment(appointment, id);
            if (updatedAppointment == null)
            {
                throw new Exception("Appointment not found.");
            }
            Appointment_DataResponse dataResponse = new Appointment_DataResponse()
            {
                AppointmentId = updatedAppointment.Id,
                PatientId = updatedAppointment.PatientId,
                PatientName = updatedAppointment.Patient?.PatientName,
                DoctorId = updatedAppointment.DoctorId,
                DoctorName = updatedAppointment.Doctor?.DoctorName,
                AppointmentDate = updatedAppointment.AppointmentDate,
                RejectionReason = updatedAppointment.RejectionReason,
                Status = updatedAppointment.Status.ToString()
            };
            return updatedAppointment;
        }

        public Task<String> DeleteAppointment(int appointmentId)
        {
            return appointmentRepository.DeleteAppointment(appointmentId);
        }

        public async Task<List<Appointment_DataResponse>> GetAllAppointments()
        {
            List<Appointment> lappointment = await  appointmentRepository.GetAllAppointments();
            List<Appointment_DataResponse> lappointmentDataResponse = new List<Appointment_DataResponse>();


            for(int i = 0; i < lappointment.Count; i++)
            {
                lappointmentDataResponse.Add(new Appointment_DataResponse()
                {
                    AppointmentId = lappointment[i].Id,
                    PatientId = lappointment[i].PatientId,
                    PatientName = lappointment[i].Patient?.PatientName,
                    DoctorId = lappointment[i].DoctorId,
                    DoctorName = lappointment[i].Doctor?.DoctorName,
                    AppointmentDate = lappointment[i].AppointmentDate,
                    RejectionReason = lappointment[i].RejectionReason,
                    Status = lappointment[i].Status.ToString()
                });
            }

            return lappointmentDataResponse;
           }


        public async Task<Appointment_DataResponse?> GetAppointmentById(int appointmentId)
        {
            var appointment = await appointmentRepository.GetAppointmentById(appointmentId);
            if (appointment == null)
            {
                return null;
            }
            Appointment_DataResponse dataResponse = new Appointment_DataResponse()
            {
                AppointmentId = appointment.Id,
                PatientId = appointment.PatientId,
                PatientName = appointment.Patient?.PatientName,
                DoctorId = appointment.DoctorId,
                DoctorName = appointment.Doctor?.DoctorName,
                AppointmentDate = appointment.AppointmentDate,
                RejectionReason = appointment.RejectionReason,
                Status = appointment.Status.ToString()
            };
            return dataResponse;
        }


        public async Task<List<Appointment_DataResponse>> GetAppointmentsByPatientId(int patientId)
        {
            List<Appointment> lappointment = await appointmentRepository.GetAppointmentsByPatientId(patientId);
            List<Appointment_DataResponse> lappointmentDataResponse = new List<Appointment_DataResponse>();



            for (int i = 0; i < lappointment.Count; i++)
            {
                lappointmentDataResponse.Add(new Appointment_DataResponse()
                {
                    AppointmentId = lappointment[i].Id,
                    PatientId = lappointment[i].PatientId,
                    PatientName = lappointment[i].Patient?.PatientName,
                    DoctorId = lappointment[i].DoctorId,
                    DoctorName = lappointment[i].Doctor?.DoctorName,
                    AppointmentDate = lappointment[i].AppointmentDate,
                    RejectionReason = lappointment[i].RejectionReason,
                    Status = lappointment[i].Status.ToString()
                });
            }

            return lappointmentDataResponse;
        }


        public async Task<List<Appointment_DataResponse>> GetAppointmentsByDoctorId(int doctorId)
        {
            List<Appointment> lappointment = await appointmentRepository.GetAppointmentsByDoctorId(doctorId);
            List<Appointment_DataResponse> lappointmentDataResponse = new List<Appointment_DataResponse>();
            for (int i = 0; i < lappointment.Count; i++)
            {
                lappointmentDataResponse.Add(new Appointment_DataResponse()
                {
                    AppointmentId = lappointment[i].Id,
                    PatientId = lappointment[i].PatientId,
                    PatientName = lappointment[i].Patient?.PatientName,
                    DoctorId = lappointment[i].DoctorId,
                    DoctorName = lappointment[i].Doctor?.DoctorName,
                    AppointmentDate = lappointment[i].AppointmentDate,
                    RejectionReason = lappointment[i].RejectionReason,
                    Status = lappointment[i].Status.ToString()
                });
            }

            return lappointmentDataResponse;


        }

        public async Task<List<Appointment_DataResponse>> GetAppointmentsByStatus(AppointmentStatus status)
        {
            List<Appointment> lappointment = await appointmentRepository.GetAppointmentsByStatus(status);
            List<Appointment_DataResponse> lappointmentDataResponse = new List<Appointment_DataResponse>();
            for (int i = 0; i < lappointment.Count; i++)
            {
                lappointmentDataResponse.Add(new Appointment_DataResponse()
                {
                    AppointmentId = lappointment[i].Id,
                    PatientId = lappointment[i].PatientId,
                    PatientName = lappointment[i].Patient?.PatientName,
                    DoctorId = lappointment[i].DoctorId,
                    DoctorName = lappointment[i].Doctor?.DoctorName,
                    AppointmentDate = lappointment[i].AppointmentDate,
                    RejectionReason = lappointment[i].RejectionReason,
                    Status = lappointment[i].Status.ToString()
                });
            }
            return lappointmentDataResponse;
        }

        public async Task<List<Appointment_DataResponse>> GetAppointmentsByDateRange(DateTime startDate, DateTime endDate)
        {
            List<Appointment> lappointment = await appointmentRepository.GetAppointmentsByDateRange(startDate, endDate);
            List<Appointment_DataResponse> lappointmentDataResponse = new List<Appointment_DataResponse>();
            for (int i = 0; i < lappointment.Count; i++)
            {
                lappointmentDataResponse.Add(new Appointment_DataResponse()
                {
                    AppointmentId = lappointment[i].Id,
                    PatientId = lappointment[i].PatientId,
                    PatientName = lappointment[i].Patient?.PatientName,
                    DoctorId = lappointment[i].DoctorId,
                    DoctorName = lappointment[i].Doctor?.DoctorName,
                    AppointmentDate = lappointment[i].AppointmentDate,
                    RejectionReason = lappointment[i].RejectionReason,
                    Status = lappointment[i].Status.ToString()
                });
            }
            return lappointmentDataResponse;
        }

        public async Task<List<Appointment_DataResponse>> GetAppointmentsByDoctorAndStatus(String Doctorname, AppointmentStatus status)
        {
            List<Appointment> lappointment = await appointmentRepository.GetAppointmentsByDoctorAndStatus(Doctorname, status);
            List<Appointment_DataResponse> lappointmentDataResponse = new List<Appointment_DataResponse>();
            for (int i = 0; i < lappointment.Count; i++)
            {
                lappointmentDataResponse.Add(new Appointment_DataResponse()
                {
                    AppointmentId = lappointment[i].Id,
                    PatientId = lappointment[i].PatientId,
                    PatientName = lappointment[i].Patient?.PatientName,
                    DoctorId = lappointment[i].DoctorId,
                    DoctorName = lappointment[i].Doctor?.DoctorName,
                    AppointmentDate = lappointment[i].AppointmentDate,
                    RejectionReason = lappointment[i].RejectionReason,
                    Status = lappointment[i].Status.ToString()
                });
            }
            return lappointmentDataResponse;
        }


        public async Task<List<Appointment_DataResponse>> GetAppointmentsByPatientAndStatus(String Patientname, AppointmentStatus status)
        {
            List<Appointment> lappointment = await appointmentRepository.GetAppointmentsByPatientAndStatus(Patientname, status);
            List<Appointment_DataResponse> lappointmentDataResponse = new List<Appointment_DataResponse>();
            for (int i = 0; i < lappointment.Count; i++)
            {
                lappointmentDataResponse.Add(new Appointment_DataResponse()
                {
                    AppointmentId = lappointment[i].Id,
                    PatientId = lappointment[i].PatientId,
                    PatientName = lappointment[i].Patient?.PatientName,
                    DoctorId = lappointment[i].DoctorId,
                    DoctorName = lappointment[i].Doctor?.DoctorName,
                    AppointmentDate = lappointment[i].AppointmentDate,
                    RejectionReason = lappointment[i].RejectionReason,
                    Status = lappointment[i].Status.ToString()
                });
            }
            return lappointmentDataResponse;
        }
    }
}

