using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), "All users listed.");
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id), "User listed by id.");
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult("User added successfully.");
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult("User update successfully.");
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult("User deleted successfully.");
        }
    }
}
