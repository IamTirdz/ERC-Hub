namespace ERC.Hub.Business.Common.Utilities
{
    public class SystemTime : ISystemTime
    {
        public DateTime Now() => DateTime.UtcNow;
    }
}
