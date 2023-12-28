namespace Veda.SharedKernel.Services.Email;

public interface IEmailService
{
    void SendEmail(EmailDTO emailDto);
}