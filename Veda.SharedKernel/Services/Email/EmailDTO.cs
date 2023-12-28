namespace Veda.SharedKernel.Services.Email;

public abstract class EmailDTO
{
    protected EmailDTO(EmailAddress to, string title)
    {
        To = to;
        Title = title;
    }

    public EmailAddress To { get; set; }
    public string Title { get; set; }
    
    public abstract string Body { get; }
}

public class HtmlEmailDTO : EmailDTO
{
    public override string Body { get; }

    public HtmlEmailDTO(EmailAddress to, string title, string content) : base(to, title)
    {
        Body = content;
    }
}