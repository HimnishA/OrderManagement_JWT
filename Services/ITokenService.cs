using OrderManagementApi.Models;


namespace OrderManagementApi.Services;


public interface ITokenService
{
    string CreateToken(ApplicationUser user);
}