namespace Veda.Application.SharedKernel.DomainServices.Email;

public interface IEmailService
{
    void SendEmail(EmailDTO emailDto);
}