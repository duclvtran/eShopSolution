using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Systems.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Systems.Users
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest request);

        Task<bool> Register(RegisterRequest request);

        Task<PagedResult<UserVM>> GetUserPaging(GetUserPagedingRequest request);
    }
}