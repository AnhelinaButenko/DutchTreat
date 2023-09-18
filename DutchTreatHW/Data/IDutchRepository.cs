using DutchTreatHW.Data.Entities;
using System.Collections.Generic;

namespace DutchTreatHW.Data;

public interface IDutchRepository
{
    IEnumerable<Product> GetAllProducts();

    IEnumerable<Order> GetAllOrders(bool includeItems);

    Order GetOrderById(int id);

    IEnumerable<Product> GetProductsByCategory(string category);

    void AddEntity(object model);

    bool SaveAll();
}