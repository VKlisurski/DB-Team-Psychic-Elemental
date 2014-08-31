namespace CarDealersSystem.Reporters.ReportModels
{
    using System;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    public class JsonReport
    {
        [JsonProperty]
        public int CarId { get; set; }

        [JsonProperty]
        public string CarModer { get; set; }

        [JsonProperty]
        public string CarMake { get; set; }

        [JsonProperty]
        public decimal PricePerUnit { get; set; }

        [JsonProperty]
        public int Quantity { get; set; }

        [JsonProperty]
        public decimal Sum { get; set; }
    }
}
