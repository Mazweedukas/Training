using ConsoleMenu;
using ConsoleMenu.Builder;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;

namespace ConsoleApp1;

public enum UserRoles
{
    Guest,
    Administrator,
    RegistredCustomer,
}

public enum OrderState
{
    None = 0,
    New = 1,
    CancelledByUser = 2,
    CancelledByAdmin = 3,
    Confirmed = 4,
    MovedToDelivery = 5,
    InDelivery = 6,
    Delivered = 7,
    DeliveryConfirmed = 8,
}

public static class UserMenuController
{
    private static readonly Dictionary<UserRoles, Menu> RolesToMenu;
    private static int userId;
    private static UserRoles userRole;
    private static StoreDbContext context;

    static UserMenuController()
    {
        userId = 0;
        userRole = UserRoles.Guest;
        RolesToMenu = new Dictionary<UserRoles, Menu>();
        var factory = new StoreDbFactory(new TestDataFactory());
        context = factory.CreateContext();
        RolesToMenu.Add(UserRoles.Guest, new GuestMainMenu().Create(context));
        RolesToMenu.Add(UserRoles.RegistredCustomer, new UserMainMenu().Create(context));
        RolesToMenu.Add(UserRoles.Administrator, new AdminMainMenu().Create(context));
    }

    public static int CurrentUserId
    {
        get { return userId; }
    }

    public static StoreDbContext Context
    {
        get { return context; }
    }

    public static int CurrentOrderId { get; set; }

    public static void Login()
    {
        Console.WriteLine("Login: ");
        var login = Console.ReadLine();
        Console.WriteLine("Password: ");
        var password = Console.ReadLine();

        if (login == null || password == null)
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        var service = new UserService(context);

        var currentUser = (UserModel?)service.Login(login, password);

        if (currentUser == null)
        {
            Console.WriteLine("User wasn't found");
            return;
        }

        if (currentUser.RoleId == 1)
        {
            userId = currentUser.Id;
            userRole = UserRoles.Administrator;
            Console.WriteLine("Login successful");
            return;
        }

        if (currentUser.RoleId == 2)
        {
            userId = currentUser.Id;
            userRole = UserRoles.RegistredCustomer;
            Console.WriteLine("Login successful");
            return;
        }

        Console.WriteLine("Incorrect Role of the user");
    }

    public static void Register()
    {
        Console.WriteLine("First Name: ");
        var firstName = Console.ReadLine();
        Console.WriteLine("Last Name: ");
        var lastName = Console.ReadLine();
        Console.WriteLine("Login: ");
        var login = Console.ReadLine();
        Console.WriteLine("Password: ");
        var password = Console.ReadLine();

        var service = new UserService(context);

        if (login == null || password == null || firstName == null || lastName == null)
        {
            Console.WriteLine("Incorrect input");
            return;
        }

        if (service.Register(firstName, lastName, login, password))
        {
            Console.WriteLine("Registered successfully");
            Console.WriteLine("Logging in:");
            var currentUser = service.Login(login, password);

            if (currentUser == null)
            {
                Console.WriteLine("User wasn't found");
                return;
            }

            UserModel currentUserModel = (UserModel)currentUser;

            if (currentUserModel.RoleId == 1)
            {
                userId = currentUserModel.Id;
                userRole = UserRoles.Administrator;
                Console.WriteLine("Login successful");
                return;
            }

            if (currentUserModel.RoleId == 2)
            {
                userId = currentUserModel.Id;
                userRole = UserRoles.RegistredCustomer;
                Console.WriteLine("Login successful");
            }
        }
        else
        {
            Console.WriteLine("Unsuccessful registration");
        }
    }

    public static void Logout()
    {
        userId = 0;
        userRole = UserRoles.Guest;
    }

    public static void Start()
    {
        ConsoleKey resKey;
        bool updateItems = true;
        do
        {
            resKey = RolesToMenu[userRole].RunOnce(ref updateItems);
        }
        while (resKey != ConsoleKey.Escape);
    }
}