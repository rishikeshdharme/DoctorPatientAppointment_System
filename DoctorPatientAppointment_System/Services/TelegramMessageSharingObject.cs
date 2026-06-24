

using Telegram.Bot;

namespace DoctorPatientAppointment_System.Services
{
    public class TelegramMessageSharingObject
    {

        private readonly TelegramBotClient telegramBotClient;
        private readonly ILogger<TelegramMessageSharingObject> logger;

        public TelegramMessageSharingObject(IConfiguration configuration,TelegramBotClient telegramBotClient, ILogger<TelegramMessageSharingObject> logger)
        {
            var token = configuration["Telegram:BotToken"];
            this.telegramBotClient = new TelegramBotClient(token!);
            this.logger = logger;
        }

        public async Task SendMessageAsync(long chatId, string msg)
        {
            try
            {
                await telegramBotClient.SendMessage(

                    chatId: chatId,
                    text: msg,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html

                    );

                logger.LogInformation($"Telegram message sent to chatId: {chatId}");

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to send Telegram message: {ex.Message}");

            }
        }

        public async Task SendAppointmentApproved(long chatID, string patientName, string doctorName, DateTime appointmentDate)
        {
            var message = $"✅ <b>Appointment Approved!</b>\n\n" +
                          $"👤 <b>Patient:</b> {patientName}\n" +
                          $"🩺 <b>Doctor:</b> {doctorName}\n" +
                          $"📅 <b>Date:</b> {appointmentDate:dddd, MMMM dd yyyy}\n" +
                          $"🕐 <b>Time:</b> {appointmentDate:hh:mm tt}\n\n" +
                          $"Please arrive 10 minutes early. To do not miss the appointment🏥"+
                          $"<i>By In any Case you miss the Appointment time So, In this case you will get chance at last</i>";

            await SendMessageAsync(chatID, message);

        }

        public async Task SendAppointmentRejectedAsync(long chatId, string patientName, string doctorName, string reason = "")
        {
            var message = $"❌ <b>Appointment Rejected</b>\n\n" +
                          $"👤 <b>Patient:</b> {patientName}\n" +
                          $"🩺 <b>Doctor:</b> {doctorName}\n" +
                          (string.IsNullOrEmpty(reason) ? "" : $"📝 <b>Reason:</b> {reason}\n") +
                          $"\nSorry to inconvience,Due to High Appointment Count we are unable accept your appointment.\n In case Emergency call this no. 8989898666";

            await SendMessageAsync(chatId, message);
        }


        public async Task SendAppointmentCreatedAsync(long chatId, string patientName, string doctorName, DateTime appointmentDate)
        {
            var message = $"📋 <b>Appointment Booked!</b>\n\n" +
                          $"👤 <b>Patient:</b> {patientName}\n" +
                          $"🩺 <b>Doctor:</b> {doctorName}\n" +
                          $"📅 <b>Date:</b> {appointmentDate:dddd, MMMM dd yyyy}\n" +
                          $"🕐 <b>Time:</b> {appointmentDate:hh:mm tt}\n\n" +
                          $"⏳ Status: <b>Pending Approval</b>";

            await SendMessageAsync(chatId, message);
        }


    }
}
