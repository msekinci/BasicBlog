using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSEkinci.BasicBlog.Entities.Concrete;

namespace MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.HasMany(x => x.CategoryBlogs).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}
