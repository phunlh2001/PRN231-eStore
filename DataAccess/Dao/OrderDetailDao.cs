using BusinessObjects;
using BusinessObjects.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao
{
    public class OrderDetailDao
    {
        #region Singleton Intance
        private static OrderDetailDao instance;
        private static readonly object instanceLock = new object();
        public static OrderDetailDao GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDao();
                    }
                    return instance;
                }
            }
        }
        #endregion

        public IEnumerable<OrderDetail> GetAll()
        {
            List<OrderDetail> orderDetails;
            try
            {
                using var context = new AppDbContext();
                orderDetails = context.OrderDetails.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return orderDetails;
        }

        public void Insert(OrderDetail detail)
        {
            try
            {
                using var context = new AppDbContext();
                context.OrderDetails.Add(detail);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
