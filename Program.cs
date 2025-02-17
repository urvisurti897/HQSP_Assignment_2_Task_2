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

    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
        }
    }
}
