using System.Collections.Generic;

namespace Application.Models
{
    public class Users : BaseModel
    {
        public Users()
        {
        }

        public Users(ApplicationDbContext context) : base(context) {
            
        }

        public string Phone { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Address { get; set; }
        public int EmailVerified { get; set; }

        public string TokenActivate { get; set; }
        public string TokenForgetpw { get; set; }
        public string PushId { get; set; }

        public virtual BinaryData ProfileImage { get; set; }

        public int UserStatus { get; set; }




    }
}
