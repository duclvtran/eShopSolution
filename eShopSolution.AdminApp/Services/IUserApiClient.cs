using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Systems.Users;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);

        Task<bool> Register(RegisterRequest request);

        Task<PagedResult<UserVM>> GetUserPaging(GetUserPagedingRequest request);
    }
}