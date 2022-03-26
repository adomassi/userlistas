namespace POC_userlistas.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using POC_userlistas.BL;

    // controller methods returns Ok (200) allways, result.success is true or false is resolved in client
    // please see FetchData.js component.
    // we can return 500 error on every error to client, but with returning result is just
    // more convenient in our extremely simple application
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly FileOperationsBusinessService _fileOperationsBusinessService;
        public FilesController(FileOperationsBusinessService fileOperationsBusinessService)
        {
            _fileOperationsBusinessService = fileOperationsBusinessService;
        }
  
        [HttpPost]      
        public async Task<IActionResult> Post()
        {             
            return Ok(await _fileOperationsBusinessService.AddUsersFromFile(Request));  
        }       
    }
}
