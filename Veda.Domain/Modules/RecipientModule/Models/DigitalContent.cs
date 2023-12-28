using Veda.Application.Abstract;

namespace Veda.Application.Modules.RecipientModule.Models;

/// <summary>
/// Herhangi bir resim, muzik, video, text, yazilim, patent dosyasi olabilir.
/// </summary>
public class DigitalContent : Entity
{
    protected DigitalContent(){}
    
    public string Name { get; set; }
    /// <summary>
    /// Dosyanin diskte kapladigi alan
    /// </summary>
    public long SizeInBytes { get; set; }
    /// <summary>
    /// Kaydedilirken Hash'i alinacak
    /// </summary>
    public string HashCode { get; set; }

    public static DigitalContent Create(string name, long sizeInBytes, string fileHashCode)
    {
        return new DigitalContent
        {
            Name = name,
            SizeInBytes = sizeInBytes,
            HashCode = fileHashCode
        };
    }
}