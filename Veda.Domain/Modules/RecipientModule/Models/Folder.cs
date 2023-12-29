using Veda.Application.Abstract;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.Modules.RecipientModule.Models;

/// <summary>
/// Her alici icin tanimlanan depolama alani. Sanal olabilir.
/// Yani fiziksel bir isletim sistemi klasorune karsilik gelmek zorunda degil. 
/// </summary>
public class Folder : Entity
{
    public int RecipientId { get; private set; }

    private readonly ICollection<DigitalContent> _digitalContents = new List<DigitalContent>();
    public virtual IReadOnlyCollection<DigitalContent> DigitalContents => new List<DigitalContent>(_digitalContents);
    
    public string FolderName { get; set; }
    public double SizeOccupied { get; private set; }

    protected Folder()
    {
    }

    public static Folder Create(string name)
    {
        return new Folder
        {
            FolderName = name,
        };
    }

    public void AddContent(DigitalContent content)
    {
        _digitalContents.Add(content);
    }
    
    public void RemoveContent(DigitalContent content, DateTime deletionTime)
    {
        if (!_digitalContents.Contains(content))
        {
            throw new DomainException("This folder does not contain any such content");
        }
        
        content.MarkAsDeleted(deletionTime);
    }
}