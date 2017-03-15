
namespace Application.Entity
{
    public class ChangePassword
    {
        public int userID { get; set; }
        public string password { get; set; }
		public string newPassword { get; set; }
    }
}