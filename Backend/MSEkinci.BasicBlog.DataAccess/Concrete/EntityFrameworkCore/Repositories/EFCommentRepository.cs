using Microsoft.EntityFrameworkCore;
using MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Context;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EFCommentRepository : EFGenericRepository<Comment>, ICommentDAL
    {
        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int blogId, int? parentCommentId)
        {
            List<Comment> result = new List<Comment>();
            await GetComments(blogId, parentCommentId, result);
            return result;
        }

        private async Task GetComments(int blogId, int? parentCommentId, List<Comment> result)
        {
            using var context = new BlogContext();
            var comments = await context.Comments.Where(x => x.BlogId == blogId & x.ParentCommentId == parentCommentId).OrderByDescending(x => x.PostedTime).ToListAsync();
            if (comments.Count > 0)
            {
                foreach (var comment in comments)
                {
                    if (comment.SubComments == null)
                    {
                        comment.SubComments = new List<Comment>();
                    }

                    await GetComments(blogId, comment.Id, comment.SubComments);

                    if (!result.Contains(comment))
                    {
                        result.Add(comment);
                    }
                }
            }
        }
    }
}
