using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EFCategoryRepository : EFGenericRepository<Category>, ICategoryDAL
    {
    }
}
