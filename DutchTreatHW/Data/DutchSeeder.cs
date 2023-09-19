using DutchTreatHW.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DutchTreatHW.Data;

public class DutchSeeder
{
    // need save or read data from DutchContext
    private readonly DutchContext _ctx;
    private readonly IWebHostEnvironment _hosting;
    private readonly UserManager<StoreUser> _userManager;

    public DutchSeeder(DutchContext ctx, IWebHostEnvironment hosting, UserManager<StoreUser> userManager)
    {
        _ctx = ctx;
        _hosting = hosting;
        _userManager = userManager;
    }

    public async Task SeedAsync()
    {
        _ctx.Database.EnsureCreated();

        StoreUser user = await _userManager.FindByEmailAsync("shawn@dutchthreat.com");
        if (user == null) 
        {
            user = new StoreUser()
            {
                FirstName = "Shawn",
                LastName = "Wildermuch",
                Email = "Shawn@dutchtreat.com",
                UserName = "shawn@dutchtreat.com"
            };

            var result = await _userManager.CreateAsync(user, "P@ssw0rd!");

            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create new user in seeder");
            }
        }   

        if (! _ctx.Products.Any())
        {
            // Need to create sample data
            var filePath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
            var json = File.ReadAllText(filePath);
            var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

            _ctx.Products.AddRange(products);

            var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
            if (order != null)
            {
                order.User = user;
                order.Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {   
                        Product = products.First(),
                        Quantity = 5,
                        UnitPrice = products.First().Price
                    }
                };
            }

            _ctx.SaveChanges();
        }
    }
}
