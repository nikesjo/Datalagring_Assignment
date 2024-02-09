using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Presentation.ConsoleApp;

internal class ConsoleUI
{
    private readonly IUserService _userService;
    private readonly IProductService _productService;

    public ConsoleUI(IUserService userService, IProductService productService)
    {
        _userService = userService;
        _productService = productService;
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
                    await ProductMenu();
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
        }
    }

    //Users
    public async Task CreateUser_UI()
    {
        UserRegistrationDto user = new();

        Console.Clear();
        Console.WriteLine("--- Create New User ---");
        Console.WriteLine();

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
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Something went wrong!");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
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
                    Console.WriteLine($"{count}.");
                    Console.WriteLine($"User Id: {user.Id}");
                    Console.WriteLine($"{user.FirstName} {user.LastName} ");
                    Console.WriteLine($"{user.PhoneNumber} {user.Email}");
                    
                    foreach (var address in user.Addresses)
                    {
                        Console.WriteLine($"{address.StreetName}");
                        Console.WriteLine($"{address.PostalCode} {address.City}");
                    }
                    Console.WriteLine();
                    count++;
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("No users was found.");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
        }
    }

    public async Task GetUser_UI()
    {
        Console.Clear();
        Console.WriteLine("--- Show Specific User ---");
        Console.WriteLine();
        Console.WriteLine("Enter User Email: ");
        var email = Console.ReadLine();

        if (!string.IsNullOrEmpty(email))
        {
            var user = await _userService.GetUserAsync(x => x.Auth.Email == email);
            Console.Clear();

            if (user != null)
            {
                Console.WriteLine();
                Console.WriteLine($"User Id: {user.Id}");
                Console.WriteLine($"{user.FirstName} {user.LastName} ");
                Console.WriteLine($"{user.PhoneNumber} {user.Email}");

                foreach (var address in user.Addresses)
                {
                    Console.WriteLine($"{address.StreetName}");
                    Console.WriteLine($"{address.PostalCode} {address.City}");
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("No user was found.");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
            }
        }
    }

    public async Task UpdateUser_UI()
    {
        Console.Clear();
        Console.WriteLine("--- Update User ---");
        Console.WriteLine();
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
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Failed to update user");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                }
            }
        }
    }

    public async Task DeleteUser_UI()
    {
        Console.Clear();
        Console.WriteLine("--- Delete User ---");
        Console.WriteLine();
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
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Failed to delete the user.");
                Console.WriteLine("Press any key to continue...");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid email.");
            Console.WriteLine("Press any key to continue...");
        }
    }

    //Products
    public async Task ProductMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Product Menu ---");
            Console.WriteLine();
            Console.WriteLine("1. Create new product");
            Console.WriteLine("2. Show all products");
            Console.WriteLine("3. Show specific product");
            Console.WriteLine("4. Update product");
            Console.WriteLine("5. Delete product");
            Console.WriteLine("6. Go back to main menu");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateProduct_UI();
                    break;
                case "2":
                    await GetProducts_UI();
                    break;
                case "3":
                    await GetProduct_UI();
                    break;
                case "4":
                    await UpdateProduct_UI();
                    break;
                case "5":
                    await DeleteProduct_UI();
                    break;
                case "6":
                    await MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }
        }
    }

    public async Task CreateProduct_UI()
    {
        ProductRegDto product = new();

        Console.Clear();
        Console.WriteLine("--- Create New Product ---");
        Console.WriteLine();

        Console.Write("Enter Article Number: ");
        product.ArticleNumber = Console.ReadLine()!;

        Console.Write("Enter Title: ");
        product.Title = Console.ReadLine()!;

        Console.Write("Enter Description: ");
        product.Description = Console.ReadLine()!;

        Console.Write("Enter Specification: ");
        product.Specification = Console.ReadLine()!;

        Console.Write("Enter Categoryname: ");
        product.CategoryName = Console.ReadLine()!;

        Console.Write("Enter Manufacture: ");
        product.Manufacture = Console.ReadLine()!;

        Console.Write("Enter Price: ");
        product.Price = decimal.Parse(Console.ReadLine()!);

        Console.Write("Enter Currency Code: ");
        product.CurrencyCode = Console.ReadLine()!;

        Console.Write("Enter Currency: ");
        product.Currency = Console.ReadLine()!;

        var result = await _productService.CreateProductAsync(product);
        if (result == true)
        {
            Console.Clear();
            Console.WriteLine("Product was created!");
            Console.WriteLine("Press any key to continue...");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Something went wrong!");
            Console.WriteLine("Press any key to continue...");
        }
    }

    public async Task GetProducts_UI()
    {
        Console.Clear();
        Console.WriteLine("--- Show All Products ---");
        Console.WriteLine();
        var result = await _productService.GetProductsAsync();

        if (result != null)
        {
            var products = result as List<ProductDto>;

            if (products != null)
            {
                int count = 1;
                foreach (var product in products)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{count}. ");
                    Console.WriteLine($"Article Number: {product.ArticleNumber}");
                    Console.WriteLine($"{product.Title}");
                    Console.WriteLine($"{product.Description} ");
                    Console.WriteLine($"{product.Specification}");
                    Console.WriteLine($"{product.Manufacture} ");
                    Console.WriteLine($"{product.CategoryName}");
                    Console.WriteLine($"{product.Price} {product.CurrencyCode} {product.Currency}  ");
                    Console.WriteLine();

                    count++;
                }
                Console.WriteLine("Press any key to continue...");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("No contacts was found.");
            Console.WriteLine("Press any key to continue...");
        }
    }
    public async Task GetProduct_UI()
    {
        Console.Clear();
        Console.WriteLine("--- Show Specific Product ---");
        Console.WriteLine();
        Console.WriteLine("Enter the products article number: ");

        var articleNumberInput = Console.ReadLine();

        if (articleNumberInput != null)
        {
            var result = await _productService.GetProductAsync(x => x.ArticleNumber == articleNumberInput);

            Console.Clear();

            if (result != null)
            {
                Console.WriteLine();
                Console.WriteLine($"Article Number: {result.ArticleNumber}");
                Console.WriteLine($"{result.Title}");
                Console.WriteLine($"{result.Description} ");
                Console.WriteLine($"{result.Specification}");
                Console.WriteLine($"{result.Manufacture} ");
                Console.WriteLine($"{result.CategoryName}");
                Console.WriteLine($"{result.Price} {result.CurrencyCode} {result.Currency}  ");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("No product found.");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid article number.");
        }
    }
    public async Task UpdateProduct_UI()
    {
        Console.Clear();
        Console.WriteLine("--- Update Product ---");
        Console.WriteLine();
        Console.WriteLine("Enter the products article number: ");

        var articleNumberInput = Console.ReadLine();

        if (articleNumberInput != null)
        {
            var productToUpdate = await _productService.GetProductAsync(x => x.ArticleNumber == articleNumberInput);

            if (productToUpdate != null)
            {
                Console.Clear();
                Console.WriteLine("Enter new product details:");

                Console.Write("Enter Title:  ");
                productToUpdate.Title = Console.ReadLine()!;

                Console.Write("Enter new description:  ");
                productToUpdate.Description = Console.ReadLine()!;

                Console.Write("Enter new specification:  ");
                productToUpdate.Specification = Console.ReadLine()!;

                Console.Write("Enter new categoryname:  ");
                productToUpdate.CategoryName = Console.ReadLine()!;

                Console.Write("Enter new manufacturer:  ");
                productToUpdate.Manufacture = Console.ReadLine()!;

                Console.Write("Enter new price:  ");
                productToUpdate.Price = decimal.Parse(Console.ReadLine()!);

                Console.Write("Enter new currency code:  ");
                productToUpdate.CurrencyCode = Console.ReadLine()!;

                Console.Write("Enter new currency full name:  ");
                productToUpdate.Currency = Console.ReadLine()!;

                var updatedProductResult = await _productService.UpdateProductAsync(productToUpdate);
                if (updatedProductResult != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Product successfully updated!");
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue...");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Failed to update product");
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue...");
                }
            }
        }
    }
    public async Task DeleteProduct_UI()
    {
        Console.Clear();
        Console.WriteLine("--- Delete Product ---");
        Console.WriteLine();
        Console.WriteLine("Enter the products article number: ");

        var articleNumberInput = Console.ReadLine();

        if (articleNumberInput != null)
        {
            var deleteResult = await _productService.DeleteProductAsync(new ProductDto { ArticleNumber = articleNumberInput });

            if (deleteResult)
            {
                Console.WriteLine();
                Console.WriteLine("Product successfully deleted!");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Failed to delete the product.");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid article number.");
        }
    }
}
