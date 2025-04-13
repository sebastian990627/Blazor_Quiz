using Radzen;

namespace Blazor_Quiz.Services
{

    public class NotifyMinService
    {
        private readonly NotificationService _notificationService;

        public NotifyMinService(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void Error(string message)
        {
            _notificationService.Notify(new()
            {
                Summary = "Błąd",
                Detail = message,
                Duration = 10000,
                Severity = NotificationSeverity.Error
            });
        }

        public void Warning(string message)
        {
            _notificationService.Notify(new()
            {
                Summary = message,
                Duration = 10000,
                Severity = NotificationSeverity.Warning
            });
        }

        public void Success()
        {
            _notificationService.Notify(new()
            {
                Summary = "Zmiany zapisane",
                Duration = 5000,
                Severity = NotificationSeverity.Success
            });
        }
        public void Success(string message)
        {
            _notificationService.Notify(new()
            {
                Summary = "Zmiany zapisane",
                Detail = message,
                Duration = 5000,
                Severity = NotificationSeverity.Success
            });
        }
        public void Success(string message, string summary)
        {
            _notificationService.Notify(new()
            {
                Summary = summary,
                Detail = message,
                Duration = 5000,
                Severity = NotificationSeverity.Success
            });
        }
    }
}
