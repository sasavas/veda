using Microsoft.Extensions.Logging;
using Veda.SharedKernel.Services.Email;

namespace Veda.Infrastructure.ServiceImplementations;

public class EmailService(ILogger<EmailService> logger) : IEmailService
{
    public void SendEmail(EmailDTO emailDto)
    {
        logger.LogInformation("Just sent an email ({title}) to {toAddress} with content: {content}"
            , emailDto.Title
            , emailDto.To.Address
            , emailDto.Body);
    }
}