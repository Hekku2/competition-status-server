using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    /// <summary>
    /// Base class for all SignalR and other messages
    /// </summary>
    /// <typeparam name="T">Content type</typeparam>
    public abstract class BaseEnvelopeModel<T>
    {
        /// <summary>
        /// Envelope version number. Version can be discarded if no
        /// functionality is specified for given version
        /// </summary>
        [Required]
        public string Version { get; protected set; } = null!;

        /// <summary>
        /// Type of the message. This and version can be used to identify
        /// correct parser for this message.
        /// </summary>
        [Required]
        public string Type { get; protected set; } = null!;

        /// <summary>
        /// Message content. This may be null, if it makes sense in context.
        /// </summary>
        public T? Content { get; set; } = default!;
    }
}
