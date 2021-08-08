using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalsListed);
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id), Messages.RentalListedById);
        }

        public IResult Add(Rental rental)
        {
            if (!IsCarAvailable(rental))
            {
                return new ErrorResult(Messages.OperationFailed);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Update(Rental rental)
        {
            if (!IsCarAvailable(rental))
            {
                return new ErrorResult(Messages.OperationFailed);
            }
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public bool IsCarAvailable(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId);

            if (result.Any(r =>
                r.ReturnDate >= rental.RentDate &&
                r.RentDate <= rental.ReturnDate
            )) return false;

            return true;
        }
    }
}
