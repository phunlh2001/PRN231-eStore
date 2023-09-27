using BusinessObjects.Objects;
using DataAccess.Dao;
using DataAccess.Repository.Interface;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(int id) => ProductDao.GetInstance.Delete(id);

        public IEnumerable<Product> GetAllByName(string name) => ProductDao.GetInstance.GetAllByName(name);

        public Product GetByName(string name) => ProductDao.GetInstance.GetByName(name);

        public IEnumerable<Product> GetList() => ProductDao.GetInstance.GetAll();

        public Product GetProduct(int id) => ProductDao.GetInstance.GetById(id);

        public void InsertProduct(Product prod) => ProductDao.GetInstance.Insert(prod);

        public void UpdateProduct(Product prod) => ProductDao.GetInstance.Update(prod);
    }
}
