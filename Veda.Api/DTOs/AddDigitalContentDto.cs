namespace Veda.Api.DTOs;

public record AddDigitalContentDto(int recipientId, string targetFileName, IFormFile file, string fileExtension);