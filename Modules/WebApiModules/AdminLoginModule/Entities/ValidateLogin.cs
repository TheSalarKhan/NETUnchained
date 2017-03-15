namespace Application.Entity {
    public class ValidateAdminLoginRes : BaseResponse {

    }

    public class ValidateAdminLoginReq {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}