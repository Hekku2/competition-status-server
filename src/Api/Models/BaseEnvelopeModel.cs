namespace Api.Models
{
    public class BaseEnvelopeModel<T>
    {
        public string Version { get; protected set; } = null!;
        public string Type { get; protected set; } = null!;
        public T Content { get; set; } = default!;
    }
}
