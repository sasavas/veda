using Microsoft.Extensions.Logging;
using Veda.Application.SharedKernel.Services.Email;
using Veda.SharedKernel.Services.Email;

namespace Veda.Infrastructure.ServiceImplementations;

public class EmailService(ILogger<EmailService> logger) : IEmailService
{
    public void SendEmail(EmailDTO emailDto)
    {
        logger.LogInformation("Just sent an email to {toAddress}", emailDto.To.Address);
    }
}