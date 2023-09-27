using BusinessObjects.Objects;
using DataAccess.Dao;
using DataAccess.Repository.Interface;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> GetAll() => CategoryDao.GetInstance.GetAll();

        public Category GetById(int id) => CategoryDao.GetInstance.GetById(id);
    }
}
