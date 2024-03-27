using Newtonsoft.Json;

namespace Acklann.Plaid.Institution
{
    public class GetOptions
    {
        [JsonProperty("include_optional_metadata")]
        public bool IncludeOptionalMetadata { get; set; }
    }
}