using System;
using System.Runtime.Serialization;

namespace Palmmedia.WpfGraph.UI.IO
{   
    /// <summary>
    /// Occurs when graph serialization fails.
    /// </summary>
    [Serializable] 
    public class GraphSerializationException : Exception
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GraphSerializationException"/> class.
		/// </summary>
        public GraphSerializationException()
            : base()
        {
        }

		/// <summary>
        /// Initializes a new instance of the <see cref="GraphSerializationException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
        public GraphSerializationException(string message)
            : base(message)
        {
        }

		/// <summary>
        /// Initializes a new instance of the <see cref="GraphSerializationException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="innerException">The inner exception.</param>
        public GraphSerializationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

		/// <summary>
        /// Initializes a new instance of the <see cref="GraphSerializationException"/> class.
		/// </summary>
		/// <param name="serializationInfo">The serialization info.</param>
		/// <param name="streamingContext">The streaming context.</param>
        protected GraphSerializationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
