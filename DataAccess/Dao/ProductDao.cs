using BusinessObjects;
using BusinessObjects.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao
{
    public class ProductDao
    {
        #region Singleton Intance
        private static ProductDao instance;
        private static readonly object instanceLock = new object();
        public static ProductDao GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDao();
                    }
                    return instance;
                }
            }
        }
        #endregion

        public IEnumerable<Product> GetAll()
        {
            List<Product> prod;
            try
            {
                var context = new AppDbContext();
                prod = context.Products
                        .Include(p => p.Category)
                        .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return prod;
        }

        public Product GetById(int id)
        {
            Product prod;
            try
            {
                using var context = new AppDbContext();
                prod = context.Products
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.ProductId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return prod;
        }

        public void Insert(Product prod)
        {
            try
            {
                Product _prod = GetById(prod.ProductId);
                if (_prod == null)
                {
                    using var context = new AppDbContext();
                    context.Products.Add(prod);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Product is already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Product prod)
        {
            try
            {
                Product _prod = GetById(prod.ProductId);
                if (_prod != null)
                {
                    using var context = new AppDbContext();
                    context.Products.Update(prod);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Product does not already exist.");
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
                Product _prod = GetById(id);
                if (_prod != null)
                {
                    using var context = new AppDbContext();
                    context.Products.Remove(_prod);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Product is already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Product> GetAllByName(string text)
        {
            IEnumerable<Product> prods;
            try
            {
                if (text != null)
                {
                    var context = new AppDbContext();
                    prods = context.Products
                        .Include(p => p.Category)
                        .Where(p => p.ProductName.Contains(text));
                }
                else
                {
                    throw new Exception("Search Input not null");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return prods;
        }

        public Product GetByName(string name)
        {
            Product prods = null;
            try
            {
                if (name != null)
                {
                    var context = new AppDbContext();
                    prods = context.Products
                        .Include(p => p.Category)
                        .FirstOrDefault(p => p.ProductName.Equals(name));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return prods;
        }
    }
}
