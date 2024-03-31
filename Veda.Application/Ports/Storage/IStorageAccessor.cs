namespace Veda.Application.Ports.Storage;

public interface IStorageAccessor
{
    string BasePath { get; }
    
    void UploadFile(Stream sourceStream, string fileName);

    FileStream DownloadFile(string fileName);

    void DeleteFile(string fileName);

    void DeleteFolder(string folderPath);
}

