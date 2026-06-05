using DoctorPatientAppointment_System.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoctorPatientAppointment_System.Model
{
    public class Appointment
    {

        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }

        public Patient? Patient { get; set; }

        public int DoctorId { get; set; }

        public Doctor? Doctor { get; set; }

        public DateTime AppointmentDate { get; set; }


        public string? RejectionReason { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    }
}
