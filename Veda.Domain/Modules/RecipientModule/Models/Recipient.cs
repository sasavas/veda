using Veda.Application.Abstract;
using Veda.Application.SharedKernel.Models;

namespace Veda.Application.Modules.RecipientModule.Models;

public class Recipient : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public TCKimlikNo TCKimlikNo { get; set; }
    public EmailAddress EMailAddress { get; set; }
    public PhoneNumber Type { get; set; }

    public List<Folder> Folders { get; set; }
}