using Veda.Application.Modules.RecipientModule.Models;

namespace Veda.Api.DTOs.Responses;

public record AdminVaultListingFileDto(string FileExtension, string ContentNameExcerpt, long Size, DateTime UploadDate)
{
    public static AdminVaultListingFileDto FromDigitalContent(DigitalContent content)
    {
        return new AdminVaultListingFileDto(content.FileExtension,
                                            //TODO show some part of the file name to the admin/operator
                                            content.Name?[..2] ?? string.Empty,
                                            content.SizeInBytes,
                                            content.UploadDate);
    }
}