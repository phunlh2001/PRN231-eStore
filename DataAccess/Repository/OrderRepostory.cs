using BusinessObjects.Objects;
using DataAccess.Dao;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class OrderRepostory : IOrderRepository
    {
        public void DeleteOrder(int id) => OrderDao.GetInstance.Delete(id);

        public IEnumerable<Order> Filter(DateTime start, DateTime end) => OrderDao.GetInstance.FilterDate(start, end);

        public IEnumerable<Order> GetList() => OrderDao.GetInstance.GetAll();

        public Order GetOrder(int id) => OrderDao.GetInstance.GetOne(id);

        public IEnumerable<Order> GetOrdersByUser(int id) => OrderDao.GetInstance.GetAllByUserId(id);

        public void InsertOrder(Order prod) => OrderDao.GetInstance.Insert(prod);

        public void UpdateOrder(Order prod) => OrderDao.GetInstance.Update(prod);
    }
}
