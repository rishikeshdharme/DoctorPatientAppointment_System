using DoctorPatientAppointment_System.DataBaseConnection;
using DoctorPatientAppointment_System.Model;
using Microsoft.EntityFrameworkCore;

namespace DoctorPatientAppointment_System.Repository
{
    public class DoctorRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DoctorRepository(ApplicationDbContext db)
        {
            this.dbContext = db;
        }

        #region Code of adding doctor by Doctor object
        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
           var doc = dbContext.Doctors.AddAsync(doctor);
            int result = await dbContext.SaveChangesAsync();
            if (result == 0)
            {
                return null;
            }
            else
            {
                return doctor;
            }

        }
        
        #endregion

        #region Update Doctor by doctorid and Doctor object
        public async Task<int> UpdateDoctor(Doctor doctor, int doctorid)
        {
           var doctorcopy =  dbContext.Doctors.Include(d=>d.Appointments).FirstOrDefault(d => d.DoctorId == doctorid);
            if(doctorcopy != null) { 
                
                doctorcopy.DoctorName = doctor.DoctorName;
                doctorcopy.DoctorSpecialization = doctor.DoctorSpecialization;
                doctorcopy.Email = doctor.Email;
                dbContext.Doctors.Update(doctorcopy);
                return await dbContext.SaveChangesAsync();

            }
            else
            {
                return 0;
            }
        }
        #endregion


        #region  Code of delete Doctor by doctorid
        public async Task<bool> DeleteDoctor(int doctorid)
        {
          var doctorcopy =   dbContext.Doctors.Include(d=>d.Appointments).FirstOrDefaultAsync(d => d.DoctorId == doctorid);
            if(doctorcopy != null)
            {
                return true;
            }
            else
            return false;
        }
        
        #endregion


        #region Code of fetching all doctors
        public async Task<List<Doctor>> GetAllDoctors()
        {
            return await dbContext.Doctors.Include(d => d.Appointments).ToListAsync();
        }
        #endregion

        #region Code of fetching doctor by doctorid
        public async Task<Doctor?> GetDoctorById(int doctorId)
        {
            return await dbContext.Doctors.Include(d => d.Appointments).FirstOrDefaultAsync(d=> d.DoctorId ==doctorId);
        }
        #endregion

        #region Code of Check Existing Email
        public async Task<bool> IsEmailExist(string email)
        {
            return await dbContext.Doctors.Include(d=> d.Appointments).AnyAsync(d => d.Email == email);
        }

        #endregion


        


    }
}

