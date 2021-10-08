using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            return ReturnResult(result);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = IsUserExists(userForRegisterDto);
            if (userExists is ErrorResult)
            {
                return BadRequest(userExists);
            }

            var registerResult = _authService.Register(userForRegisterDto);
            if (registerResult is ErrorResult)
            {
                return BadRequest(userExists);
            }

            var result = _authService.CreateAccessToken(registerResult.Data);
            return ReturnResult(result);
        }

        [HttpPost("registerindividualcustomer")]
        public IActionResult RegisterForIndividualCustomer(
            IndividualCustomerForRegisterDto individualCustomerForRegisterDto)
        {
            var userExists = IsUserExists(individualCustomerForRegisterDto);
            if (userExists is ErrorResult)
            {
                return BadRequest(userExists);
            }

            var registerResult = _authService.RegisterForIndividualCustomer(individualCustomerForRegisterDto);
            if (registerResult is ErrorResult)
            {
                return BadRequest(userExists);
            }

            var result = _authService.CreateAccessToken(registerResult.Data);
            return ReturnResult(result);
        }

        private IResult IsUserExists(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return new ErrorResult(Messages.OperationFailed);
            }
            return new SuccessResult(Messages.OperationSuccessful);
        }

        private IActionResult ReturnResult(IResult result)
        {
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
