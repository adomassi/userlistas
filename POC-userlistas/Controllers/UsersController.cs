namespace POC_userlistas.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using POC_userlistas.BL;
    using POC_userlistas.Models;

    // controller methods returns Ok (200) allways, result.success is true or false is resolved in client
    // please see FetchData.js component.
    // we can return 500 error on every error to client, but with returning result is just
    // more convenient in our extremely simple application
    [ApiController]    
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersBusinessService _usersBusinessService;       

        public UsersController(UsersBusinessService usersBusinessService)
        {
            _usersBusinessService = usersBusinessService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {  
            return Ok(await _usersBusinessService.GetUsers());           
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {           
            return Ok(await _usersBusinessService.AddUser(user));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User user)
        {           
            return Ok(await _usersBusinessService.ModifyUser(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {           
            return Ok(await _usersBusinessService.DeleteUser(id));           
        }
    }
}