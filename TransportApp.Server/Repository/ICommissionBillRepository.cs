using TransportApp.Server.Dtos;

namespace TransportApp.Server.Repository
{
    public interface ICommissionBillRepository
    {
        Task<CommissionBillDto> GetLoadAsync(string lcNo);
    }

}
