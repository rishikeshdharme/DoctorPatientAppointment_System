using System.ComponentModel.DataAnnotations;

namespace DoctorPatientAppointment_System.Model
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public long TelegramChatId { get; set; }
        public string? PatientName { get; set; } = null;
        public string? PatientPhone { get; set; }

        // ← NEW: Email used for login
        public string Email { get; set; } = string.Empty;

        // ← NEW: Hashed password (never plain text)
        public string PasswordHash { get; set; } = string.Empty;

    }
}
