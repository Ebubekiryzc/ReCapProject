using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = (from r in context.Rentals
                              where r.CarId == rental.CarId
                              orderby r.ReturnDate
                              select r).FirstOrDefault();
                if (CheckNull(result) || CheckRentDate(result))
                {
                    return false;
                }

                return true;
            }
        }

        public bool CheckNull(Rental rental)
        {
            return rental.ReturnDate == null;
        }

        public bool CheckRentDate(Rental rental)
        {
            return rental.RentDate < DateTime.Now;
        }
    }
}
