using System.Text.RegularExpressions;

namespace HQSP_Assignment_2_Task_2
{
    //Defining the interface
    interface ICustomer
    {
        //Creating the properties
        string Name { get; set; } //Name of the customer
        string BuildingType { get; set; } //Type of building
        int Size { get; set; } //Size of the building
        int LightBulbs { get; set; } //Number of light bulbs
        int Outlets { get; set; } //Number of outlets
        string CreditCard { get; set; } //Credit card number


        //Creating method to create wiring schema
        void CreateWiringSchema();

        //Creating method to purchase parts
        void PurchaseParts();

        //Creating method for specific task
        void SpecificTask();

        //Creating method to display information
        void DisplayInfo();
    }

    //Creating the abstract class
    abstract class CustomerBase : ICustomer
    {
        //Creating the properties
        private string name; //Name of the customer
        private string buildingType; //Type of building
        private int size; //Size of the building
        private int lightBulbs; //Number of light bulbs
        private int outlets; //Number of outlets
        private string creditCard; //Credit card number

        //Creating property for name with get and set accessors
        public string Name
        {
            //Get accessor returns the value of the name field
            get => name;

            //Set accessor assigns a value to the name field
            set
            {
                //Validating the name field and throwing an exception if the name field is invalid
                if (!IsValidInput(value, @"^[A-Za-z]+$"))
                    throw new ArgumentException("Name should contain only letters.");

                //Assigning the value to the name field if it passes the validation
                name = value;
            }
        }

        //Creating property for buildingType with get and set accessors
        public string BuildingType
        {
            //Get accessor returns the value of the buildingType field
            get => buildingType;

            //Set accessor assigns a value to the buildingType field
            set
            {
                //Validating the buildingType field and throwing an exception if the buildingType field is invalid
                if (!IsValidInput(value, @"^(house|barn|garage)$"))
                    throw new ArgumentException("Building type should be either 'house' or 'barn' or 'garage'.");

                //Assigning the value to the buildingType field if it passes the validation
                buildingType = value;
            }
        }


        //Creating property for size with get and set accessors
        public int Size
        {
            //Get accessor returns the value of the size field
            get => size;

            //Set accessor assigns a value to the size field
            set
            {
                //Validating the size field and throwing an exception if the size field is invalid
                if (value < 1000 || value > 50000)
                    throw new ArgumentException("Size should range from 1000 sq. ft. to 50000 sq. ft.");

                //Assigning the value to the size field if it passes the validation
                size = value;
            }
        }


        //Creating property for lightBulbs with get and set accessors
        public int LightBulbs
        {
            //Get accessor returns the value of the lightBulbs field
            get => lightBulbs;

            //Set accessor assigns a value to the lightBulbs field
            set
            {
                //Validating the lightBulbs field and throwing an exception if the lightBulbs field is invalid
                if (value < 1 || value > 20)
                    throw new ArgumentException("Light bulbs should range from 1 to 20.");

                //Assigning the value to the lightBulbs field if it passes the validation
                lightBulbs = value;
            }
        }

        //Creating property for outlets with get and set accessors
        public int Outlets
        {
            //Get accessor returns the value of the outlets field
            get => outlets;

            //Set accessor assigns a value to the outlets field
            set
            {
                //Validating the outlets field and throwing an exception if the outlets field is invalid
                if (value < 1 || value > 50)
                    throw new ArgumentException("Outlets should range from 1 to 50.");

                //Assigning the value to the outlets field if it passes the validation
                outlets = value;
            }
        }


        //Creating property for creditCard with get and set accessors
        public string CreditCard
        {
            //Get accessor returns the value of the creditCard field
            get => creditCard;

            //Set accessor assigns a value to the creditCard field
            set
            {
                //Validating the creditCard field and throwing an exception if the creditCard field is invalid
                if (!IsValidInput(value, @"^\d{16}$"))
                    throw new ArgumentException("Credit card should have exact 16 digits.");

                //Assigning the value to the creditCard field if it passes the validation
                creditCard = value;
            }
        }

        //Creating method to create wiring schema
        public void CreateWiringSchema()
        {
            //Displaying the message
            Console.WriteLine($"{Name}: Creating wiring schema for {BuildingType}.");
        }

        //Creating method to purchase parts
        public void PurchaseParts()
        {
            //Displaying the message
            Console.WriteLine($"{Name}: Purchasing necessary parts for {BuildingType}.");
        }

        //Creating abstract method for specific task to be implemented by the derived classes
        public abstract void SpecificTask();

        //Creating method to display information
        public void DisplayInfo()
        {
            //Masking the credit card number
            string maskedCreditCard = $"{CreditCard.Substring(0, 4)} XXXX XXXX {CreditCard.Substring(12, 4)}";

            //Displaying the information
            Console.WriteLine($"{Name}, {BuildingType}, {Size} sq. ft., {LightBulbs} bulbs, {Outlets} outlets, {maskedCreditCard}");
        }

        //Creating method to validate the input
        private bool IsValidInput(string input, string pattern)
        {
            //Returning true if the input matches the pattern, otherwise returning false
            return Regex.IsMatch(input, pattern);
        }
    }

