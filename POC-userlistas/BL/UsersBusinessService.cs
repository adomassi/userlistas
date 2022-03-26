namespace POC_userlistas.BL
{
    using PGdemoApp.Core;
    using POC_userlistas.Constants;
    using POC_userlistas.DAL.Services;
    using POC_userlistas.Models;
    public class UsersBusinessService
    {
        private readonly ILogger<UsersBusinessService> _logger;
        private readonly UsersDataService _usersDataService;
        public UsersBusinessService(ILogger<UsersBusinessService> logger, UsersDataService usersDataService)
        {
            _usersDataService = usersDataService;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<Users>>> GetUsers()
        {
            try
            {
                return await _usersDataService.GetAllUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Result<IEnumerable<Users>>()
                { 
                    Error = Errors.Error
                };                
            }
        }

        public async Task<Result> AddUser(User user)
        {
            var result = new Result();
            try
            {
                await _usersDataService.AddUser(user);
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

        public async Task<Result> ModifyUser(User user)
        {
            var result = new Result();
            try
            {
                await _usersDataService.ModifyUser(user);
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

        public async Task<Result> DeleteUser(int id)
        {
            var result = new Result();
            try
            {
                await _usersDataService.DeleteUser(id);
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



