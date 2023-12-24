using Serilog;

namespace Veda.Application.SharedKernel.Services.Email;

public class EmailService(ILogger logger) : IEmailService
{
    public void SendEmail(EmailDTO emailDto)
    {
        logger.Information("Just sent an email to {toAddress}", emailDto.To.Address);
    }
}