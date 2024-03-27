using Newtonsoft.Json;

namespace Acklann.Plaid.Institution
{
    /// <summary>
    /// Represents a request for plaid's '/institutions/get' endpoints. The '/institutions/te' endpoint to retrieve a complete list of supported institutions.
    /// </summary>
    /// <seealso cref="Acklann.Plaid.ResponseBase" />
    public class GetResponse : ResponseBase
    {
        /// <summary>
        /// Gets or sets the institutions.
        /// </summary>
        /// <value>The institutions.</value>
        [JsonProperty("institutions")]
        public Entity.Institution[] Institutions { get; set; }
    }
}