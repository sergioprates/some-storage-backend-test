using Microsoft.Extensions.Logging;
using StorageService.Events;

namespace StorageService
{
    public class FileStorage
    {
        private readonly string filePath;
        private readonly ILogger<FileStorage> logger;
        static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public FileStorage(string filePath, ILogger<FileStorage> logger)
        {
            this.filePath = filePath;
            this.logger = logger;
        }

        public async Task SaveAsync(EmailReadEvent @event, CancellationToken cancellationToken)
        {
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                using (var writer = File.AppendText(this.filePath))
                    await writer.WriteLineAsync(@event.ToString());

                this.logger.LogInformation("Saved data to storage.");
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Something went wrong.");
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
