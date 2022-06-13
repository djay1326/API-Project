using ApiProject.AuthData;
using Bal.Leaves;
using Dal.Models;
using Dal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveApiRepository _leaveApiRepository;
        private readonly UserManager<Users> userManager;

        public LeaveController(ILeaveApiRepository leaveApiRepository, UserManager<Users> userManager)
        {
            _leaveApiRepository = leaveApiRepository;
            this.userManager = userManager;
        }

        [Route("first")]
        [HttpGet]
        [CustomAction]
        public async Task<IEnumerable<leave>> GetLeaves()
        {
           return await _leaveApiRepository.Get();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<leave>> GetLeaves(int id)
        {
            return await _leaveApiRepository.Get(id);
        }
        /// <summary>
        /// This is used to Delete a leave request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<leave>> Delete(int id)
        {
            var leaveDelete = await _leaveApiRepository.Get(id);
            if(leaveDelete == null)
            {
                return NotFound();
            }
            await _leaveApiRepository.Delete(leaveDelete.leaveid);
            return Ok(await _leaveApiRepository.Get());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<leave>> Update(int id,[FromBody] leave leave)
        {
            await _leaveApiRepository.Update(id, leave);
            return (await _leaveApiRepository.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult<leave>> CreateLeave([FromBody] leave leave)
        {
            var name = User.Identity.Name;
            //var abc = _DbContext.Users.Where(x => x.Email == y).FirstOrDefault();
            //int userId = int.Parse(userManager.GetUserId(User));
            //var user = userManager.FindByName(User.Identity.Name);
            //int id = RequestContext.Principal.Identity.GetUserId();
            //var userId = await _leaveApiRepository.GetEmail();
            var createleave = await _leaveApiRepository.Create(name,leave);
            return Ok(createleave);
        }

        
        [HttpGet]
        [Route("Second")]
        [AuthorizeActionFilter("Read")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> about()
        {
            string data = "Given Authorization";
            return Ok(data);
        }

        [HttpGet]
        [Route("Third")]
        [AuthorizeActionFilter("Write")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> faq()
        {
            string data = "Not Given Authorization";

            return Ok(data);
        }


        [HttpGet]
        [Route("Fourth")]        
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> GenerateError()
        {
            throw new NotImplementedException();
        }
        
    }
}
