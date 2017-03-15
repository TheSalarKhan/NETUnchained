using System.Linq;
using Application.Entity;
using Application.Models;

namespace Application.Services {
    public class PushNotificationService : BaseService
    {
        public PushNotificationService(ApplicationDbContext context) : base(context)
        {
        }

        private string getDeviceId(int userId) {
            if(userId == -1)
                return "All";

            Users user = _db.Users.Where(m => 
                m.Id == userId &&
                m.UserStatus == Constant.USER_ACTIVE).FirstOrDefault();

            if(user == null) {
                return null;
            }

            return user.PushId;
        }
        public PushNotificationRes PushNotification(PushNotificationReq request) {
            string deviceId = getDeviceId(request.UserId);

            if(deviceId == null) {
                return new PushNotificationRes()
                {
                    status = 0,
                    developerMessage = "Invalid UserId"
                };
            }
            

            Utils.NotificationUtils.SendNotification(request.Message,deviceId);

            return new PushNotificationRes()
            {
                status = 1,
                developerMessage = "Notification Sent"
            };
        }
    }
}