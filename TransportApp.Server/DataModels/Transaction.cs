namespace TransportApp.Server.DataModels
{
    public class Transaction
    {
        public long TransactionNo { get; set; }
        public DateTime Date { get; set; }
        public string Ledger { get; set; }
        public string Particulars { get; set; }
        public decimal Payment { get; set; }
        public decimal Receipt { get; set; }
    }
}
