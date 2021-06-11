using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //Console.WriteLine(ProblemOne());
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //Console.WriteLine(BonusOne());
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private int ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var users = _context.Users.ToList().Count;
            return users;
        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products;

            var productsAboveValue = products.Where(p => p.Price > 150);

            foreach(var product in productsAboveValue)
            {
                Console.WriteLine(product.Name + " " + product.Price);
            }
        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products;

            var productsWithS = products.Where(p => p.Name.Contains("s"));

            foreach (var product in productsWithS)
            {
                Console.WriteLine(product.Name);
            }
        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.

            var users = _context.Users;
            var date = new DateTime(2016, 1, 1, 0, 0, 0);

            var usersBefore2016 = users.Where(user => user.RegistrationDate < date);
            
            foreach(var userDate in usersBefore2016)
            {
                Console.WriteLine(userDate.Email + " " + userDate.RegistrationDate);
            }
        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            var users = _context.Users;
            var beforeDate = new DateTime(2018, 1, 1, 0, 0, 0);
            var afterDate = new DateTime(2016, 1, 1, 0, 0, 0);

            var usersInDateRange = users.Where(user => beforeDate > user.RegistrationDate && user.RegistrationDate > afterDate);

            foreach (var userDate in usersInDateRange)
            {
                Console.WriteLine(userDate.Email + " " + userDate.RegistrationDate);
            }

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var customerCart = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => ur.User.Email == "afton@gmail.com");
            foreach(ShoppingCart cart in customerCart)
            {
                Console.WriteLine(cart.Product.Name + " " + cart.Product.Price + " " + cart.Quantity);
            }
        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var customerCartTotal = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => ur.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();
            Console.WriteLine(customerCartTotal);
        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.

            var selectedEmployees = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Employee").ToList();
            var shoppingCartContents = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product);

            foreach (var emp in selectedEmployees)
            {
                foreach(var product in shoppingCartContents)
                {
                    if (product.UserId == emp.UserId)
                    {
                        Console.WriteLine(product.Product.Name + " " + product.Product.Price + " " + product.Quantity);
                    }
                }
            }
        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Garden Rake",
                Description = "Finest rake in the land.",
                Price = 12
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.

            var productId = _context.Products.Where(p => p.Name == "Garden Rake").Select(p => p.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();

            ShoppingCart newItem = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };

            _context.ShoppingCarts.Add(newItem);
            _context.SaveChanges();

        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Name == "Garden Rake").SingleOrDefault();
            product.Price = 120;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var user = _context.Users.Where(ur => ur.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private string BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".

            Console.WriteLine("Enter your email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();

            var users = _context.Users.ToList();

            foreach(var user in users)
            {
                if (user.Email == email && user.Password == password)
                {
                    return "Signed in!";
                }                
            }
            return "Invalid email or password";
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.

            //var customerCartTotal = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => ur.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();

            var users = _context.Users;
            var cart = _context.ShoppingCarts.Include(p => p.Product).ToList();

            decimal totalSum = 0;

            foreach(var user in users)
            {
                decimal sum = 0;
                foreach (var c in cart)
                {
                    if(user.Id == c.UserId)
                    {
                        sum += c.Product.Price * Convert.ToDecimal(c.Quantity);
                    }
                }
                Console.WriteLine(user.Id + "--Sum: " + sum);
                totalSum += sum;
            }
            Console.WriteLine("Total: " + totalSum);
        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials

            Console.WriteLine("Enter your email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();

            var users = _context.Users.ToList();

            foreach (var user in users)
            {
                if (user.Email == email && user.Password == password)
                {
                    Console.WriteLine("Signed in!"); ;
                    bool loop = true;
                    while (loop) { 

                        Console.WriteLine(@"Select an option:
                                        1-Products in Cart
                                        2-View All Products
                                        3-Add To Cart
                                        4-Remove Item From Cart
                                        5-Exit");

                        string userInput = Console.ReadLine();

                        switch (userInput)
                        {
                            case "1":
                                var customerCart = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => ur.User.Email == email);
                                foreach (ShoppingCart cart in customerCart)
                                {
                                    Console.WriteLine(cart.Product.Name + " " + cart.Product.Price + " " + cart.Quantity);
                                }
                                break;
                            case "2":
                                var allProducts = _context.Products;
                                foreach (var product in allProducts)
                                {
                                    Console.WriteLine("All products:");
                                    Console.WriteLine(product.Name + " " + product.Description + " " + product.Price);
                                }
                                break;
                            case "3":
                                Console.WriteLine("Type name of product of your choice");
                                string chosenProduct = Console.ReadLine();

                                var customerCart2 = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => ur.User.Email == email).ToList();

                                var selectedProduct = _context.Products.Where(p => p.Name == chosenProduct).Select(p => p.Id).SingleOrDefault();

                                var shopCart = _context.ShoppingCarts.Include(p => p.Product);

                                foreach (var cart2 in customerCart2)
                                {
                                    Console.WriteLine(cart2.Product.Name);
                                    Console.WriteLine(chosenProduct);
                                    if (cart2.Product.Name == chosenProduct)
                                    {
                                        cart2.Quantity += 1;
                                        _context.ShoppingCarts.Update(cart2);
                                        _context.SaveChanges();
                                        break;
                                    }
                                }

                                ShoppingCart newItem = new ShoppingCart()
                                {
                                    UserId = user.Id,
                                    ProductId = Convert.ToInt32(selectedProduct),
                                    Quantity = 1
                                };

                                _context.ShoppingCarts.Add(newItem);
                                _context.SaveChanges();
                                Console.WriteLine("Item Added!");
                                break;

                            case "4":

                                var currentCartProductById = _context.ShoppingCarts.Include(p => p.Product).Where(c => c.UserId == user.Id);

                                var currentCartProducts = _context.ShoppingCarts.Include(p => p.Product);

                                foreach (var cartObject in currentCartProductById)
                                {
                                    Console.WriteLine(cartObject.Product.Name);
                                }

                                Console.WriteLine("Type name of item you wish to remove");
                                string removedItem = Console.ReadLine();

                                var removedItemId = _context.Products.Where(p => p.Name == removedItem).Select(p => p.Id).SingleOrDefault();

                                removedItemId = Convert.ToInt32(removedItemId);

                                var itemToRemove = _context.ShoppingCarts.Where(i => i.UserId == user.Id && i.ProductId == removedItemId).SingleOrDefault();

                                _context.ShoppingCarts.Remove(itemToRemove);
                                _context.SaveChanges();
                                break;

                            case "5":
                                loop = false;

                                break;


                        }
                    }

                }
            }
            Console.WriteLine("Invalid email or password"); ;
            BonusThree();

        }

    }
}
