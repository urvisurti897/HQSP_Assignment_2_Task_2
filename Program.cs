using System.Text.RegularExpressions;

namespace HQSP_Assignment_2_Task_2
{
    interface ICustomer
    {
        string Name { get; set; }
        string BuildingType { get; set; }
        int Size { get; set; }
        int LightBulbs { get; set; }
        int Outlets { get; set; }
        string CreditCard { get; set; }

        void CreateWiringSchema();
        void PurchaseParts();
        void SpecificTask();
        void DisplayInfo();
    }

    abstract class CustomerBase : ICustomer
    {
        private string name;
        private string buildingType;
        private int size;
        private int lightBulbs;
        private int outlets;
        private string creditCard;

        public string Name
        {
            get => name;
            set
            {
                if (!IsValidInput(value, @"^[A-Za-z]+$"))
                    throw new ArgumentException("Name should contain only letters.");
                name = value;
            }
        }

        public string BuildingType
        {
            get => buildingType;
            set
            {
                if (!IsValidInput(value, @"^(house|barn|garage)$"))
                    throw new ArgumentException("Building type should be either 'house' or 'barn' or 'garage'.");
                buildingType = value;
            }
        }

        public int Size
        {
            get => size;
            set
            {
                if (value < 1000 || value > 50000)
                    throw new ArgumentException("Size should range from 1000 sq. ft. to 50000 sq. ft.");
                size = value;
            }
        }

        public int LightBulbs
        {
            get => lightBulbs;
            set
            {
                if (value < 1 || value > 20)
                    throw new ArgumentException("Light bulbs should range from 1 to 20.");
                lightBulbs = value;
            }
        }

        public int Outlets
        {
            get => outlets;
            set
            {
                if (value < 1 || value > 50)
                    throw new ArgumentException("Outlets should range from 1 to 50.");
                outlets = value;
            }
        }

        public string CreditCard
        {
            get => creditCard;
            set
            {
                if (!IsValidInput(value, @"^\d{16}$"))
                    throw new ArgumentException("Credit card should have exact 16 digits.");
                creditCard = value;
            }
        }

        public void CreateWiringSchema()
        {
            Console.WriteLine($"{Name}: Creating wiring schema for {BuildingType}.");
        }

        public void PurchaseParts()
        {
            Console.WriteLine($"{Name}: Purchasing necessary parts for {BuildingType}.");
        }

        public abstract void SpecificTask();

        public void DisplayInfo()
        {
            string maskedCreditCard = $"{CreditCard.Substring(0, 4)} XXXX XXXX {CreditCard.Substring(12, 4)}";
            Console.WriteLine($"{Name}, {BuildingType}, {Size} sq. ft., {LightBulbs} bulbs, {Outlets} outlets, {maskedCreditCard}");
        }

        private bool IsValidInput(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }
    }

    class HouseCustomer : CustomerBase
    {
        public override void SpecificTask()
        {
            Console.WriteLine($"{Name}: Installing fire alarms.");
        }
    }

    class BarnCustomer : CustomerBase
    {
        public override void SpecificTask()
        {
            Console.WriteLine($"{Name}: Wiring milking equipment.");
        }
    }

    class GarageCustomer : CustomerBase
    {
        public override void SpecificTask()
        {
            Console.WriteLine($"{Name}: Installing automatic doors.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<ICustomer> customers = new List<ICustomer>();
            string input;

            do
            {
                ICustomer customer = CreateCustomer();
                customers.Add(customer);
                Console.Write("\nWould you like to add another customer? (yes/no): ");
                input = Console.ReadLine().ToLower();
            } while (input == "yes");

            Console.WriteLine("\nCustomer Information:");
            Console.WriteLine("************************\n");

            foreach (var customer in customers)
            {
                customer.CreateWiringSchema();
                customer.PurchaseParts();
                customer.SpecificTask();
                customer.DisplayInfo();
                Console.WriteLine("---------------------------------------------------------------------------");
            }

            static ICustomer CreateCustomer()
            {
                string name = GetValidatedInput("Enter customer name : ",
                    @"^[A-Za-z]+$", "Invalid Input.Name should contain only letters.");

                string buildingType = GetValidatedInput("Enter building type (house or barn or garage): ",
                    @"^(house|barn|garage)$", "Invalid Input.Building type should be either 'house' or 'barn' or 'garage'.");

                int size = int.Parse(GetValidatedInput("Enter size (1000 sq.ft. to 50000 sq. ft.): ",
                    @"^\d{4,5}$", "Invalid Input.Size should range from 1000 and 50000 sq. ft."));

                int lightBulbs = int.Parse(GetValidatedInput("Enter number of light bulbs (1 - 20): ",
                    @"^([1-9]|1[0-9]|20)$", "Invalid Input.Light bulbs must be between 1 and 20."));

                int outlets = int.Parse(GetValidatedInput("Enter number of outlets (1 - 50): ",
                    @"^([1-9]|[1-4][0-9]|50)$", "Invalid Input.Outlets must be between 1 and 50."));

                string creditCard = GetValidatedInput("Enter 16 digit credit card number : ",
                    @"^\d{16}$", "Invalid Input.Credit card should have exact 16 digits.");

                ICustomer customer = buildingType switch
                {
                    "house" => new HouseCustomer(),
                    "barn" => new BarnCustomer(),
                    "garage" => new GarageCustomer(),
                    _ => throw new ArgumentException("Invalid building type")
                };

                customer.Name = name;
                customer.BuildingType = buildingType;
                customer.Size = size;
                customer.LightBulbs = lightBulbs;
                customer.Outlets = outlets;
                customer.CreditCard = creditCard;

                return customer;
            }

            static string GetValidatedInput(string prompt, string pattern, string errorMessage)
            {
                string input;
                do
                {
                    Console.Write(prompt);
                    input = Console.ReadLine().Trim();
                    if (!IsValidInput(input, pattern))
                    {
                        Console.WriteLine(errorMessage);
                    }
                } while (!IsValidInput(input, pattern));

                return input;
            }
        }
    }
}
