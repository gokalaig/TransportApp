namespace TransportApp.Server.Dtos
{
    public class CommissionBillDto
    {
        public string LcNo { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime AppliedDate { get; set; }
        public string TruckNo { get; set; }
        public int TransportId { get; set; }
        public string ToLocation { get; set; }
        public string FromLocation { get; set; }
        public decimal TotalHire { get; set; }
        public decimal OverLoad { get; set; }
        public decimal TotalRent { get; set; }
        public decimal Commission { get; set; }
        public decimal Thapal { get; set; }
        public decimal StaffMamul { get; set; }
        public decimal Hamali { get; set; }
        public decimal TruckTotal { get; set; }
        public decimal Advance { get; set; }
        public decimal Balance { get; set; }
        public decimal TransportClosingBalance { get; set; }
        public decimal TruckClosingBalance { get; set; }
        public decimal TDS { get; set; }
        public decimal ToPay { get; set; }
        public decimal Others { get; set; }
        public decimal Expense { get; set; }
        public decimal ExpenseBalance { get; set; }
        public decimal TransportHire { get; set; }
        public decimal TotalTruckHire { get; set; }
        public decimal TransportBalance { get; set; }
    }


}
