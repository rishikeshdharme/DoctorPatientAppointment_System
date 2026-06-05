namespace DoctorPatientAppointment_System.DTO_Requests_
{
    public class Patient_RegistrationRequest
    {
        public string PatientName { get; set; } = string.Empty;
        public string PatientPhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
