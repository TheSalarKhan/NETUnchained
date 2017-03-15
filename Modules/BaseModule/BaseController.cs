using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers {

    /**
        This controller is designed to enable BHRNDbContext injection to
        any controllers that extend it.
    */
    public class BaseController : Controller {
        protected readonly ApplicationDbContext _db;

        public BaseController(ApplicationDbContext context) {
            _db = context;
        }
        
    }
}
