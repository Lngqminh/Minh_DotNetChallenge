namespace DotNetTraining.Common.Services.Email
{
    public interface IEmailLogService
    {
        Task SendReminderEmailAsync();
    }
}
