namespace Api.Models
{
    public class BaseEnvelopeModel<T>
    {
        public string Version { get; protected set; } = null!;
        public string Type { get; protected set; } = null!;

        /// <summary>
        /// Message content. This may be null, if it makes sense in context.
        /// </summary>
        public T? Content { get; set; } = default!;
    }
}
