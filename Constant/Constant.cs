namespace Application
{
    public static class Constant
    {
        public const string APPLICATION_NAME = "Application";
        public const string APPLICATION_NAME_LOWER = "application";
        // Firebase
        public const string FCM_SERVER_KEY= "";
		public const string FCM_SENDER_KEY = "";
        public const string FCM_TITLE = APPLICATION_NAME;

        // BrainTree
        public const string BT_MERCHANT_ID = "";
        public const string BT_PUBLIC_KEY = "";
        public const string BT_PRIVATE_KEY = "";

        // Send Grid
        public const string SENDGRID_API_KEY = "";
        public const string SENDGRID_API_TOKEN = "";


        public const int USER_ACTIVE = 1;
        public const int USER_INACTIVE = 0;
        public const int USER_REACTIVE = 2;

        public const string EMAIL_FROM_NAME = APPLICATION_NAME;
        public const string EMAIL_FROM_ADDRESS = "no-reply@"+APPLICATION_NAME_LOWER+".com";
        public const string INSERT_USER_EMAIL_SUBJECT = "Verification of email for "+APPLICATION_NAME;
        public const string INSERT_USER_EMAIL_BODY = "Thank you for signing up with "+APPLICATION_NAME+".\nPlease verify your email address with this code: ";

        public const string REACTIVATION_EMAIL_SUBJECT= "Verification of reactivation of "+APPLICATION_NAME+" account";
        public const string REACTIVATION_EMAIL_BODY= "Thank you for reactivating your "+APPLICATION_NAME+" account.\nPlease verify your account with this Code : ";

        public const string FORGET_PASSWORD_EMAIL_SUBJECT = APPLICATION_NAME+" App - Confirm password reset";
        public const string FORGET_PASSWORD_EMAIL_BODY = "Verify your password reset with this code: ";

        public const string VERIFY_PIN_EMAIL_SUBJECT = "Password change notification from "+APPLICATION_NAME;
        public const string VERIFY_PIN_EMAIL_BODY = "Your password was successfully changed.";

        public const string CHANGE_PASSWORD_EMAIL_SUBJECT = "Password change notification from "+APPLICATION_NAME+".";
        public const string CHANGE_PASSWORD_EMAIL_BODY = "Your password was successfully changed.";

    }
}