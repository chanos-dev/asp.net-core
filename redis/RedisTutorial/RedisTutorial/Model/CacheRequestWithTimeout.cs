namespace RedisTutorial.Model
{
    public class CacheRequestWithTimeout
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int Timeout { get; set; }
    }
}
