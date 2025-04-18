using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // 💡 Thay chuỗi kết nối này cho đúng với SQL Server của bạn
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-1F6H2NE;Initial Catalog=NET1061_ASM;Integrated Security=True;Encrypt=False");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
