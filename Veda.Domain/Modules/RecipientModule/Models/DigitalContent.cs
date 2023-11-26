using Veda.Application.Abstract;

namespace Veda.Application.Modules.RecipientModule.Models;

/// <summary>
/// Herhangi bir resim, muzik, video, text, yazilim, patent dosyasi olabilir.
/// </summary>
public class DigitalContent : Entity
{
    public string Name { get; set; }
    /// <summary>
    /// Dosyanin diskte kapladigi alan
    /// </summary>
    public double Size { get; set; }
    /// <summary>
    /// Kaydedilirken Hash'i alinacak
    /// </summary>
    public string HashCode { get; set; }
}