using Application.Models;

namespace Application {
    public abstract class Injectable
    {
        protected ApplicationDbContext _db { get; set; }

        public Injectable(ApplicationDbContext _db) {
            this._db = _db;
        }
    }
}