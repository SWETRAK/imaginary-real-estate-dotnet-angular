namespace ImaginaryRealEstate.Services.Interfaces;

public interface IMinioService
{ 
    Task InsertFile(string objectName, string contentType, MemoryStream fileStream);

    Task<(MemoryStream, string)> GetFile(string objectName);
    
    Task RemoveFile(string objectName);
}