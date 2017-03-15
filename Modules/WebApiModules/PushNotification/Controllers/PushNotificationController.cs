using System.Collections.Generic;
using System.Linq;
using Application.Entity;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers {
    public class PushNotificationController : BaseController
    {
        public PushNotificationController(ApplicationDbContext context) : base(context)
        {
        }

        [RouteAttribute("api/notification/pushnotification"),HttpPostAttribute]
        public PushNotificationRes PushNotification([FromBodyAttribute] PushNotificationReq request) {
            return new PushNotificationService(_db).PushNotification(request);
        }


        [RouteAttribute("api/notification/getUsersForDropDown"), HttpPostAttribute]
        public JsonResult GetUsersForDropDown()
        {

            // This is how we pickup data from a chosen request
            string query = Utils.ChosenUtils.GetQuery(Request);

            List<ChosenAutoCompleteResult> toSend =
                _db.Users.Where(m => m.UserStatus == Constant.USER_ACTIVE &&
                    (m.Email.Contains(query) || m.Name.Contains(query)
                )).Select(x =>
                    new ChosenAutoCompleteResult(x.Id.ToString(), x.Name + " " + x.Email)
                ).ToList();

            toSend.Add(new ChosenAutoCompleteResult("-1","All"));

            return Utils.ChosenUtils.GetResponse(query,toSend.ToArray());
        }
    }
}