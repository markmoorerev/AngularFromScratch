using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// returns the list of Users
        /// To convert this to async, 1) make the method async, 2) return a Task, 3) await the method, 4) use the async version of the method.
        /// 5) add using Microsoft.EntityFrameworkCore; 6) add using System.Threading.Tasks;
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// returns the User with the indicated Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}