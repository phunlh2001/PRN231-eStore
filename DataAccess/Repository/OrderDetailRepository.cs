using BusinessObjects.Objects;
using DataAccess.Dao;
using DataAccess.Repository.Interface;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void Add(OrderDetail detail) => OrderDetailDao.GetInstance.Insert(detail);

        public IEnumerable<OrderDetail> GetList() => OrderDetailDao.GetInstance.GetAll();
    }
}
