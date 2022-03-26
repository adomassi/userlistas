namespace POC_userlistas.BL
{
    using Newtonsoft.Json;
    using PGdemoApp.Core;
    using POC_userlistas.Constants;
    using POC_userlistas.DAL.Services;
    using POC_userlistas.Models;
    public class FileOperationsBusinessService
    {
        private readonly ILogger<FileOperationsBusinessService> _logger;
        private readonly UsersDataService _usersDataService;
        public FileOperationsBusinessService(ILogger<FileOperationsBusinessService> logger, 
            UsersDataService usersDataService)
        {
            _usersDataService = usersDataService;
            _logger = logger;
        }

        public async Task<Result> AddUsersFromFile(HttpRequest? request)
        {
            var result = new Result();
            try
            {
                if (request == null)
                {                    
                    result.Error = Errors.HttpRequestIsNull;
                    return result;
                }
               
                var file = request.Form.Files.FirstOrDefault(x => x.Length != 0);

                if (file == null)
                {
                    result.Error = Errors.NoFile;
                    return result;
                }

                using (var readStream = new StreamReader(file.OpenReadStream()))
                {
                    var fileStream = readStream.ReadToEnd();
                    var users = JsonConvert.DeserializeObject<List<Users>>(fileStream);

                    await _usersDataService.AddBulkUsers(users);                   
                }

                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                result.Error = Errors.Error;
                return result;
            }
        }    
    }
}



