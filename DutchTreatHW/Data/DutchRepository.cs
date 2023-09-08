using DutchTreatHW.Data.Entities;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreatHW.Data;

public class DutchRepository : IDutchRepository
{
    private DutchContext _ctx;
    private readonly ILogger<DutchRepository> _logger;

    public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        try
        {
            _logger.LogInformation("Get all products was called");

            return _ctx.Products
                .OrderBy(p => p.Title)
                .ToList();
        }
        catch (System.Exception ex)
        {

            _logger.LogError($"Failed to get all products: {ex}");
            return null;
        }      
    }

    public IEnumerable<Product> GetProductsByCategory(string category)
    {
        return _ctx.Products.Where(p => p.Category == category).ToList();
    }

    public bool SaveAll()
    {
        return _ctx.SaveChanges() > 0;
    }
}