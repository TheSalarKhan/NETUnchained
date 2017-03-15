using Application.Entity;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers {
    public class AdminLoginController : BaseController
    {
        
        public AdminLoginController(ApplicationDbContext context) : base(context)
        {
        }


        [RouteAttribute("api/adminlogin/validate"),HttpPostAttribute]
        public ValidateAdminLoginRes ValidateAdminLogin([FromBodyAttribute] ValidateAdminLoginReq request) {
            return new AdminLoginService(_db).ValidateAdminLogin(request);
        }


        
        

    }
}