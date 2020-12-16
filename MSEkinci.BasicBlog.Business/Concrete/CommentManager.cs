using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;

namespace MSEkinci.BasicBlog.Business.Concrete
{
    public class CommentManager : GenericManager<Comment>, ICommentService
    {
        private readonly IGenericDAL<Comment> _genericDAL;
        public CommentManager(IGenericDAL<Comment> genericDAL) : base(genericDAL)
        {
            _genericDAL = genericDAL;
        }
    }
}
