using Microsoft.EntityFrameworkCore;
using TransportApp.Server.Data;
using TransportApp.Server.DataModels;
using System;
using System.Threading.Tasks;
using System.Linq;
using TransportApp.Server.JWT_Helper;

namespace TransportApp.Server.Repository
{
    public class TruckRepository : ITruckRepository
    {
        private readonly AppDbContext _context;
        private readonly IJwtHelper _jwtHelper;
        public readonly ITokenValidator _tokenValidator;

        public TruckRepository(AppDbContext context, IJwtHelper jwtHelper,ITokenValidator tokenValidator)
        {
            _context = context;
            _jwtHelper = jwtHelper;
            _tokenValidator = tokenValidator;
        }

        private CustomResponse<T> ValidateToken<T>(string authToken)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                return new CustomResponse<T>
                {
                    status = 401,
                    message = "Missing authorization token",
                    data = default
                };
            }

            if (authToken.StartsWith("Bearer "))
            {
                authToken = authToken.Substring(7);
            }

            bool isExpired;
            var principal = _jwtHelper.ValidateToken(authToken, out isExpired);

            if (principal == null)
            {
                return new CustomResponse<T>
                {
                    status = 401,
                    message = isExpired ? "Token has expired" : "Invalid token",
                    data = default
                };
            }

            return null; // Token is valid
        }

        public async Task<CustomResponse<TruckMaster>> GetTruckByNumberAsync(string authToken, string truckNo)
        {
            var tokenResponse = _tokenValidator.ValidateToken<TruckMaster>(authToken);

            if (tokenResponse != null) return tokenResponse;

            try
            {
                var truck = _context.TruckMaster
                            .FromSqlRaw("EXEC TruckMaster @Mode='V', @Truck_No={0}", truckNo)
                            .AsEnumerable()
                            .SingleOrDefault();

                if (truck == null)
                {
                    return new CustomResponse<TruckMaster>
                    {
                        status = 404,
                        message = "Truck not found",
                        data = null
                    };
                }

                return new CustomResponse<TruckMaster>
                {
                    status = 200,
                    message = "Truck retrieved successfully",
                    data = truck
                };
            }
            catch (Exception ex)
            {
                return new CustomResponse<TruckMaster>
                {
                    status = 500,
                    message = "An error occurred while retrieving the truck: " + ex.Message,
                    data = null
                };
            }
        }

        public async Task<CustomResponse<string>> AddTruckAsync(string authToken, TruckMaster truck)
        {
            var tokenResponse = _tokenValidator.ValidateToken<string>(authToken);

            if (tokenResponse != null) return tokenResponse;

            try
            {
                var existingTruck = await _context.TruckMaster.FindAsync(truck.Truck_No);
                if (existingTruck != null)
                {
                    return new CustomResponse<string>
                    {
                        status = 409,
                        message = "Truck with the same Truck_No already exists",
                        data = null
                    };
                }

                await _context.Database.ExecuteSqlRawAsync("EXEC TruckMaster @Mode='A', @Truck_No={0}, @NameBoard={1}, @Mobile={2}, @Mobile2={3}, @Mobile3={4}, @PAN={5}, @Address={6}, @Email={7}, @Type={8}, @Parent_Truck={9}, @UserName={10}",
                    truck.Truck_No, truck.NameBoard, truck.Mobile, truck.Mobile2, truck.Mobile3, truck.PAN, truck.Address, truck.Email, truck.Type, truck.Parent_Truck, truck.Created_by);

                return new CustomResponse<string>
                {
                    status = 201,
                    message = "Truck added successfully",
                    data = "Success"
                };
            }
            catch (Exception ex)
            {
                return new CustomResponse<string>
                {
                    status = 500,
                    message = "An error occurred while adding the truck: " + ex.Message,
                    data = null
                };
            }
        }

        public async Task<CustomResponse<string>> UpdateTruckAsync(string authToken, TruckMaster truck)
        {
            var tokenResponse = ValidateToken<string>(authToken);
            if (tokenResponse != null) return tokenResponse;

            try
            {
                var existingTruck = await _context.TruckMaster.FindAsync(truck.Truck_No);
                if (existingTruck == null)
                {
                    return new CustomResponse<string>
                    {
                        status = 404,
                        message = "Truck not found, update failed",
                        data = null
                    };
                }

                await _context.Database.ExecuteSqlRawAsync("EXEC TruckMaster @Mode='U', @Truck_No={0}, @NameBoard={1}, @Mobile={2}, @Mobile2={3}, @Mobile3={4}, @PAN={5}, @Address={6}, @Email={7}, @Type={8}, @Parent_Truck={9}, @UserName={10}",
                    truck.Truck_No, truck.NameBoard, truck.Mobile, truck.Mobile2, truck.Mobile3, truck.PAN, truck.Address, truck.Email, truck.Type, truck.Parent_Truck, truck.Created_by);

                return new CustomResponse<string>
                {
                    status = 200,
                    message = "Truck updated successfully",
                    data = "Success"
                };
            }
            catch (Exception ex)
            {
                return new CustomResponse<string>
                {
                    status = 500,
                    message = "An error occurred while updating the truck: " + ex.Message,
                    data = null
                };
            }
        }
    }
}
