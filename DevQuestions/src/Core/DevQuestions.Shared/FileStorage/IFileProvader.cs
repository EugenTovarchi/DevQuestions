namespace Shared.FileStorage;

public interface IFileProvader
{
    //Task<string> UploadAsync(Stream stream, string key, string bucket);

    /// <summary>
    /// Этот метод для получения единственного URL
    /// </summary>
    /// <param name="fileId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<string> GetUrlByIdAsync (Guid fileId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Метод для обворачивания свойства ScreenshotId в Dictionary [key:guid, value: url ]
    /// </summary>
    /// <param name="fileIds">Принимаем список Guid</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Получаем Dictionary </returns>
    public Task <Dictionary<Guid, string>> GetUrlByIdAsync (IEnumerable<Guid> fileIds, CancellationToken cancellationToken = default);

}
