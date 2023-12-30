using Veda.Application.Abstract;

namespace Veda.Application.Modules.RecipientModule.Models;

/// <summary>
/// Herhangi bir resim, muzik, video, text, yazilim, patent dosyasi olabilir.
/// </summary>
public class DigitalContent : Entity
{
    protected DigitalContent(){}
    public string Name { get; private set; }
    public string FileExtension { get; private set; }
    /// <summary>
    /// Dosyanin diskte kapladigi alan
    /// </summary>
    public long SizeInBytes { get; private set; }
    /// <summary>
    /// Kaydedilirken Hash'i alinacak
    /// </summary>
    public string HashCode { get; private set; }
    public DateTime UploadDate { get; private set; }
    public DateTime? DeletionDate { get; private set; }

    public static DigitalContent Create(string name, long sizeInBytes, string fileHashCode, DateTime uploadDate)
    {
        return new DigitalContent
        {
            Name = name,
            SizeInBytes = sizeInBytes,
            HashCode = fileHashCode,
            UploadDate = uploadDate,
        };
    }

    public void MarkAsDeleted(DateTime deletionDate)
    {
        DeletionDate = deletionDate;
    }
}