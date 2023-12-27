using Veda.SharedKernel.Services.Email;

namespace Veda.Application.SharedKernel.Services.Email;

public abstract class EmailDTO
{
    protected EmailDTO(EmailAddress to, string title)
    {
        To = to;
        Title = title;
    }

    public EmailAddress To { get; set; }
    public string Title { get; set; }
}

public class HtmlEmailDTO : EmailDTO
{
    public string Content { get; set; }

    public HtmlEmailDTO(EmailAddress to, string title, string content) : base(to, title)
    {
        Content = content;
    }
}