using System.Runtime.Serialization;

namespace SampleWebAPI.Models
{
    [DataContract]
    public class BulkSmsResponse
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "from", EmitDefaultValue = false)]
        public string Sender { get; set; }

        [DataMember(Name = "to", EmitDefaultValue = false)]
        public string Receiver { get; set; }

        [DataMember(Name = "body", EmitDefaultValue = false)]
        public string MessageBody { get; set; }

        [DataMember(Name = "encoding", EmitDefaultValue = false)]
        public string Encoding { get; set; }

        [DataMember(Name = "protocolId", EmitDefaultValue = false)]
        public int ProtocolId { get; set; }

        public Submission Submission { get; set; }

        public Status Status { get; set; }

        [DataMember(Name = "relatedSentMessageId", EmitDefaultValue = false)]
        public string RelatedSentMessageId { get; set; }

        [DataMember(Name = "userSuppliedId", EmitDefaultValue = false)]
        public string UserSuppliedId { get; set; }

        [DataMember(Name = "numberOfParts", EmitDefaultValue = false)]
        public string NumberOfParts { get; set; }

        [DataMember(Name = "creditCost", EmitDefaultValue = false)]
        public string CreditCost { get; set; }
    }

    [DataContract]
    public class Submission
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        [DataMember(Name = "date", EmitDefaultValue = false)]
        public string Date { get; set; }
    }

    [DataContract]
    public class Status
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "subtype", EmitDefaultValue = false)]
        public string SubType { get; set; }
    }
}