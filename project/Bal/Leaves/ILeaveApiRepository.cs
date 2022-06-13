using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dal.Models;
using Dal.ViewModels;

namespace Bal.Leaves
{
    public interface ILeaveApiRepository
    {
        Task<IEnumerable<leave>> Get();
        Task<leave> Get(int id);

        Task Delete(int id);

        Task Update(int id, leave leaveinfo);

        Task<leave> Create(string name, leave leave);

        Task<int> GetEmail(LoginViewModel loginViewModel);
    }
}
