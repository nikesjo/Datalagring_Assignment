using Infrastructure.Dtos;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.ConsoleApp;

internal class ConsoleUI
{
    private readonly IUserService _userService;

    public ConsoleUI(IUserService userService)
    {
        _userService = userService;
    }

    public async Task MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Menu ---");
            Console.WriteLine();
            Console.WriteLine("1. User menu");
            Console.WriteLine("2. Product menu");
            Console.WriteLine("3. Exit");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await UserMenu();
                    break;
                case "2":
                    //await ProductMenu();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }
        }
    }
    public async Task UserMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- User Menu ---");
            Console.WriteLine();
            Console.WriteLine("1. Create new user");
            Console.WriteLine("2. Show all users");
            Console.WriteLine("3. Show specific user");
            Console.WriteLine("4. Update user");
            Console.WriteLine("5. Delete user");
            Console.WriteLine("6. Go back to main menu");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateUser_UI();
                    break;
                case "2":
                    await GetUsers_UI();
                    break;
                case "3":
                    await GetUser_UI();
                    break;
                case "4":
                    await UpdateUser_UI();
                    break;
                case "5":
                    await DeleteUser_UI();
                    break;
                case "6":
                    await MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }

            Console.ReadKey();
        }
    }

    //Users
    public async Task CreateUser_UI()
    {
        UserRegistrationDto user = new();

        Console.Clear();
        Console.WriteLine("--- Create User ---");

        Console.Write("First Name:");
        user.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        user.LastName = Console.ReadLine()!;

        Console.Write("Phone Number: ");
        user.PhoneNumber = Console.ReadLine()!;

        Console.Write("Email: ");
        user.Email = Console.ReadLine()!;

        Console.Write("Password: ");
        user.Password = Console.ReadLine()!;

        Console.Write("Street Name: ");
        user.StreetName = Console.ReadLine()!;

        Console.Write("Postal Code: ");
        user.PostalCode = Console.ReadLine()!;

        Console.Write("City: ");
        user.City = Console.ReadLine()!;

        var result = await _userService.CreateUserAsync(user);
        if (result != null)
        {
            Console.Clear();
            Console.WriteLine("User was created!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Something went wrong!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    public async Task GetUsers_UI()
    {
        Console.Clear();
        Console.WriteLine("--- Show All Users ---");
        var result = await _userService.GetUsersAsync();

        if (result != null)
        {
            var users = result as List<UserDto>;

            if (users != null)
            {
                Console.Clear();
                int count = 1;
                foreach (var user in users)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{count}. {user.FirstName} {user.LastName} ");
                    Console.WriteLine($"{user.PhoneNumber} {user.Email}");
                    
                    foreach (var address in user.Addresses)
                    {
                        Console.WriteLine($"{address.StreetName}");
                        Console.WriteLine($"{address.PostalCode} {address.City}");
                    }
                    Console.WriteLine();
                    count++;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No users was found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    public async Task GetUser_UI()
    {
        Console.Clear();
        Console.WriteLine("--- Show Specific User ---");
        Console.WriteLine("Enter User Email: ");
        var email = Console.ReadLine();

        if (!string.IsNullOrEmpty(email))
        {
            var user = await _userService.GetUserAsync(x => x.Auth.Email == email);
            Console.Clear();

            if (user != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{user.FirstName} {user.LastName} ");
                Console.WriteLine($"{user.PhoneNumber} {user.Email}");

                foreach (var address in user.Addresses)
                {
                    Console.WriteLine($"{address.StreetName}");
                    Console.WriteLine($"{address.PostalCode} {address.City}");
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No user was found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    public async Task UpdateUser_UI()
    {
        Console.Clear();
        Console.WriteLine("--- Update User ---");
        Console.WriteLine("Enter User Id: ");
        var id = int.Parse(Console.ReadLine()!);

        if (id > 0)
        {
            var userToUpdate = await _userService.GetUserAsync(x => x.Id == id);

            if (userToUpdate != null)
            {
                Console.Clear();
                Console.WriteLine("Enter new user details:");
                Console.WriteLine();

                Console.Write("Enter First Name: ");
                userToUpdate.FirstName = Console.ReadLine()!;

                Console.Write("Enter Last Name: ");
                userToUpdate.LastName = Console.ReadLine()!;

                Console.Write("Enter Phone Number: ");
                userToUpdate.PhoneNumber = Console.ReadLine()!;

                Console.Write("Enter Email: ");
                userToUpdate.Email = Console.ReadLine()!;

                Console.Write("Enter Password: ");
                userToUpdate.Password = Console.ReadLine()!;

                foreach (var address in userToUpdate.Addresses)
                {
                    Console.Write("Enter StreetName: ");
                    address.StreetName = Console.ReadLine()!;

                    Console.Write("Enter PostalCode: ");
                    address.PostalCode = Console.ReadLine()!;

                    Console.Write("Enter City: ");
                    address.City = Console.ReadLine()!;
                }

                var updatedUser = await _userService.UpdateUserAsync(userToUpdate);
                
                if (updatedUser != null)
                {
                    Console.Clear();
                    Console.WriteLine("User successfully updated!");

                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Failed to update user");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }

    public async Task DeleteUser_UI()
    {
        Console.Clear();
        Console.WriteLine("--- Delete User ---");
        Console.WriteLine("Enter User Email: ");
        var email = Console.ReadLine();

        if (!string.IsNullOrEmpty(email))
        {
            var userToDelete = await _userService.DeleteUserAsync(new UserDto { Email = email });

            if (userToDelete)
            {
                Console.Clear();
                Console.WriteLine("User successfully deleted!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Failed to delete the user.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Invalid email address.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
