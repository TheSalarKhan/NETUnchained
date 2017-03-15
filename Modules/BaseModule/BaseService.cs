using Application.Models;

namespace Application.Services {
    public abstract class BaseService : Injectable {
        public BaseService(ApplicationDbContext context) : base(context) { }
    }
}