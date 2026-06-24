using DoctorPatientAppointment_System.DataBaseConnection;
using DoctorPatientAppointment_System.Enums;
using DoctorPatientAppointment_System.Model;
using DoctorPatientAppointment_System.Services;
using Microsoft.EntityFrameworkCore;

namespace DoctorPatientAppointment_System.Repository
{
    public class AppointmentRepo
    {

        private readonly ApplicationDbContext dbContext;
        private readonly TelegramMessageSharingObject telegramMessageSharingObject;

        public AppointmentRepo(ApplicationDbContext db, TelegramMessageSharingObject telegramMessageSharingObject)
        {
            this.dbContext = db;
            this.telegramMessageSharingObject = telegramMessageSharingObject;
        }


        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            var app = await dbContext.Appointments.AddAsync(appointment);
            int result = await dbContext.SaveChangesAsync();

              Appointment appointmentsaved =  await GetAppointmentById(appointment.Id);
            if(appointmentsaved?.Patient?.TelegramChatId!=null)
            {
                telegramMessageSharingObject.SendAppointmentCreatedAsync
                    (
                    appointmentsaved.Patient.TelegramChatId,
                    appointmentsaved.Patient.PatientName,
                    appointmentsaved.Doctor?.DoctorName ?? "Unknown Doctor",
                    appointmentsaved.AppointmentDate


                    );
            }
            return appointmentsaved!;


        }

        public async Task<Appointment?> UpdateAppointment(Appointment appointment, int id)
        {
            var existing = dbContext.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefault(a => a.Id == id);

            if (existing == null) 
                return null;

            var previousStatus = existing.Status;

            existing.AppointmentDate = appointment.AppointmentDate;
            existing.Status = appointment.Status;
            existing.RejectionReason = appointment.RejectionReason;
            dbContext.SaveChanges();

            // Send Telegram notification only when status changes
            if (previousStatus != appointment.Status && existing.Patient?.TelegramChatId != null)
            {
                long chatId = existing.Patient.TelegramChatId;

                if (appointment.Status == AppointmentStatus.Approved)
                {
                    _ = telegramMessageSharingObject.SendAppointmentApproved(
                        chatId,
                        existing.Patient.PatientName,
                        existing.Doctor?.DoctorName ?? "Doctor",
                        existing.AppointmentDate
                    );
                }
                else if (appointment.Status == AppointmentStatus.Rejected)
                {
                    _ = telegramMessageSharingObject.SendAppointmentRejectedAsync(
                        chatId,
                        existing.Patient.PatientName,
                        existing.Doctor?.DoctorName ?? "Doctor",
                        existing.RejectionReason ?? ""
                    );
                }
            }

            return existing;
        }

        public async Task<String> DeleteAppointment(int id)
        {
            var result = 0;
            var appointment = dbContext.Appointments.Find(id);
            if (appointment != null)
            {
                dbContext.Appointments.Remove(appointment);
                result = await dbContext.SaveChangesAsync();
            }

            if (result == 0)
            {
                return "Appointment is not delete";
            }
            else
            {
                return "Appointment is deleted successfully";
            }
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {

           return  await dbContext.Appointments
                                 .Include(x => x.Doctor)
                                 .Include(y => y.Patient)
                                 .FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await dbContext.Appointments.Include(x => x.Doctor)
                  .Include(y => y.Patient)
                  .ToListAsync();
        }



        public async Task<List<Appointment>> GetAppointmentsByPatientId(int patientId)
        {
            return await dbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.PatientId == patientId)
                .ToListAsync();

        }

        public async Task<List<Appointment>> GetAppointmentsByDoctorId(int doctorId)
        {
            return await dbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByStatus(AppointmentStatus status)
        {
            return await dbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.Status == status)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByDateRange(DateTime startDate, DateTime endDate)
        {
            return await dbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.AppointmentDate >= startDate && a.AppointmentDate <= endDate)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctorAndStatus(String Doctorname, AppointmentStatus status)
        {
            return await dbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.Doctor.DoctorName == Doctorname && a.Status == status)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByPatientAndStatus(String Patientname, AppointmentStatus status)
        {
            return await dbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.Patient.PatientName == Patientname && a.Status == status)
                .ToListAsync();
        }
    }
}
