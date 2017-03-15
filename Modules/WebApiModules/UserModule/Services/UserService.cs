using System;
using System.Linq;
using Application.Entity;
using Application.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService : BaseService
	{

		public UserService(ApplicationDbContext context) : base(context) {}

        public BaseResponse insertUser(RegisterUserReqObj user)
		{
			string token = "";
			BaseResponse response = new BaseResponse();
            try
            {
                var db = _db;
                var isRegistered = db.Users.Where(c => c.Email.Equals(user.email)).Include(m => m.ProfileImage).FirstOrDefault();


                if (isRegistered == null)
                {
                    Random rnd = new Random();
                    token = rnd.Next(100000, 999999).ToString();

                    var insertUserObj = new Users(_db);

                    insertUserObj.Phone = user.phone;
                    insertUserObj.Name = user.name;
                    insertUserObj.Email = user.email;
                    insertUserObj.Password = Utils.EncryptionUtils.CreateMD5(user.password);
                    insertUserObj.Address = user.address;
                    insertUserObj.ProfileImage = new BinaryData(Encoding.UTF8.GetBytes(user.profileImage));



                    insertUserObj.PushId = "";

                    insertUserObj.TokenActivate = token;
                    insertUserObj.EmailVerified = 0;

                    insertUserObj.UserStatus = Constant.USER_ACTIVE;

                    // {
                    //     Email = user.email,
                    //     Phone = user.phone,
                    //     Password = Utils.EncryptionUtils.CreateMD5(user.password),
                    //     TokenActivate = token,
                    //     EmailVerified = 0,
                    //     UserStatus = Constant.USER_ACTIVE,
                    //     CreatedAt = DateTime.Today,
                    //     PushId = "No Push",
                    //     Name = user.name,
                    //     Address = user.address,
                    //     // the user.profileImage is a base64, we can't insert it into the db
                    //     // its a huge string..... its bad!
                    //     ProfileImage = Encoding.UTF8.GetBytes(user.profileImage)
                    // };




                    db.Users.Add(insertUserObj);
                    response.status = db.SaveChanges();

                    // db save changes will return 2 because there are two objects saved
                    // 1) The user object.
                    // 2) The image object.
                    if (response.status == 2)
                    {
                        string subject = Application.Constant.INSERT_USER_EMAIL_SUBJECT;
                        string body = Application.Constant.INSERT_USER_EMAIL_BODY + token;

                        if (Utils.EmailUtils.sendEmail(subject, body, user.email))
                        {
                            response.status = 1;
                            response.developerMessage = "Account Created, Check Your Email";
                        }
                        else
                        {
                            response.status = 2;
                            response.developerMessage = "Couldn't Send Verification Email, Try Again Later!";
                        }
                    }
                    else
                    {
                        response.status = -2;
                        response.developerMessage = "Couldn't Register, Try Again Later!";
                    }
                }
                else
                {
                    var registeredUser = isRegistered;

                    if (registeredUser.UserStatus == Constant.USER_ACTIVE)
                    {
                        response.status = -1;
                        response.developerMessage = "Email Already Exist";
                    }
                    else
                    {
                        Random rnd = new Random();
                        token = rnd.Next(100000, 999999).ToString();

                        registeredUser.Phone = user.phone;
                        registeredUser.Password = Utils.EncryptionUtils.CreateMD5(user.password);
                        registeredUser.UserStatus = Constant.USER_REACTIVE;
                        registeredUser.Address = user.address;
                        registeredUser.TokenActivate = token;
                        registeredUser.EmailVerified = 0;

                        registeredUser.ProfileImage.Data = Encoding.UTF8.GetBytes(user.profileImage);
                        
                        db.SaveChanges();

                        string subject = Constant.REACTIVATION_EMAIL_SUBJECT;
                        string body = Constant.REACTIVATION_EMAIL_BODY + token;

                        if (Utils.EmailUtils.sendEmail(subject, body, user.email))
                        {
                            response.status = 1;
                            response.developerMessage = "Account Reactivated, Check Your Email";

                        }
                        else
                        {
                            response.status = 2;
                            response.developerMessage = "Couldn't Send Verification Email, Try Again Later!";

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                response.developerMessage = "Something went wrong: "+ex.Message;
                response.status = 3;
            }
			return response;
		}


		public UserResObj loginUser(string email, string password, string pushID)
		{
			UserResObj userLogin = new UserResObj();
			userLogin.response = new BaseResponse();

			string encryptedPassword = Utils.EncryptionUtils.CreateMD5(password);
            try
            {
                var db = _db;

                var temp = db.Users.Where(u => u.UserStatus == Constant.USER_ACTIVE && u.Email.Equals(email) && u.Password.Equals(encryptedPassword))
                        .Include(m => m.ProfileImage)
                        .FirstOrDefault();

                if (temp != null)
                {

                    var user = temp;

                    if (user.UserStatus == Constant.USER_INACTIVE)
                    {
                        userLogin.response.status = 3;
                        userLogin.response.developerMessage = "Account Inactive";
                        return userLogin;
                    }

                    int isEmailVerified = (int)user.EmailVerified;
                    if (isEmailVerified == 1)
                    {
                        if (string.IsNullOrEmpty(pushID))
                        {
                            pushID = "pushID";
                        }
                        user.PushId = pushID;
                        db.SaveChanges();

                        userLogin.response.status = 1;
                        userLogin.response.developerMessage = "Login Successfull";

                        userLogin.fillObject(user);
                        return userLogin;
                    }
                    else
                    {
                        string subject = Application.Constant.INSERT_USER_EMAIL_SUBJECT;
                        string body = Application.Constant.INSERT_USER_EMAIL_BODY + temp.TokenActivate;
                        Utils.EmailUtils.sendEmail(subject, body, temp.Email);

                        userLogin.response.status = 2;
                        userLogin.response.developerMessage = "Email not verified.";
                        return userLogin;
                    }
                }


                userLogin.response.status = -1;
                userLogin.response.developerMessage = "Incorrect Credentials";
                return userLogin;
            }
            catch (Exception e)
            {

                userLogin.response.status = 0;
                userLogin.response.developerMessage = "Something Went Wrong: "+ e.Message;
                return userLogin;
            }
		}

		public UserResObj validateUser(string email, string token, string pushID)
		{
			UserResObj userLogin = new UserResObj();
			userLogin.response = new BaseResponse();

            try
            {

                var db = _db;

                var temp = db.Users.Where(u => u.Email.Equals(email) && u.TokenActivate.Equals(token)).Include(m => m.ProfileImage).FirstOrDefault();


                if (temp != null)
                {
                    if (string.IsNullOrEmpty(pushID))
                        pushID = "pushID";

                    var userObj = temp;
                    userObj.EmailVerified = 1;
                    userObj.UserStatus = Constant.USER_ACTIVE;
                    userObj.PushId = pushID;
                    db.SaveChanges();
                    userLogin.response.status = 1;
                    userLogin.response.developerMessage = "Verification Successfull";

                    userLogin.fillObject(userObj);
                }

                else
                {
                    userLogin.response.status = -1;
                    userLogin.response.developerMessage = "Invalid Pin";
                }
            }
            catch (Exception e) {
                userLogin.response.status = -1;
                userLogin.response.developerMessage = "Something went wrong: " + e.Message;

            }

			return userLogin;

		}


        public BaseResponse CancelAccount(int userID) {

            BaseResponse response = new BaseResponse();

            try
            {
                var db = _db;
                var user = db.Users.Where(u => u.Id == userID).Single();
                user.UserStatus = Constant.USER_INACTIVE;


                // var creditCard = db.CreditCard.SingleOrDefault(c => c.UserId == userID);
                // db.CreditCard.Remove(creditCard);


                db.SaveChanges();

                response.status = 1;
                response.developerMessage = "Account has been cancelled";
            }
            catch (Exception e)
            {

                response.status = -1;
                response.developerMessage = "Something went wrong: " + e.Message;
                return response;

            }

            return response;

        }

        public BaseResponse ForgetPasswordEmail(string emailAddress)
		{
			BaseResponse res = new BaseResponse();

			Random rnd = new Random();
			int randomNumber = rnd.Next(100000, 999999);
			try
			{
                var db = _db;

                var user = db.Users.Where(b => b.Email == emailAddress && b.UserStatus == 1);

                if (user != null && user.Count() > 0)
                {
                    var obj = user.Single();
                    obj.TokenForgetpw = randomNumber.ToString();
                    res.status = db.SaveChanges();

                    string subject = Constant.FORGET_PASSWORD_EMAIL_SUBJECT;
                    string body = Constant.FORGET_PASSWORD_EMAIL_BODY + randomNumber;

                    if (Utils.EmailUtils.sendEmail(subject, body, emailAddress))
                    {
                        res.status = 1;
                        res.developerMessage = "Email sent with instructions";

                    }
                    else
                    {
                        res.status = 0;
                        res.developerMessage = "Couldn't Send Instruction Email, Try Again Later!";

                    }

                }
                else
                {
                    res.status = 2;
                    res.developerMessage = "No such account exist";
                }
			}
			catch (Exception e)
			{
				res.status = -1;
				res.developerMessage = "Something went wrong: " + e.Message;
			}

			return res;
		}

		public BaseResponse verifyPin(VerifyPinReqObj verPin)
		{
			BaseResponse res = new BaseResponse();

            try
            {
                var db = _db;

                var obj = db.Users.SingleOrDefault(b => b.Email == verPin.email);

                if (obj != null)
                {

                    if (obj.TokenForgetpw.Equals(verPin.token))
                    {

                        obj.Password = Utils.EncryptionUtils.CreateMD5(verPin.password);
                        res.status = db.SaveChanges();

                        string subject = Constant.VERIFY_PIN_EMAIL_SUBJECT;
                        string body = Constant.VERIFY_PIN_EMAIL_BODY;
                        string address = obj.Email;

                        if (Utils.EmailUtils.sendEmail(subject, body, address))
                        {

                            res.status = 1;
                            res.developerMessage = "Password changed successfully.";
                        }
                        else
                        {
                            res.status = -2;
                            res.developerMessage = "Failed to send email. SMTP Server Down.";
                        }

                    }
                    else
                    {
                        res.status = 0;
                        res.developerMessage = "Wrong token provided.";
                    }
                }
                else
                {
                    res.developerMessage = "Token is not set in DB OR Email is incorrect.";
                    res.status = 2;
                }
            }
            catch (Exception e)
            {
                res.developerMessage = "Something went wrong: " + e.Message;
                res.status = -1;
            }
			return res;
		}


		public BaseResponse ChangePassword(ChangePassword updatePassword)
		{

            if (updatePassword.password.Equals(updatePassword.newPassword)) {

                return new BaseResponse
                {
                    status = -4,
                    developerMessage = "Current password is same as old password"
                };
            }


            BaseResponse response = new BaseResponse();

            try
            {
                var db = _db;

                string encryptedPassword = Utils.EncryptionUtils.CreateMD5(updatePassword.password);
                var obj = db.Users.SingleOrDefault(b => b.Id == updatePassword.userID && b.Password == encryptedPassword);
                if (obj != null)
                {
                    obj.Password = Utils.EncryptionUtils.CreateMD5(updatePassword.newPassword);
                    db.SaveChanges();
                    response.status = 1;
                }

                if (response.status == 1)
                {

                    string subject = Constant.CHANGE_PASSWORD_EMAIL_SUBJECT;
                    string body = Constant.CHANGE_PASSWORD_EMAIL_BODY;
                    string address = obj.Email;

                    if (Utils.EmailUtils.sendEmail(subject, body, address))
                    {
                        response.status = 1;
                        response.developerMessage = "Password changed successfully";
                    }
                    else
                    {
                        response.status = -2;
                        response.developerMessage = "Failed to send email; SMTP Server Down";

                    }

                }

                else
                {
                    return new BaseResponse
                    {
                        status = -3,
                        developerMessage = "Current Password is not Valid"
                    };
                }
            }
            catch (Exception e)
            {
                return new BaseResponse
                {
                    status = -1,
                    developerMessage = "Something went wrong:" + e.Message
                };
            }

			return response;
		}



		public BaseResponse updateUser(UpdateUserReqObj updatedUserObj)
		{
			BaseResponse res = new BaseResponse();

            try
            {
                var db = _db;

                var obj = db.Users.Include(m => m.ProfileImage).SingleOrDefault(b => b.Id == updatedUserObj.userID);
                if (obj != null)
                {
                    obj.Phone = updatedUserObj.phone;
                    obj.Name = updatedUserObj.name;
                    obj.Address = updatedUserObj.address;

                    // Updat the image only when not null and not empty.
                    // if(updatedUserObj.image != null && updatedUserObj.image.Trim() != "")
                    obj.ProfileImage.Data = Encoding.UTF8.GetBytes(updatedUserObj.image);
                    
                    res.status = db.SaveChanges();
                }
                res.status = 1;
                res.developerMessage = res.developerMessage + "Profile Updated Successfully";
                // if (res.status > 0)
                // {
                    
                // }
                // else
                // {
                //     // res.status = -2;
                //     res.developerMessage = res.developerMessage + "Profile Didn't Update, Try Again Later" + res.status;
                // }
            }
            catch (Exception e)
             {
                res.status = -1;
                res.developerMessage = res.developerMessage + "Something went wrong: " + e.Message;
            }


			return res;
		}

        public GetAllUsersRes GetAllUsers(GetAllUsersReq request) {
            return new GetAllUsersRes(_db.Users.Where(m=>m.UserStatus == Constant.USER_ACTIVE).ToArray());
        }

        public DeleteUserRes DeleteUser(DeleteUserReq request) {
            Users user = _db.Users.Where(m => m.Id == request.UserId && m.UserStatus != Constant.USER_INACTIVE).FirstOrDefault();


            if(user == null) {
                return new DeleteUserRes()
                {
                    status = 0,
                    developerMessage = "User does not exist"
                };
            }

            user.UserStatus = Constant.USER_INACTIVE;

            _db.SaveChanges();

            return new DeleteUserRes()
            {
                status = 1,
                developerMessage = "User removed"
            };
        }


	}
}
