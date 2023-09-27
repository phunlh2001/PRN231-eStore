using BusinessObjects.Objects;
using System.Collections.Generic;

namespace DataAccess.Repository.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
    }
}
