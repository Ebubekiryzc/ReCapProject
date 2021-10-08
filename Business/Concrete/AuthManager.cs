using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IIndividualCustomerService _individualCustomerService;
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IIndividualCustomerService individualCustomerService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _individualCustomerService = individualCustomerService;
        }
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            var user = UserAdd(userForRegisterDto);
            return new SuccessDataResult<User>(user, Messages.RegistrationSuccessful);
        }

        public IDataResult<User> RegisterForIndividualCustomer(IndividualCustomerForRegisterDto individualCustomerForRegisterDto)
        {
            var user = UserAdd(individualCustomerForRegisterDto);
            int userId = _userService.GetByMail(individualCustomerForRegisterDto.Email).Data.Id;
            var individualCustomer = new IndividualCustomer
            {
                Id = userId,
                FirstName = individualCustomerForRegisterDto.FirstName,
                LastName = individualCustomerForRegisterDto.LastName,
                CompanyName = individualCustomerForRegisterDto.CompanyName
            };

            _individualCustomerService.Add(individualCustomer);
            return new SuccessDataResult<User>(user, Messages.RegistrationSuccessful);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck is null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyHash(userForLoginDto.Password, userToCheck.PasswordHash,
                userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.LoginSuccessful);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email).Data != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }

            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreatedSuccessfully);
        }

        private User UserAdd(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreateHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.Add(user);
            return user;
        }
    }
}
