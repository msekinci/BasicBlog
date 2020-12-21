using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.Business.Concrete
{
    public class CommentManager : GenericManager<Comment>, ICommentService
    {
        private readonly IGenericDAL<Comment> _genericDAL;
        private readonly ICommentDAL _commentDAL;
        public CommentManager(IGenericDAL<Comment> genericDAL, ICommentDAL commentDAL) : base(genericDAL)
        {
            _genericDAL = genericDAL;
            _commentDAL = commentDAL;
        }

        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int blogId, int? parentCommentId)
        {
            return await _commentDAL.GetAllWithSubCommentsAsync(blogId, parentCommentId);
        }
    }
}
