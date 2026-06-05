namespace DoctorPatientAppointment_System.DTO_Response_
{
    public class Appointment_DataResponse
    {
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }

        public string? PatientName { get; set; }

        public int DoctorId { get; set; }

        public String? DoctorName { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string? RejectionReason { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
