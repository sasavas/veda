namespace Veda.Application.SharedKernel.Models;

public class EmailAddress
{
    public EmailAddress(string address)
    {
        //TODO validate
        Address = address;
    }

    public string Address { get; set; }
}