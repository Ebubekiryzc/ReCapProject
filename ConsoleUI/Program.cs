using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instances:
            Brand brand = new Brand();
            Car car = new Car();
            CarImage carImage = new CarImage();
            Color color = new Color();
            IndividualCustomer individualCustomer = new IndividualCustomer();
            Rental rental = new Rental();
            User user = new User();

            // Operations:
            BrandOperations brandOperations;
            CarOperations carOperations;
            CarImageOperations carImageOperations;
            ColorOperations colorOperations;
            IndividualCustomerOperations individualCustomerOperations;

            //Dals:
            IBrandDal brandDal = new InMemoryBrandDal();
            ICarDal carDal = new InMemoryCarDal(null, null);
            ICarImageDal carImageDal = new InMemoryCarImageDal();
            IColorDal colorDal = new InMemoryColorDal();
            IIndividualCustomerDal individualCustomerDal = new InMemoryIndividualCustomerDal();
            IRentalDal rentalDal = new InMemoryRentalDal();
            IUserDal userDal = new InMemoryUserDal();

            // Main Operations:
            string operationList = "1- Brand\n" +
                                "2- Car\n" +
                                "3- Car Image\n" +
                                "4- Color\n" +
                                "5- IndividualCustomer\n" +
                                "6- Rental\n" +
                                "7- User\n" +
                                "8- Exit";
            // Brand Operations:
            string brandOperationList = "1- Create Brand\n" +
                                        "2- Update Brand\n" +
                                        "3- Delete Brand\n" +
                                        "4- Get All Brands\n" +
                                        "5- Get Brand By Id\n" +
                                        "6- Return to Top Menu";
            // Car Operations:
            string carOperationList = "1- Create Car\n" +
                                      "2- Update Car\n" +
                                      "3- Delete Car\n" +
                                      "4- Get All\n" +
                                      "5- Get Cars By Brand Id\n" +
                                      "6- Get Cars By Color Id\n" +
                                      "7- Get Car By Id\n" +
                                      "8- Get Cars With Detail\n" +
                                      "9- Return to Top Menu";
            // Car Image Operations:
            string carImageOperationList = "1- Create Car Image\n" +
                                           "2- Update Car Image\n" +
                                           "3- Delete Car Image\n" +
                                           "4- Get All\n" +
                                           "5- Get By Car Id\n" +
                                           "6- Get By Id\n" +
                                           "7- Return to Top Menu";
            // Color Operations: 
            string colorOperationList = "1- Create Color\n" +
                                        "2- Update Color\n" +
                                        "3- Delete Color\n" +
                                        "4- Get All\n" +
                                        "5- Get By Id\n" +
                                        "6- Return to Top Menu";
            // Individual Customer Operations:
            string individualCustomerOperationList = "1- Create Individual Customer\n" +
                                                     "2- Update Individual Customer\n" +
                                                     "3- Delete Individual Customer\n" +
                                                     "4- Get All\n" +
                                                     "5- Get By Id\n" +
                                                     "6- Return to Top Menu";
            // Rental Operations:
            string rentalOperationList = "1- Create Rental\n" +
                                         "2- Update Rental\n" +
                                         "3- Delete Rental\n" +
                                         "4- Get All\n" +
                                         "5- Get By Id\n" +
                                         "6- Get With Individual Customer Details\n" +
                                         "7- Get With Individual Customer Details By Rental Id\n" +
                                         "8- Return to Top Menu";
            // Conditions:
            bool condition;
            bool internalCondition;

            do
            {
                // Loop cycle:
                condition = true;
                internalCondition = true;

                Console.Clear();

                Console.WriteLine(operationList);
                Console.Write("Please enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        {
                            using (brandOperations = new BrandOperations(out brandDal))
                            {
                                do
                                {
                                    Console.WriteLine(brandOperationList);
                                    Console.Write("Please choose a function to run: ");
                                    choice = Convert.ToInt32(Console.ReadLine());

                                    switch (choice)
                                    {
                                        case 1:
                                            {
                                                brandOperations.AddToDatabase(brand);
                                                break;
                                            }
                                        case 2:
                                            {
                                                brandOperations.UpdateToDatabase(brand);
                                                break;
                                            }
                                        case 3:
                                            {
                                                brandOperations.DeleteFromDatabase(brand);
                                                break;
                                            }
                                        case 4:
                                            {
                                                brandOperations.GetAll();
                                                break;
                                            }
                                        case 5:
                                            {
                                                brandOperations.GetById();
                                                break;
                                            }
                                        case 6:
                                            {
                                                internalCondition = false;
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Please enter a valid choise!");
                                                break;
                                            }
                                    }
                                } while (internalCondition);
                            }
                            break;
                        }
                    case 2:
                        {
                            using (carOperations =
                                new CarOperations(out carDal, new List<Brand> { new Brand { Id = 1, Name = "Mercedes" }, new Brand { Id = 2, Name = "BMW" } }, new List<Color> { new Color { Id = 1, Name = "Yellow" }, new Color { Id = 2, Name = "Orange" } }))
                            {
                                do
                                {
                                    Console.WriteLine($"\n{carOperationList}");
                                    Console.Write("Please choose a function to run: ");
                                    choice = Convert.ToInt32(Console.ReadLine());

                                    switch (choice)
                                    {
                                        case 1:
                                            {
                                                carOperations.AddToDatabase(car);
                                                break;
                                            }
                                        case 2:
                                            {
                                                carOperations.UpdateToDatabase(car);
                                                break;
                                            }
                                        case 3:
                                            {
                                                carOperations.DeleteFromDatabase(car);
                                                break;
                                            }
                                        case 4:
                                            {
                                                carOperations.GetAll();
                                                break;
                                            }
                                        case 5:
                                            {
                                                carOperations.GetCarsByBrandId();
                                                break;
                                            }
                                        case 6:
                                            {
                                                carOperations.GetCarsByColorId();
                                                break;
                                            }
                                        case 7:
                                            {
                                                carOperations.GetById();
                                                break;
                                            }
                                        case 8:
                                            {
                                                carOperations.GetCarsWithDetails();
                                                break;
                                            }
                                        case 9:
                                            {
                                                internalCondition = false;
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Please enter a valid choise!");
                                                break;
                                            }
                                    }
                                } while (internalCondition);
                            }
                            break;
                        }
                    case 3:
                        {
                            using (carImageOperations = new CarImageOperations(out carImageDal))
                            {
                                do
                                {
                                    Console.WriteLine($"\n{carImageOperationList}");
                                    Console.Write("Please choose a function to run: ");
                                    choice = Convert.ToInt32(Console.ReadLine());

                                    switch (choice)
                                    {
                                        case 1:
                                            {
                                                carImageOperations.AddToDatabase(carImage);
                                                break;
                                            }
                                        case 2:
                                            {
                                                carImageOperations.UpdateToDatabase(carImage);
                                                break;
                                            }
                                        case 3:
                                            {
                                                carImageOperations.DeleteFromDatabase(carImage);
                                                break;
                                            }
                                        case 4:
                                            {
                                                carImageOperations.GetAll();
                                                break;
                                            }
                                        case 5:
                                            {
                                                carImageOperations.GetCarImagesByCarId();
                                                break;
                                            }
                                        case 6:
                                            {
                                                carImageOperations.GetById();
                                                break;
                                            }
                                        case 7:
                                            {
                                                internalCondition = false;
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Please enter a valid choise!");
                                                break;
                                            }
                                    }
                                } while (internalCondition);
                            }
                            break;
                        }
                    case 4:
                        {
                            using (colorOperations = new ColorOperations(out colorDal))
                            {
                                do
                                {
                                    Console.WriteLine($"\n{colorOperationList}");
                                    Console.Write("Please choose a function to run: ");
                                    choice = Convert.ToInt32(Console.ReadLine());

                                    switch (choice)
                                    {
                                        case 1:
                                            {
                                                colorOperations.AddToDatabase(color);
                                                break;
                                            }
                                        case 2:
                                            {
                                                colorOperations.UpdateToDatabase(color);
                                                break;
                                            }
                                        case 3:
                                            {
                                                colorOperations.DeleteFromDatabase(color);
                                                break;
                                            }
                                        case 4:
                                            {
                                                colorOperations.GetAll();
                                                break;
                                            }
                                        case 5:
                                            {
                                                colorOperations.GetById();
                                                break;
                                            }
                                        case 6:
                                            {
                                                internalCondition = false;
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Please enter a valid choise!");
                                                break;
                                            }
                                    }
                                } while (internalCondition);
                            }
                            break;
                        }
                    case 5:
                        {
                            using (individualCustomerOperations = new IndividualCustomerOperations(out individualCustomerDal))
                            {
                                do
                                {
                                    Console.WriteLine($"\n{individualCustomerOperationList}");
                                    Console.Write("Please choose a function to run: ");
                                    choice = Convert.ToInt32(Console.ReadLine());

                                    switch (choice)
                                    {
                                        case 1:
                                            {
                                                individualCustomerOperations.AddToDatabase(individualCustomer);
                                                break;
                                            }
                                        case 2:
                                            {
                                                individualCustomerOperations.UpdateToDatabase(individualCustomer);
                                                break;
                                            }
                                        case 3:
                                            {
                                                individualCustomerOperations.DeleteFromDatabase(individualCustomer);
                                                break;
                                            }
                                        case 4:
                                            {
                                                individualCustomerOperations.GetAll();
                                                break;
                                            }
                                        case 5:
                                            {
                                                individualCustomerOperations.GetById();
                                                break;
                                            }
                                        case 6:
                                            {
                                                internalCondition = false;
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Please enter a valid choise!");
                                                break;
                                            }
                                    }
                                } while (internalCondition);
                            }
                            break;
                        }
                    case 8:
                        {
                            condition = false;
                            Console.WriteLine("Good bye!");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Please enter a valid choise!");
                            break;
                        }
                }
            } while (condition);

        }
    }
}