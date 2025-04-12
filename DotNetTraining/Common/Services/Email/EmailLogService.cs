using Common.Loggers.Interfaces;

namespace DotNetTraining.Common.Services.Email
{
    public class EmailLogService : IEmailLogService
    {
        private readonly ILogManager _logger;

        public EmailLogService(ILogManager logger)
        {
            _logger = logger;
        }

        public async Task SendReminderEmailAsync()
        {
            // Nếu chưa có cấu hình SMTP thì chỉ mô phỏng gửi mail
            _logger.Info("[EmailLogService] Sending reminder email...");

            await Task.Delay(1000); // giả lập thời gian gửi

            _logger.Info("[EmailLogService] Reminder email sent successfully!");
        }
    }
}
