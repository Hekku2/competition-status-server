namespace Api.Models
{
    public class BaseEnvelopeModel<T>
    {
        public string Version { get; protected set; }
        public string Type { get; protected set; }
        public T Content { get; set; }
    }
}
