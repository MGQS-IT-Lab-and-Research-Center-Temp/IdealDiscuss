using IdealDiscuss.Entities;

namespace IdealDiscuss.Context
{
    internal class DbInitializer
    {
        internal static void Initialize(IdealDiscussContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }

            var roles = new Role[]
            {
                new Role()
                {
                    RoleName = "Admin",
                    Description = "Role for admin",
                    CreatedBy = "System",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    ModifiedBy = "",
                    LastModified = new DateTime() //0001-01-01 00:00:00:00
                },
                new Role()
                {
                    RoleName = "AppUser",
                    Description = "Role for nominal user",
                    CreatedBy = "System",
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    ModifiedBy = "",
                    LastModified = new DateTime()
                }
            };

            foreach (var r in roles)
            {
                context.Roles.Add(r);
            }

            context.SaveChanges();

            var users = new User[]
            {
                //new User()
                //{
                //    UserName = "admin",
                //    Password = "p@ssword1",
                //    Email = "admin@gmail.com",
                //    RoleId = 1,
                //    CreatedBy = "System",
                //    DateCreated = DateTime.Now,
                //    IsDeleted = false,
                //    ModifiedBy = "",
                //    LastModified = new DateTime()
                //}
            };

            foreach (var u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();
        }
    }
}
