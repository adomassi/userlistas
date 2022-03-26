namespace POC_userlistas.DAL.Services
{
    using Microsoft.EntityFrameworkCore;
    using PGdemoApp.Core;
    using PGdemoApp.data;
    using POC_userlistas.Constants;
    using POC_userlistas.Models;

    public class UsersDataService
    {
        private readonly AppDbContext _db;

        public UsersDataService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<IEnumerable<Users>>> GetAllUsers()
        {
            var result = new Result<IEnumerable<Users>>();
            result.ResultObject = await _db.Users.ToListAsync();
            result.Success = true;
            return result;
        }

        public async Task AddUser(User user)
        {            
            await _db.Users.AddAsync(new Users() { Name = user.Name });
            await _db.SaveChangesAsync();           
        }

        public async Task<Result> ModifyUser(User user)
        {
            var result = new Result();
            var userToModify = await _db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (userToModify == null)
            {
                result.Error = Errors.UserNotFound;
                return result;
            }    

            userToModify.Name = user.Name;
            await _db.SaveChangesAsync();
            result.Success = true;
            return result;
        }

        public async Task<Result> DeleteUser(int id)
        {
            var result = new Result();
            var userToModify = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userToModify == null)
            {
                result.Error = Errors.UserNotFound;
                return result;
            }
            _db.Users.Remove(userToModify);
            await _db.SaveChangesAsync();
            result.Success = true;
            return result;
        }

        public async Task AddBulkUsers(List<Users> users)
        {
            await _db.Users.AddRangeAsync(users);
            await _db.SaveChangesAsync();
        }
    }
}