    //Creating the derived class HouseCustomer
    class HouseCustomer : CustomerBase
    {
        //Implementing the abstract method SpecificTask
        public override void SpecificTask()
        {
            //Displaying the message
            Console.WriteLine($"{Name}: Installing fire alarms.");
        }
    }

    //Creating the derived class BarnCustomer
    class BarnCustomer : CustomerBase
    {
        //Implementing the abstract method SpecificTask
        public override void SpecificTask()
        {
            //Displaying the message
            Console.WriteLine($"{Name}: Wiring milking equipment.");
        }
    }

    //Creating the derived class GarageCustomer
    class GarageCustomer : CustomerBase
    {
        //Implementing the abstract method SpecificTask
        public override void SpecificTask()
        {
            //Displaying the message
            Console.WriteLine($"{Name}: Installing automatic doors.");
        }
    }
    //Main Program Class
    internal class Program
    {
        //Implementing the Main method
        static void Main(string[] args)
        {
            //Creating a list of ICustomer
            List<ICustomer> customers = new List<ICustomer>();

            //Creating a string variable input
            string input;

            //Creating a do-while loop to add customers
            do
            {
                //Creating a customer and adding it to the list
                ICustomer customer = CreateCustomer();

                //Adding the customer to the list
                customers.Add(customer);

                //Asking the user if they want to add another customer
                Console.Write("\nWould you like to add another customer? (yes/no): ");

                //Reading the input from the user
                input = Console.ReadLine().ToLower();
            }
            //Looping until the user enters 'no'
            while (input == "yes");

            //Displaying the customer information
            Console.WriteLine("\nCustomer Information:");
            Console.WriteLine("************************\n");

            //Iterating through the customers and displaying the information
            foreach (var customer in customers)
            {
                //Calling the methods to create wiring schema
                customer.CreateWiringSchema();

                //Calling the methods to purchase parts
                customer.PurchaseParts();

                //Calling the methods to perform specific task
                customer.SpecificTask();

                //Calling the method to display information
                customer.DisplayInfo();
                Console.WriteLine("---------------------------------------------------------------------------");
            }

            //Creating a method to create a customer
            static ICustomer CreateCustomer()
            {
                //Getting the customer name
                string name = GetValidatedInput("Enter customer name : ",
                    @"^[A-Za-z]+$", "Invalid Input.Name should contain only letters.");

                //Getting the building type
                string buildingType = GetValidatedInput("Enter building type (house or barn or garage): ",
                    @"^(house|barn|garage)$", "Invalid Input.Building type should be either 'house' or 'barn' or 'garage'.");

                //Getting the size of the building
                int size = int.Parse(GetValidatedInput("Enter size (1000 sq.ft. to 50000 sq. ft.): ",
                    @"^\d{4,5}$", "Invalid Input.Size should range from 1000 and 50000 sq. ft."));

                //Getting the number of light bulbs
                int lightBulbs = int.Parse(GetValidatedInput("Enter number of light bulbs (1 - 20): ",
                    @"^([1-9]|1[0-9]|20)$", "Invalid Input.Light bulbs must be between 1 and 20."));

                //Getting the number of outlets
                int outlets = int.Parse(GetValidatedInput("Enter number of outlets (1 - 50): ",
                    @"^([1-9]|[1-4][0-9]|50)$", "Invalid Input.Outlets must be between 1 and 50."));

                //Getting the credit card number
                string creditCard = GetValidatedInput("Enter 16 digit credit card number : ",
                    @"^\d{16}$", "Invalid Input.Credit card should have exact 16 digits.");

                //Creating the customer based on the building type
                ICustomer customer = buildingType switch
                {
                    "house" => new HouseCustomer(),
                    "barn" => new BarnCustomer(),
                    "garage" => new GarageCustomer(),
                    _ => throw new ArgumentException("Invalid building type")
                };

                //Assigning the values to the customer name property
                customer.Name = name;

                //Assigning the values to the customer building type property
                customer.BuildingType = buildingType;

                //Assigning the values to the customer size property
                customer.Size = size;

                //Assigning the values to the customer light bulbs property
                customer.LightBulbs = lightBulbs;

                //Assigning the values to the customer outlets property
                customer.Outlets = outlets;

                //Assigning the values to the customer credit card property
                customer.CreditCard = creditCard;

                //Returning the customer
                return customer;
            }

            //Creating a method to get validated input
            static string GetValidatedInput(string prompt, string pattern, string errorMessage)
            {
                //Creating a string variable input
                string input;

                //Creating a do-while loop to get the validated input
                do
                {
                    //Displaying the prompt
                    Console.Write(prompt);

                    //Reading the input from the user
                    input = Console.ReadLine().Trim();

                    //Validating the input and displaying the error message if the input is invalid
                    if (!IsValidInput(input, pattern))
                    {
                        //Displaying the error message
                        Console.WriteLine(errorMessage);
                    }
                }
                //Looping until the input is valid
                while (!IsValidInput(input, pattern));

                //Returning the input
                return input;
            }

            //Creating a method to validate the input
            static bool IsValidInput(string input, string pattern)
            {
                //Returning true if the input matches the pattern, otherwise returning false
                return Regex.IsMatch(input, pattern);
            }
        }
    }
}
