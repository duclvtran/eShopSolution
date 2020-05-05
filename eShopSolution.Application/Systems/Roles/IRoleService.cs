using eShopSolution.ViewModels.Systems.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Systems.Roles
{
    public interface IRoleService
    {
        Task<List<RoleVm>> GetAll();
    }
}