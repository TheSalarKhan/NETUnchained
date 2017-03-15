using Application.Entity;
using Application.Services;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class UserController : BaseController
	{

		public UserController(ApplicationDbContext context) : base(context) {}

    	[Route("api/users/register"), HttpPost]
		public BaseResponse AddUser([FromBody] RegisterUserReqObj user)
		{
			return new UserService(_db).insertUser(user);
		}

		[Route("api/users/login"), HttpPost]
		[ProducesResponseType(typeof(UserResObj),200)]
		public IActionResult LoginUser([FromBody] LoginReqObj credential)
		{

			return Ok(new UserService(_db).loginUser(credential.email, credential.password, credential.pushID));

		}

		[Route("api/users/validate"), HttpPost]
		public UserResObj ValidateUser([FromBody] ValidateUser user)
		{
			return new UserService(_db).validateUser(user.email, user.token,user.pushID);
		}


		[Route("api/users/forgetPassword"), HttpPost]
		public BaseResponse ForgetPasswordEmail([FromBody] forgetPasswordEmail userEmail)
		{
			return new UserService(_db).ForgetPasswordEmail(userEmail.email);
		}

		[Route("api/users/verifyForgetPasswordPin"), HttpPost]
		public BaseResponse VerifyPin([FromBody] VerifyPinReqObj verifyPin)
		{
			return new UserService(_db).verifyPin(verifyPin);
		}

		[Route("api/users/changePassword"), HttpPost]
		public BaseResponse ChangePassword([FromBody] ChangePassword updatePassword)
		{
			return new UserService(_db).ChangePassword(updatePassword);
		}

		[Route("api/users/updateUser"), HttpPost]
		public BaseResponse UpdateUser([FromBody] UpdateUserReqObj updatedUserObj)
		{
			return new UserService(_db).updateUser(updatedUserObj);
		}

    	[Route("api/users/cancelAccount"), HttpPost]
		public BaseResponse CancelAccount([FromBody] CancelAccountReqObj cancelObj)
        {
            return new UserService(_db).CancelAccount(cancelObj.userID);
        }


		[Route("api/users/getAllUsers"), HttpPost]
		public GetAllUsersRes Users([FromBody] GetAllUsersReq request)
        {
            return new UserService(_db).GetAllUsers(request);
        }


		[Route("api/users/deleteUser"), HttpPost]
		public DeleteUserRes Users([FromBody] DeleteUserReq request)
        {
            return new UserService(_db).DeleteUser(request);
        }

    }
}
