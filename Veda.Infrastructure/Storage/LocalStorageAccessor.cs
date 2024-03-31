using Veda.Application.Ports.Storage;

namespace Veda.Infrastructure.Storage;

public class LocalStorageAccessor : IStorageAccessor
{
    public string BasePath { get; }

    public LocalStorageAccessor(string basePath)
    {
        BasePath = basePath;

        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }
    }

    public void UploadFile(Stream sourceStream, string fileName)
    {
        var filePath = GetFullPath(fileName);

        if (sourceStream.CanSeek)
        {
            sourceStream.Seek(0, SeekOrigin.Begin);
        }

        using FileStream fileStream = File.Create(filePath);
        sourceStream.CopyTo(fileStream);
    }

    public FileStream DownloadFile(string fileName)
    {
        var filePath = GetFullPath(fileName);
        
        if (!File.Exists(GetFullPath(filePath)))
        {
            throw new FileNotFoundException("The file does not exist.", filePath);
        }
        
        return new FileStream(filePath, FileMode.Open, FileAccess.Read);
    }

    public void DeleteFile(string fileName)
    {
        var filePath = GetFullPath(fileName);

        if (!File.Exists(fileName))
        {
            return;
        }
        
        File.Delete(filePath);
    }

    public void DeleteFolder(string folderPath)
    {
        var fullFolderPath = GetFullPath(folderPath);

        if (!Directory.Exists(fullFolderPath))
        {
            return;
        }
        
        Directory.Delete(fullFolderPath);
    }

    private string GetFullPath(string path)
    {
        return Path.Combine(BasePath, path);
    }
}