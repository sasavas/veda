namespace Veda.Application.SharedKernel.DomainServices.HtmlHelper;

public interface IHtmlService
{
    IHtmlBuilder GetHtmlBuilder();
}


public interface IHtmlBuilder
{
    IHtmlBuilder AddTitle(string title);
    IHtmlBuilder AddParagraph(string content);
    IHtmlBuilder AddImage(string imgUrl);
    IHtmlBuilder AddLink(string url);

    string Build();
}