using System;

namespace Application.Models {
    public class BaseModel {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public BaseModel() {

        }

        public BaseModel(ApplicationDbContext context) {
            this.Created();
            this.Updated();
        }

        // We cannot keep this logic in the
        // constructor because every time the object
        // is instantiated the CreatedAt timestamp will be
        // updated.
        public void Created() {
            this.CreatedAt = DateTime.Now;
            this.IsActive = true;
        }

        public void Updated() {
            this.UpdatedAt = DateTime.Now;
        }

        public void Delete() {
            this.IsActive = false;
            this.Updated();
        }

        public void setActive(bool val) {
            this.IsActive = val;
            this.Updated();
        }
    }
}