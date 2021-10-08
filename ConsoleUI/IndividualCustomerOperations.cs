using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Linq;

namespace ConsoleUI
{
    public class IndividualCustomerOperations : IDisposable
    {
        private IIndividualCustomerDal _individualCustomerDal;
        private IIndividualCustomerService _individualCustomerManager;

        public IndividualCustomerOperations(out IIndividualCustomerDal individualCustomerDal)
        {
            Console.Write(
                "Which dal do you want to use? (1- EfIndividualCustomerDal, 2- InMemoryIndividualCustomerDal): ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    {
                        individualCustomerDal = new EfIndividualCustomerDal();
                        break;
                    }
                case 2:
                    {
                        individualCustomerDal = new InMemoryIndividualCustomerDal();
                        break;
                    }
                default:
                    {
                        individualCustomerDal = new EfIndividualCustomerDal();
                        break;
                    }
            }

            _individualCustomerDal = individualCustomerDal;
            _individualCustomerManager = new IndividualCustomerManager(_individualCustomerDal);
        }

        public void GetAll()
        {
            var result = _individualCustomerManager.GetAll();
            Console.WriteLine(result.Message);
            foreach (var individualCustomerDal in result.Data)
            {
                Console.WriteLine("Id: {0}, FirstName: {1}, LastName: {2}, CompanyName: {3}", individualCustomerDal.Id,
                    individualCustomerDal.FirstName, individualCustomerDal.LastName, individualCustomerDal.CompanyName);
            }
        }

        public void GetById()
        {
            Console.Write("Please enter the id of individual customer you want to get: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var result = _individualCustomerManager.GetById(id).Data;
            Console.WriteLine("Id: {0}, FirstName: {1}, LastName: {2}, CompanyName: {3}", result.Id, result.FirstName,
                result.LastName, result.CompanyName);
        }

        public void AddToDatabase(IndividualCustomer individualCustomer)
        {
            Console.WriteLine(
                $"{_individualCustomerManager.Add(CreateIndividualCustomer(individualCustomer)).Message}\n");
        }

        public void UpdateToDatabase(IndividualCustomer individualCustomer)
        {
            Console.WriteLine(
                $"{_individualCustomerManager.Update(UpdateIndividualCustomer(individualCustomer)).Message}\n");
        }

        public void DeleteFromDatabase(IndividualCustomer individualCustomer)
        {
            Console.WriteLine(
                $"{_individualCustomerManager.Delete(DeleteIndividualCustomer(individualCustomer)).Message}\n");
        }

        private IndividualCustomer UpdateIndividualCustomer(IndividualCustomer individualCustomer)
        {
            var result = CheckIndividualCustomerExists(individualCustomer);
            if (result)
            {
                Console.WriteLine("\nFor new entity: ");
                EnterBodyInfo(individualCustomer);
            }

            return individualCustomer;
        }

        private IndividualCustomer DeleteIndividualCustomer(IndividualCustomer individualCustomer)
        {
            var result = CheckIndividualCustomerExists(individualCustomer);
            if (!result)
            {
                individualCustomer.Id = -1;
            }

            return individualCustomer;
        }

        private bool CheckIndividualCustomerExists(IndividualCustomer individualCustomer)
        {
            EnterIdentityInfo(individualCustomer);
            EnterBodyInfo(individualCustomer);
            bool checkBrandExists = _individualCustomerManager.GetAll().Data.Any(c =>
                c.Id == individualCustomer.Id && c.FirstName == individualCustomer.FirstName &&
                c.LastName == individualCustomer.LastName && c.CompanyName == individualCustomer.CompanyName);
            if (!checkBrandExists)
            {
                Console.WriteLine("There is no customer matcing that instance.");
                individualCustomer.Id = -1;
            }

            return checkBrandExists;
        }

        private void EnterBodyInfo(IndividualCustomer individualCustomer)
        {
            Console.Write("Please enter the first name for your customer: ");
            individualCustomer.FirstName = Console.ReadLine();
            Console.Write("Please enter the last name for your customer: ");
            individualCustomer.LastName = Console.ReadLine();
            Console.Write("Please enter the company name for your customer: ");
            individualCustomer.CompanyName = Console.ReadLine();
        }

        private void EnterIdentityInfo(IndividualCustomer individualCustomer)
        {
            Console.Write("Please enter the id of the customer you want to affect: ");
            int id = Convert.ToInt32(Console.ReadLine());
            individualCustomer.Id = id;
        }

        private IndividualCustomer CreateIndividualCustomer(IndividualCustomer individualCustomer)
        {
            individualCustomer = new IndividualCustomer();
            EnterBodyInfo(individualCustomer);
            return individualCustomer;
        }

        public void Dispose()
        {
            _individualCustomerManager = null;
            _individualCustomerDal = null;
        }
    }
}