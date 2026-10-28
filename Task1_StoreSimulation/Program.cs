using System.Text;

struct Purchase
{
    public string ProductName;
    public int Price;

    public Purchase(string productName, int price)
    {
        ProductName = productName;
        Price = price;
    }
}

class Product
{
    public string Name;
    public int Price;
    public int Quantity;

    public Product(string name, int price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}

class Customer
{
    public int CardNumber;
    public int Money;
    public int SpentMoney;
    public List<Purchase> Purchases = new List<Purchase>();

    public Customer(int cardNumber, int money)
    {
        CardNumber = cardNumber;
        Money = money;
        SpentMoney = 0;
    }

    public void BuyProduct(Product product)
    {
        Money -= product.Price;
        SpentMoney += product.Price;
        product.Quantity--;

        Purchases.Add(new Purchase(product.Name, product.Price));
    }
}

class Store
{
    public List<Product> Products = new List<Product>();
    public int Profit = 0;

    public Store()
    {
        Products.Add(new Product("Хліб", 40, 40));
        Products.Add(new Product("Молоко", 55, 30));
        Products.Add(new Product("Сир", 180, 20));
        Products.Add(new Product("Шоколад", 70, 40));
        Products.Add(new Product("Кава", 250, 20));
        Products.Add(new Product("Сік", 85, 30));
        Products.Add(new Product("Печиво", 65, 40));
    }

    public Product? GetRandomAvailableProduct(Customer customer, Random rnd)
    {
        var availableProducts = Products
            .Where(product => product.Quantity > 0 && customer.Money >= product.Price)
            .ToList();

        return availableProducts.Count == 0
            ? null
            : availableProducts[rnd.Next(availableProducts.Count)];
    }

    public void SellProduct(Customer customer, Product product)
    {
        customer.BuyProduct(product);
        Profit += product.Price;
    }
}

class Program
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        Random rnd = new Random();

        Console.Write("Введіть кількість покупців: ");

        int customerCount;

        while (!int.TryParse(Console.ReadLine(), out customerCount) || customerCount <= 0)
        {
            Console.Write("Введіть правильну кількість покупців: ");
        }

        Store store = new Store();
        List<Customer> customers = new List<Customer>();

        for (int i = 0; i < customerCount; i++)
        {
            int cardNumber = rnd.Next(1, 1001);
            int money = rnd.Next(1000, 10001);

            customers.Add(new Customer(cardNumber, money));
        }

        Console.WriteLine("\nПокупці:");

        foreach (Customer customer in customers)
        {
            Console.WriteLine(
                $"Картка: {customer.CardNumber}, гроші: {customer.Money} грн");
        }

        foreach (Customer customer in customers)
        {
            Console.WriteLine($"\nПокупець з карткою {customer.CardNumber} почав покупки.");

            while (store.GetRandomAvailableProduct(customer, rnd) is Product product)
            {
                store.SellProduct(customer, product);

                Console.WriteLine(
                    $"Куплено: {product.Name} за {product.Price} грн. " +
                    $"Залишилось грошей: {customer.Money} грн.");
            }
        }

        Customer maxProductsCustomer = customers[0];
        Customer maxSpentCustomer = customers[0];

        foreach (Customer customer in customers)
        {
            if (customer.Purchases.Count > maxProductsCustomer.Purchases.Count)
            {
                maxProductsCustomer = customer;
            }

            if (customer.SpentMoney > maxSpentCustomer.SpentMoney)
            {
                maxSpentCustomer = customer;
            }
        }

        Console.WriteLine("\n-----------------------------");
        Console.WriteLine($"Прибуток магазину: {store.Profit} грн");

        Console.WriteLine(
            $"Найбільше товарів купив покупець з карткою " +
            $"{maxProductsCustomer.CardNumber} - " +
            $"{maxProductsCustomer.Purchases.Count} товарів.");

        Console.WriteLine(
            $"Найбільше грошей витратив покупець з карткою " +
            $"{maxSpentCustomer.CardNumber} - " +
            $"{maxSpentCustomer.SpentMoney} грн.");

        Console.WriteLine("\nЗалишок товарів у магазині:");

        foreach (Product product in store.Products)
        {
            Console.WriteLine(
                $"{product.Name}: {product.Quantity} шт.");
        }
    }
}
