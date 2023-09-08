using DutchTreatHW.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DutchTreatHW.Data;

public class DutchSeeder
{
    // need save or read data from DutchContext
    private readonly DutchContext _ctx;
    private readonly IWebHostEnvironment _hosting;

    public DutchSeeder(DutchContext ctx, IWebHostEnvironment hosting)
    {
        _ctx = ctx;
        _hosting = hosting;
    }

    public void Seed()
    {
        _ctx.Database.EnsureCreated();

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
