using System;
using System.Threading;

public class GUI
{
    private Administrator admin;
    private User user;
    private VendingMachine machine;
    private Factory factory;
    private bool isClosed = false;
    private enum Menu
    {
        Start,
        Admin,
        User,
    };
    private Menu menu = Menu.Start;

    public bool IsClosed
    {
        get { return isClosed; }
    }

    public GUI(int width, int height)
    {
        Console.SetBufferSize(width, height);

        admin = new Administrator(1000);
        user = new User(60);
        machine = new VendingMachine(15, 15);
        factory = new Factory(0);

        machine.AddProduct(factory.BuyFromFactory(new Product("Cola", 1000, 20.00), null, 12));
        machine.AddProduct(factory.BuyFromFactory(new Product("Chips", 1010, 25.00), null, 14));
        machine.AddProduct(factory.BuyFromFactory(new Product("Chips", 1020, 25.00), null, 14));
        machine.AddProduct(factory.BuyFromFactory(new Product("Energy Drink", 1030, 28.00), null, 7));
        machine.AddProduct(factory.BuyFromFactory(new Product("Chocolate Bar", 1040, 16.00), null, 13));
    }

    //  Special print functions ********************************************
    private void Print(int x, int y, int xOff, int yOff, string message)
    {
        Console.SetCursorPosition((x * 20) + xOff, (y * 8) + yOff);
        Console.Write(message);
    }
    private void PrintMachine()
    {
        int amount = machine.ProductAmount.Count;
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int i = y * 4 + x;

                if (i < amount)
                {
                    Print(x, y, 0, 1, "- " + machine.Products[i].Name);
                    Print(x, y, 0, 2, "ID     " + machine.Products[i].ItemID.ToString());
                    Print(x, y, 0, 3, "DKK    " + machine.Products[i].Price.ToString());
                    Print(x, y, 0, 4, "AMOUNT " + machine.ProductAmount[i].ToString());
                }
                for (int l = 0; l < 8; l++)
                    Print(x, y, 19, l, "│");

                for (int l = 0; l < 20; l++)
                    Print(x, y, l, 0, "─");
            }
        }
        Console.SetCursorPosition(0, 5 * 8);
    }

    // Input lines **********************************************************
    private string PrintStringInputBox(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
    private int PrintIntInputBox(string message)
    {
        while (true)
        {
            Console.Write(message);

            try
            {
                int value = int.Parse(Console.ReadLine());
                return value;
            }
            catch
            {
                Console.WriteLine("Failed...");
            }
        }
    }
    private double PrintDoubleInputBox(string message)
    {
        while (true)
        {
            Console.Write(message);
            try
            {
                double value = double.Parse(Console.ReadLine());
                return value;
            }
            catch
            {
                Console.WriteLine("Failed...");
            }
        }
    }

    // Start menu **********************************************************
    private void PrintStartMenu()
    {
        Console.WriteLine("- Start Menu");
        Console.WriteLine("0. User Menu");
        Console.WriteLine("1. Admin Menu");
        Console.WriteLine("2. Exit");

        int choice = PrintIntInputBox("Enter choice: ");
        
        switch (choice)
        {
        case 0:
            menu = Menu.User;
            return;
        case 1:
            menu = Menu.Admin;
            return;
        case 2:
            isClosed = true;
            return;
        default:
            Console.WriteLine("Failed...");
            break;
        }
        Console.ReadKey();
    }

    // Admin menu **********************************************************
    private void PrintRefillProduct()
    {
        int id = PrintIntInputBox("Enter product id: ");
        int amount = PrintIntInputBox("Enter product amount: ");
        try
        {
            if (machine.CheckMachineRoom(id, amount) != null)
            {
                Console.WriteLine(machine.CheckMachineRoom(id, amount));
                return;
            }

            int index = machine.FindProductIndexFromID(id);
            FactoryPack pack = factory.BuyFromFactory(machine.Products[index], admin.Money, amount);
            Console.WriteLine(machine.RefillProduct(pack));
        }
        catch
        {
            Console.WriteLine("Failed...");
        }
    }
    private void PrintChangeProductPrice()
    {
        int id = PrintIntInputBox("Enter product id: ");
        double price = PrintDoubleInputBox("Enter product price: ");

        Console.WriteLine(machine.ChangeProductPrice(id, price));
    }
    private void PrintAddProduct()
    {
        string name = PrintStringInputBox("Enter product name: ");
        int id = PrintIntInputBox("Enter product id: ");
        double price = PrintDoubleInputBox("Enter product price: ");
        int amount = PrintIntInputBox("Enter amount of products: ");

        if (machine.CheckMachineRoom(id, amount) != null)
        {
            Console.WriteLine(machine.CheckMachineRoom(id, amount));
            return;
        }

        Product product = new Product(name, id, price);
        FactoryPack pack = factory.BuyFromFactory(product, admin.Money, amount);
        Console.WriteLine(machine.AddProduct(pack));
    }
    private void PrintAdminMenu()
    {
        Console.WriteLine("- Admin Menu");
        Console.WriteLine("0. Refill product");
        Console.WriteLine("1. Change product price");
        Console.WriteLine("2. Add product");
        Console.WriteLine("3. Empty money box");
        Console.WriteLine("4. Show balance");
        Console.WriteLine("5. Back");

        int choice = PrintIntInputBox("Enter choice: ");

        switch (choice)
        {
        case 0:
            PrintRefillProduct();
            break;
        case 1:
            PrintChangeProductPrice();
            break;
        case 2:
            PrintAddProduct();
            break;
        case 3:
            Money money = machine.EmptyMoneyBox();
            Console.WriteLine("Money box was emptied with DKK " + money.Balance);
            admin.Money.Balance += money.Balance;
            break;
        case 4:
            Console.WriteLine("Admin balance is DKK " + admin.Money.Balance);
            break;
        case 5:
            menu = Menu.Start;
            return;
        default:
            Console.WriteLine("Failed...");
            break;
        }

        Console.ReadKey();
    }

    // User menu ***********************************************************
    private void PrintUserBuy()
    {
        int id = PrintIntInputBox("Enter product id: ");

        Console.WriteLine("\nAre you sure you want to by this product ID: " + id + "?");
        Console.WriteLine("0. No  | 1. Yes");
        int answer = PrintIntInputBox("Enter choice: ");
        
        if (answer != 1)
            return;

        Product product = machine.BuyProduct(id, user.Money);
        if (product != null)
        {
            Console.WriteLine("You bought product!");
            user.OwnedProducts.Add(product);
        }
        else
            Console.WriteLine("Failed...");
    }
    private void PrintUserOwnedProducts()
    {
        Console.WriteLine("\n- Owned products");
        for (int i = 0; i < user.OwnedProducts.Count; i++)
            Console.WriteLine("You own: " + user.OwnedProducts[i].Name);
    }
    private void PrintUserWork()
    {
        user.Money.Balance += 33;
        Console.WriteLine("Working...");
        Thread.Sleep(1000);
        Console.WriteLine("You have been hard working and recived DKK 10.00");
    }
    private void PrintUserMenu()
    {
        Console.WriteLine("- User Menu");
        Console.WriteLine("0. Buy product");
        Console.WriteLine("1. Show owned products");
        Console.WriteLine("2. Work");
        Console.WriteLine("3. Show balance");
        Console.WriteLine("4. Back");

        int choice = PrintIntInputBox("Enter choice: ");

        switch (choice)
        {
        case 0:
            PrintUserBuy();
            break;
        case 1:
            PrintUserOwnedProducts();
            break;
        case 2:
            PrintUserWork();
            break;
        case 3:
            Console.WriteLine("User balance is DKK " + user.Money.Balance);
            break;
        case 4:
            menu = Menu.Start;
            return;
        default:
            Console.WriteLine("Failed...");
            break;
        }

        Console.ReadKey();
    }

    // Main ***************************************************************
    private void PrintMenus()
    {
        switch (menu)
        {
        case Menu.Start:
            PrintStartMenu();
            break;
        case Menu.User:
            PrintUserMenu();
            break;
        case Menu.Admin:
            PrintAdminMenu();
            break;
        }
    }
    public void Main()
    {
        PrintMachine();
        PrintMenus();
        Console.Clear();
    }
}