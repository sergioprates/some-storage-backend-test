namespace StorageService.Events
{
    public class EmailReadEvent
    {
        public string Referrer { get; set; }
        public string UserAgent { get; set; }
        public string? IPAddress { get; set; }
        public DateTime VisitDateTime { get; set; } = DateTime.UtcNow;
    }
}
