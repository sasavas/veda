namespace Veda.SharedKernel.Services.HtmlHelper;

public interface IHtmlBuilder
{
    IHtmlBuilder AddTitle(string title);
    IHtmlBuilder AddParagraph(string content);
    IHtmlBuilder AddImage(string imgUrl);
    IHtmlBuilder AddLink(string url);

    string Build();
}