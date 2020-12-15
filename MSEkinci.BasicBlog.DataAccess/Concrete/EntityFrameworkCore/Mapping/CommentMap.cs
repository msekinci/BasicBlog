using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSEkinci.BasicBlog.Entities.Concrete;

namespace MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.AuthorName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.AuthorEmail).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(400).IsRequired();
            builder.HasMany(x => x.SubComments).WithOne(x => x.ParentComment).HasForeignKey(x => x.ParentCommentId);
        }
    }
}
