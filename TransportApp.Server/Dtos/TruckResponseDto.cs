namespace TransportApp.Server.Dtos
{
    public class TruckResponseDto
    {
        public string truck_No { get; set; }
        public string nameBoard { get; set; }
        public string mobile { get; set; }
        public string mobile2 { get; set; }
        public string mobile3 { get; set; }
        public string pAN { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string type { get; set; }
        public string parent_Truck { get; set; }
        public DateTime Created_on { get; set; }
        public string created_by { get; set; }
        public string parent_NameBoard { get; set; }
    }

}
