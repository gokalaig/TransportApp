using TransportApp.Server.Dtos;

namespace TransportApp.Server.Repository
{
    public interface ICommissionBillService
    {
        Task<CommissionBillDto> GetLoadAsync(string lcNo);
    }


}
