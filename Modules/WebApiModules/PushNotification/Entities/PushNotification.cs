namespace Application.Entity {
    
    public class PushNotificationReq {
        public int UserId { get; set; }
        public string Message { get; set; }
    }

    public class PushNotificationRes : BaseResponse {
        
    }
}