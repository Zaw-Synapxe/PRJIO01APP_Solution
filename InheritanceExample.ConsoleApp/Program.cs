namespace InheritanceExample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Product desk = new Desk();

            ((Desk)desk).ImportTaxPercentage = 2;

            desk.Price = 100;

            desk.Add();
            desk.Add();

            Product droneTurbo = new TurboDrone();

            droneTurbo.Price = 200;

            ((Drone)droneTurbo).QuantityIncremented = 10;

            droneTurbo.Add();
            droneTurbo.Add();

            Product droneStandard = new StandardDrone();

            droneStandard.Price = 150;

            ((Drone)droneStandard).QuantityIncremented = 5;

            droneStandard.Add();
            droneStandard.Add();
            droneStandard.Add();

            List<Product> products = new List<Product>();

            products.Add(desk);
            products.Add(droneStandard);
            products.Add(droneTurbo);

            Console.WriteLine("Stock Report");
            Console.WriteLine("------------");
            Console.WriteLine();

            foreach (Product product in products)
            {
                Console.WriteLine(product);

            }

            decimal grandTotalStockValue = products.Sum(p => p.GetTotalValueInStock());

            Console.WriteLine();

            Console.WriteLine($"Grand total value of all products in stock is: {grandTotalStockValue}");


            Console.ReadKey();
        }



    }



    public class TurboDrone : Drone
    {
        public override string ProductName
        {
            get
            {
                return "Turbo Drone";
            }
        }
    }

    public class StandardDrone : Drone
    {
        public override string ProductName
        {
            get
            {
                return "Standard Drone";
            }
        }
    }

    public class Drone : Product
    {
        public int QuantityIncremented { get; set; }

        public override string ProductName
        {
            get
            {
                return "Drone";
            }
        }
        public Drone()
        {
            QuantityIncremented = 1;
        }

        public override void Add()
        {
            _quantity = _quantity + QuantityIncremented;
        }

    }





    public class Desk : Product
    {
        public decimal ImportTaxPercentage { get; set; }

        public override string ProductName
        {
            get
            {
                return "Desk";
            }
        }

        public Desk()
        {

        }
        public override decimal GetTotalValueInStock()
        {
            decimal netTotal = base.GetTotalValueInStock() - (base.GetTotalValueInStock() * (ImportTaxPercentage / 100));

            return netTotal;
        }
    }

    public abstract class Product
    {
        protected int _quantity = 0;
        public decimal Price { get; set; }
        public abstract string ProductName { get; }

        public Product()
        {

        }


        public virtual void Add()
        {
            _quantity++;
        }

        public void Remove()
        {
            if (_quantity > 0)
                _quantity--;
        }

        public virtual decimal GetTotalValueInStock()
        {
            return _quantity * Price;
        }
        public override string ToString()
        {
            return $"Product Name: {ProductName}, Price: {Price}, Quantity: {_quantity}, Total Value: {GetTotalValueInStock()}";
        }
    }

}