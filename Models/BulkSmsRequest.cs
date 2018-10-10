using System.Runtime.Serialization;

namespace SampleWebAPI.Models
{
    [DataContract]
    public class BulkSmsRequest
    {
        [DataMember(Name = "to", EmitDefaultValue = false)]
        public string To { get; set; }

        [DataMember(Name = "body", EmitDefaultValue = false)]
        public string Body { get; set; }
    }
}