using Refit;

namespace HangFireApplication.Services;

public interface IMailServiceApi
{
    //NotificationServerApi Url  http://localhost:5020
    [Post("/api/emails")]
    Task SendEmailAsync([Body] EmailBodyDto emailRequest);
}
