using System.Linq;
using Application.Models;

namespace Application.Entity {
    public class GetAllUsersReq {

    }

    


    public class GetAllUsersRes {
        public UserViewModel[] Users { get; set; }

        public GetAllUsersRes(Users[] users) {
            if(users == null) {
                this.Users = new UserViewModel[0];
                return;
            }

            this.Users = users.Select( x => new UserViewModel() {
                id = x.Id,
                name = x.Name,
                email = x.Email,
                phone = x.Phone,
                address = x.Address
            }).ToArray();
        }
    }
}