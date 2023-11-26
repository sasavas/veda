namespace Veda.Application.SharedKernel.Services.Email;

public interface IEmailService
{
    void SendEmail(EmailDTO emailDto);
}