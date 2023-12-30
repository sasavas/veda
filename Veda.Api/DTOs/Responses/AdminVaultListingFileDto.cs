namespace Veda.Api.DTOs.Responses;

public record AdminVaultListingFileDto(string FileExtension, string ContentNameExcerpt, long Size, DateTime UploadDate);