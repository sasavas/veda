using System.Text;
using Microsoft.Extensions.Logging;
using Veda.SharedKernel.Services.HtmlHelper;

namespace Veda.Infrastructure.ServiceImplementations;

public class HtmlService(ILogger<HtmlService> logger) : IHtmlService
{
    public IHtmlBuilder GetHtmlBuilder()
    {
        logger.LogDebug("GetHtmlBuilder is called");
        return new HtmlBuilder();
    }
}

public class HtmlBuilder : IHtmlBuilder
{
    private readonly StringBuilder builder = new();
    
    public IHtmlBuilder AddTitle(string title)
    {
        builder.Append($"<h1>{title}</h1>");
        return this;
    }

    public IHtmlBuilder AddParagraph(string content)
    {
        builder.Append($"<p>{content}</p>");
        return this;
    }

    public IHtmlBuilder AddImage(string imgUrl)
    {
        throw new NotImplementedException();
    }

    public IHtmlBuilder AddLink(string url)
    {
        builder.Append($"""<img href="{url}" />""");
        return this;
    }

    public string Build()
    {
        return builder.ToString();
    }
}