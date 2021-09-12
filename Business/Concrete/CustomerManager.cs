using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IDataResult<List<IndividualCustomer>> GetAll()
        {
            return new SuccessDataResult<List<IndividualCustomer>>(_customerDal.GetAll(), Messages.CustomersListed);
        }

        public IDataResult<IndividualCustomer> GetById(int id)
        {
            return new SuccessDataResult<IndividualCustomer>(_customerDal.Get(c => c.Id == id), Messages.CustomerListed);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(IndividualCustomer individualCustomer)
        {
            _customerDal.Add(individualCustomer);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(IndividualCustomer individualCustomer)
        {
            _customerDal.Update(individualCustomer);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Delete(IndividualCustomer individualCustomer)
        {
            _customerDal.Delete(individualCustomer);
            return new SuccessResult(Messages.OperationSuccessful);
        }
    }
}
