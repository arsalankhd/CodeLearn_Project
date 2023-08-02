using System.Linq;
using CodeLearn.DataLayer.Entities.Course;
using CodeLearn.DataLayer.Entities.Order;
using CodeLearn.DataLayer.Entities.Permissions;
using CodeLearn.DataLayer.Entities.Question;
using CodeLearn.DataLayer.Entities.User;
using CodeLearn.DataLayer.Entities.Wallet;
using Microsoft.EntityFrameworkCore;

namespace CodeLearn.DataLayer.Context
{
    public class CodeLearnContext : DbContext
    {
        public CodeLearnContext(DbContextOptions<CodeLearnContext> options) : base(options)
        {

        }

        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserDiscountCode> UserDiscountCodes { get; set; }

        #endregion

        #region Wallet

        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        #endregion

        #region Permission

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Course

        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }


        #endregion

        #region Order

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        #endregion

        #region Question

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<CourseGroup>()
                .HasQueryFilter(g => !g.IsDelete);

            modelBuilder.Entity<Course>()
                .HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Entity<Course>()

                .HasOne<CourseGroup>(f => f.CourseGroup)

                .WithMany(g => g.Courses)

                .HasForeignKey(f => f.GroupId);

            modelBuilder.Entity<Course>()

                .HasOne<CourseGroup>(f => f.Group)

                .WithMany(g => g.SubGroup)

                .HasForeignKey(f => f.SubGroup);

            base.OnModelCreating(modelBuilder);
        }
    }
}
