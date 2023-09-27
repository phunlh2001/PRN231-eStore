using BusinessObjects.Objects;
using System;
using System.Collections.Generic;

namespace DataAccess.Repository.Interface
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetList();
        Order GetOrder(int id);
        void InsertOrder(Order prod);
        void UpdateOrder(Order prod);
        void DeleteOrder(int id);
        IEnumerable<Order> Filter(DateTime start, DateTime end);
        IEnumerable<Order> GetOrdersByUser(int id);
    }
}
