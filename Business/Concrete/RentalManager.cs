using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using Entities.DTOs;

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
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id), Messages.RentalListed);
        }

        public IDataResult<List<RentalDetailsForIndividualCustomers>> GetAllRentalsWithIndividualCustomerDetails()
        {
            return new SuccessDataResult<List<RentalDetailsForIndividualCustomers>>(
                _rentalDal.GetAllRentalsWithIndividualCustomerDetails(), Messages.RentalsListed);
        }

        public IDataResult<RentalDetailsForIndividualCustomers> GetRentalDetailsForIndividualCustomersByRentalId(int id)
        {
            return new SuccessDataResult<RentalDetailsForIndividualCustomers>(_rentalDal.GetRentalDetailsForIndividualCustomersByRentalId(id), Messages.RentalListed)
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Check(IsCarAvailable(rental));

            if (result is ErrorResult)
            {
                return result;
            }

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            var result = BusinessRules.Check(IsCarAvailable(rental));

            if (result is ErrorResult)
            {
                return result;
            }

            _rentalDal.Update(rental);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        private IResult IsCarAvailable(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId);

            if (result.Any(r =>
                r.ReturnDate >= rental.RentDate &&
                r.RentDate <= rental.ReturnDate
            )) return new ErrorResult(Messages.DateInvalid);

            return new SuccessResult(Messages.OperationSuccessful);
        }
    }
}
