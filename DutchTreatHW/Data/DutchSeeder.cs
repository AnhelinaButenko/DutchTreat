﻿using DutchTreatHW.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DutchTreatHW.Data;

public class DutchSeeder
{
    private readonly DutchContext _ctx;
    private readonly IWebHostEnvironment _env;

    public DutchSeeder(DutchContext ctx, IWebHostEnvironment env)
    {
        _ctx = ctx;
        _env = env;
    }

    public void Seed()
    {
        _ctx.Database.EnsureCreated();

        if (! _ctx.Products.Any())
        {
            // Need to create sample data
            var filePath = Path.Combine("Data/art.json");
            var json = File.ReadAllText(filePath);
            var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

            _ctx.AddRange(products);

            var order = new Order()
            {
                OrderDate = System.DateTime.Today,
                OrderNumber = "10000",
                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Product = products.First(),
                        Quantity = 5,
                        UnitPrice = products.First().Price,
                    }
                }
            };

            _ctx.Add(order);

            _ctx.SaveChanges();
        }
    }
}
