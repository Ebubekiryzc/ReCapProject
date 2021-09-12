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
    public class IndividualCustomerManager : IIndividualCustomerService
    {
        private IIndividualCustomerDal _individualCustomerDal;

        public IndividualCustomerManager(IIndividualCustomerDal individualCustomerDal)
        {
            _individualCustomerDal = individualCustomerDal;
        }

        public IDataResult<List<IndividualCustomer>> GetAll()
        {
            return new SuccessDataResult<List<IndividualCustomer>>(_individualCustomerDal.GetAll(), Messages.CustomersListed);
        }

        public IDataResult<IndividualCustomer> GetById(int id)
        {
            return new SuccessDataResult<IndividualCustomer>(_individualCustomerDal.Get(c => c.Id == id), Messages.CustomerListed);
        }

        [ValidationAspect(typeof(IndividualCustomerValidator))]
        public IResult Add(IndividualCustomer individualCustomer)
        {
            _individualCustomerDal.Add(individualCustomer);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        [ValidationAspect(typeof(IndividualCustomerValidator))]
        public IResult Update(IndividualCustomer individualCustomer)
        {
            _individualCustomerDal.Update(individualCustomer);
            return new SuccessResult(Messages.OperationSuccessful);
        }

        public IResult Delete(IndividualCustomer individualCustomer)
        {
            _individualCustomerDal.Delete(individualCustomer);
            return new SuccessResult(Messages.OperationSuccessful);
        }
    }
}
