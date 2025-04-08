using Microsoft.Data.SqlClient;
using TransportApp.Server.Data;
using TransportApp.Server.Dtos;
using TransportApp.Server.JWT_Helper;

namespace TransportApp.Server.Repository
{
    using Dapper;
    using Microsoft.Data.SqlClient;
    using System.Data;

    public class CommissionBillRepository : ICommissionBillRepository
    {
        private readonly IConfiguration _configuration;

        public CommissionBillRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CommissionBillDto> GetLoadAsync(string lcNo)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var parameters = new DynamicParameters();
            parameters.Add("@LC_No", lcNo);
            parameters.Add("@Mode", "V");

            var result = await connection.QueryFirstOrDefaultAsync<CommissionBillDto>(
                "Commission_Bill",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }


}
