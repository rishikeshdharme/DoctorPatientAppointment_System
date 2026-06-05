namespace DoctorPatientAppointment_System.DTO_Requests_
{
    public class Doctor_RegistrationRequest
    {
        public string DoctorName { get; set; } = string.Empty;
        public string DoctorSpecialization { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
