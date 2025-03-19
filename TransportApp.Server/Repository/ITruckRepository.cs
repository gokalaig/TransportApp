using TransportApp.Server.DataModels;

namespace TransportApp.Server.Repository
{
    public interface ITruckRepository
    {
        Task<CustomResponse<TruckMaster>> GetTruckByNumberAsync(string truckNo, string authToken);
        Task<CustomResponse<string>> AddTruckAsync(string truckNo, TruckMaster truck);
        Task<CustomResponse<string>> UpdateTruckAsync(string truckNo, TruckMaster truck);
    }

}
