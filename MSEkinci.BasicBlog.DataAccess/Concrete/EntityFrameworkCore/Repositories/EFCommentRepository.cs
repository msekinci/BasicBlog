using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;

namespace MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EFCommentRepository : EFGenericRepository<Comment>, ICommentDAL
    {
    }
}
