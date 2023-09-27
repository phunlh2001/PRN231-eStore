using BusinessObjects.Objects;
using System.Collections.Generic;

namespace DataAccess.Repository.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetList();
        Product GetProduct(int id);
        IEnumerable<Product> GetAllByName(string name);
        Product GetByName(string name);
        void InsertProduct(Product prod);
        void UpdateProduct(Product prod);
        void DeleteProduct(int id);
    }
}
