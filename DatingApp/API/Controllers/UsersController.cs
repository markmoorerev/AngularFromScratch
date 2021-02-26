using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        // private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            this._mapper = mapper;
            //this._context = context;
            this._userRepository = userRepository;
        }

        /// <summary>
        /// returns the list of Users
        /// To convert this to async, 
        /// 1) make the method async, 
        /// 2) return a Task, 
        /// 3) await the method,
        /// 4) use the async version of the method. 5) add using Microsoft.EntityFrameworkCore; 
        /// 6) add using System.Threading.Tasks;
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            // IEnumerable<AppUser> users = await _userRepository.GetUsersAsync();
            //IEnumerable<MemberDto> usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);  // this will automagically mapp users to MemberDto objects using the Mapper Library
            // return Ok(usersToReturn);
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }

        /// <summary>
        /// returns the User with the indicated Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }
    }
}