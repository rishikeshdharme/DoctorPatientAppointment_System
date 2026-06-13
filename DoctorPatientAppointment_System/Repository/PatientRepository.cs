using DoctorPatientAppointment_System.DataBaseConnection;
using DoctorPatientAppointment_System.Model;
using Microsoft.EntityFrameworkCore;

namespace DoctorPatientAppointment_System.Repository
{
    public class PatientRepository
    {

        private readonly ApplicationDbContext dbContext;

        public PatientRepository(ApplicationDbContext db)
        {
            this.dbContext = db;
        }

        public async Task<bool> CheckISExistingEmail(string email)
        {
            var patient = await dbContext.Patients.Include(p=>p.Appointments).FirstOrDefaultAsync(p => p.Email == email);
            if (patient != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Patient> AddPatient(Patient patient)
        {
            var pat = dbContext.Patients.AddAsync(patient);
            int result = await dbContext.SaveChangesAsync();
            if (result == 0)
            {
                return null;
            }
            else
            {
                return patient;
            }
        }

        public async Task<int> UpdatePatient(Patient patient, int patientid)
        {
            var patientcopy = dbContext.Patients.Include(p => p.Appointments).FirstOrDefault(p => p.PatientId == patientid);
            if (patientcopy != null)
            {
                patientcopy.PatientName = patient.PatientName;
                patientcopy.PatientPhone = patient.PatientPhone;
                patientcopy.Email = patient.Email;
                patientcopy.PasswordHash = patient.PasswordHash;
                dbContext.Patients.Update(patientcopy);
                return await dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<bool> DeletePatient(int patientid)
        {
            var patientcopy = await dbContext.Patients.Include(p => p.Appointments).FirstOrDefaultAsync(p => p.PatientId == patientid);
            if (patientcopy != null)
            {
                dbContext.Patients.Remove(patientcopy);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;


        }

        public async Task<List<Patient>> GetAllPatients()
        {
            return await dbContext.Patients.Include(p => p.Appointments).ToListAsync();
        }

        public async Task<Patient?> GetPatientById(int patientid)
        {
            return await dbContext.Patients.Include(p => p.Appointments).FirstOrDefaultAsync(p => p.PatientId == patientid);
        }

        public async Task<Patient?> GetPatientByName(string patientname)
        {
            return await dbContext.Patients.Include(p => p.Appointments).FirstOrDefaultAsync(p => p.PatientName == patientname);
        }
}
}
