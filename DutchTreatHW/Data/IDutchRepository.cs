using DutchTreatHW.Data.Entities;
using System.Collections.Generic;

namespace DutchTreatHW.Data;

public interface IDutchRepository
{
    IEnumerable<Product> GetAllProducts();

    IEnumerable<Product> GetProductsByCategory(string category);

    bool SaveAll();
}