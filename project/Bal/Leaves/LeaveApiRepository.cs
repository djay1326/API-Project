using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dal.Models;
using Dal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bal.Leaves
{
    public class LeaveApiRepository : ILeaveApiRepository
    {
        private readonly HelperlandContextData _DbContext;
        private SignInManager<Users> _signInManager;
        public LeaveApiRepository(SignInManager<Users> signInManager, HelperlandContextData DbContext)
        {
            _DbContext = DbContext;
            _signInManager = signInManager;
        }
        public async Task<int> GetEmail(LoginViewModel loginViewModel)
        {
            //var data = _DbContext.leave.FindAsync(email);
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);
            Users u = _DbContext.Users.Where(x=>x.Email == loginViewModel.Email).FirstOrDefault();
            //string data = u.Email;
            //if(loginViewModel.Email == u.Email)
            //{
            //    return u.Id;
            //}
            int x = u.Id;
            return x;
            //return await _DbContext.leave.FindAsync(email);
        }

        public async Task<IEnumerable<leave>> Get()
        {
            return await _DbContext.leave.ToListAsync();
        }

        public async Task<leave> Get(int id)
        {
            return await _DbContext.leave.FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var leaveDelete = await _DbContext.leave.FindAsync(id);
            _DbContext.leave.Remove(leaveDelete);
            await _DbContext.SaveChangesAsync();
        }

        public async Task Update(int id, leave leaveinfo)
        {
            var leave = await _DbContext.leave.FindAsync(id);
            if(leave != null)
            {
                leave.fromDate = leaveinfo.fromDate;
                leave.toDate = leaveinfo.toDate;
                leave.reason = leaveinfo.reason;
                await _DbContext.SaveChangesAsync();
            }
        }

        public async Task<leave> Create(string name,leave leave)
        {
            var abc = _DbContext.Users.Where(x => x.UserName == name).FirstOrDefault();
            var z = abc.Id;
            var def = _DbContext.UserRoles.Where(x => x.UserId == z).FirstOrDefault();
            leave leavecomingdata = new leave();
            leavecomingdata.statusid = 1;
            leavecomingdata.roleid = def.RoleId;
            leavecomingdata.userid = z;
            leavecomingdata.fromDate = leave.fromDate;
            leavecomingdata.toDate = leave.toDate;
            leavecomingdata.reason = leave.reason;
            _DbContext.leave.Add(leavecomingdata);
            await _DbContext.SaveChangesAsync();
            return leavecomingdata;
        }
    }
}
