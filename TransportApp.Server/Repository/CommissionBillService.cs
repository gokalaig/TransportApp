using TransportApp.Server.Dtos;

namespace TransportApp.Server.Repository
{
    public class CommissionBillService : ICommissionBillService
    {
        private readonly ICommissionBillRepository _commissionBillRepository;

        public CommissionBillService(ICommissionBillRepository commissionBillRepository)
        {
            _commissionBillRepository = commissionBillRepository;
        }

        public async Task<CommissionBillDto> GetLoadAsync(string lcNo)
        {
            return await _commissionBillRepository.GetLoadAsync(lcNo);
        }
    }



}
