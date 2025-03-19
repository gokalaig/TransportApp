using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TransportApp.Server.DataModels
{
    public class TruckMaster
    {
        [Key]  // Define Truck_No as the Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Truck_No { get; set; }
        public string? NameBoard { get; set; }
        public string? Mobile { get; set; }
        public string? Mobile2 { get; set; }
        public string? Mobile3 { get; set; }
        public string? PAN { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Type { get; set; }
        public string? Parent_Truck { get; set; }
        public string? Parent_NameBoard { get; set; }
        public DateTime Created_on { get; set; }
        public string Created_by { get; set; }
    }
}
