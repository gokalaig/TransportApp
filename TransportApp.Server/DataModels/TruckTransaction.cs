namespace TransportApp.Server.DataModels
{
    public class TruckTransaction
    {
        

        public DateTime LoadDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int LcNo { get; set; }
        public string Transport { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string DriverCell { get; set; }

        public List<TruckDetails> TruckDetailsList { get; set; } = new List<TruckDetails>();
        public LoadDetails LoadDetails { get; set; }
        public TransportDetails TransportDetails { get; set; }
        public TruckBalance TruckBalance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
