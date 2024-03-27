using Newtonsoft.Json;

namespace Acklann.Plaid.Institution
{
    /// <summary>
    /// Represents a request for plaid's '/institutions/get' endpoints. The '/institutions/get' endpoint to retrieve a complete list of supported institutions.
    /// </summary>
    /// <seealso cref="Acklann.Plaid.SerializableContent" />
    public class GetRequest : SerializableContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRequest"/> class.
        /// </summary>
        public GetRequest()
        {
            NullValueHandling = NullValueHandling.Include;
        }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>The offset.</value>
        [JsonProperty("offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        [JsonProperty("secret")]
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the client_id.
        /// </summary>
        /// <value>The client identifier.</value>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public GetOptions Options { get; set; }
    }
}