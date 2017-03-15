using System.Linq;
using Application.Entity;
using Application.Models;

namespace Application.Services
{
    public class AdminLoginService : BaseService
    {
        public AdminLoginService(ApplicationDbContext context) : base(context)
        {
        }

        public ValidateAdminLoginRes ValidateAdminLogin(ValidateAdminLoginReq request) {
            string userName = request.UserName;
            string password = Utils.EncryptionUtils.CreateMD5(request.Password);

            AdminLogin login = 
                _db.AdminLogins
                    .Where(m => m.UserName == userName && m.Password == password).FirstOrDefault();
            
            if(login == null) {
                return new ValidateAdminLoginRes()
                {
                    status = 0,
                    developerMessage = "Invalid UserName or Password"
                };
            }

            return new ValidateAdminLoginRes()
            {
                status = 1,
                developerMessage = "Login Successful"
            };
        }
    }
}