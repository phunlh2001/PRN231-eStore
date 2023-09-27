using BusinessObjects;
using BusinessObjects.Objects;
using DataAccess.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DataAccess.Dao
{
    public class OrderDao
    {
        #region Singleton Intance
        private static OrderDao instance;
        private static readonly object instanceLock = new object();
        public static OrderDao GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDao();
                    }
                    return instance;
                }
            }
        }
        #endregion

        public IEnumerable<Order> GetAll()
        {
            IEnumerable<Order> order;
            try
            {
                using var context = new AppDbContext();
                order = context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(o => o.Product)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return order;
        }

        public IEnumerable<Order> GetAllByUserId(int id)
        {
            IEnumerable<Order> order;
            try
            {
                using var context = new AppDbContext();
                order = context.Orders
                    .Where(o => o.MemberId == id)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return order;
        }

        public Order GetOne(int id)
        {
            Order order;
            try
            {
                using var context = new AppDbContext();
                order = context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(o => o.Product)
                    .ThenInclude(p => p.Category)
                    .FirstOrDefault(o => o.OrderId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return order;
        }

        public void Insert(Order order)
        {
            try
            {
                Order _order = GetOne(order.OrderId);
                if (_order == null)
                {
                    order.RequiredDate = BaseDate.GetSevenNextDays().ToString("dd/MM/yyyy");
                    order.ShippedDate = BaseDate.GetFiveNextDays().ToString("dd/MM/yyyy");

                    using var context = new AppDbContext();
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order is already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Order order)
        {
            try
            {
                Order _order = GetOne(order.OrderId);
                if (_order != null)
                {
                    using var context = new AppDbContext();
                    context.Orders.Update(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order does not already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                Order _order = GetOne(id);
                if (_order != null)
                {
                    using var context = new AppDbContext();
                    context.Orders.Remove(_order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order is already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Order> FilterDate(DateTime start, DateTime end)
        {
            IEnumerable<Order> orders;
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                string format = "dd/MM/yyyy";

                using var context = new AppDbContext();
                var allOrders = context.Orders.ToList();

                orders = allOrders
                    .Where(o => DateTime.ParseExact(o.OrderDate, format, provider) >= start &&
                        DateTime.ParseExact(o.OrderDate, format, provider) <= end)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return orders;
        }
    }
}
