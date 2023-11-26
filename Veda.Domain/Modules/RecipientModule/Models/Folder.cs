using Veda.Application.Abstract;

namespace Veda.Application.Modules.RecipientModule.Models;

/// <summary>
/// Her alici icin tanimlanan depolama alani. Sanal olabilir. Yani dosyalari gruplamak icin kullanilabilir sadece. 
/// </summary>
public class Folder : Entity
{
    public List<DigitalContent> DigitalContents { get; set; }
    public string FolderName { get; set; }
    public double SizeOccupied { get; set; }
}