namespace Veda.Api.DTOs;

public record AddDigitalContentDto(int recipientId, string fileName, IFormFile file);