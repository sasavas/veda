using Veda.Application.Abstract;

namespace Veda.Application.Modules.RecipientModule.Models;

/// <summary>
/// Her alici icin tanimlanan depolama alani. Sanal olabilir.
/// Yani fiziksel bir isletim sistemi klasorune karsilik gelmek zorunda degil. 
/// </summary>
public class Folder : Entity
{
    public int RecipientId { get; private set; }
    public List<DigitalContent> DigitalContents { get; set; } = new();
    public string FolderName { get; set; }
    public double SizeOccupied { get; private set; }
    
    private Folder(){}

    public static Folder Create(string name)
    {
        return new Folder
        {
            FolderName = name,
        };
    }
    
    public void AddContent(DigitalContent content)
    {
        DigitalContents.Add(content);
    }
}