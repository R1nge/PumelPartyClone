using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Mirror.Hosting.Edgegap.Models.SDK
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class MatchmakerReleaseUpdateBase
    {
        /// <summary>
        /// Name of the matchmaker release. Should be unique, and will be used to differentiate your releases.
        /// </summary>
        /// <value>Name of the matchmaker release. Should be unique, and will be used to differentiate your releases.</value>
        [DataMember(Name = "version", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class MatchmakerReleaseUpdateBase {\n");
            sb.Append("  Version: ").Append(Version).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}