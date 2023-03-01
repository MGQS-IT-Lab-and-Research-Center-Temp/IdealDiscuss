using IdealDiscuss.Dtos.UserDto;
using IdealDiscuss.Entities;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;

namespace IdealDiscuss.Context
{
    public class AppDbinitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<IdealDiscussContext>();

                context.Database.EnsureCreated();
                //seedingrole
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<Role>());
                    {
                        new Role()
                        {
                            RoleName = "Admin",
                            Description = "Role for admin",
                            CreatedBy = "System",
                            DateCreated = DateTime.Now,
                            IsDeleted = false,
                            ModifiedBy = "System",
                            LastModified = DateTime.Now
                        };
                        new Role()
                        {
                            RoleName = "AppUser",
                            Description = "Role for nominal user",
                            CreatedBy = "System",
                            DateCreated = DateTime.Now,
                            IsDeleted = false,
                            ModifiedBy = "System",
                            LastModified = DateTime.Now
                        };
                    }
                    context.SaveChanges();
                }
                //seedinguser
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>());{
                        new User()
                        {
                            UserName = "Admin",
                            Password = "Admin@123#",
                            Email = "Admin@gmail.com",
                            RoleId = 1,
                            CreatedBy = "System",
                            DateCreated = DateTime.Now,
                            IsDeleted = false,
                            ModifiedBy = "System",
                            LastModified = DateTime.Now
                        };
                        new User()
                        {
                            UserName = "JohnDoe",
                            Password = "john@123#",
                            Email = "johndoe@gmail.com",
                            RoleId = 2,
                            CreatedBy = "System",
                            DateCreated = DateTime.Now,
                            IsDeleted = false,
                            ModifiedBy = "System",
                            LastModified = DateTime.Now
                        };
                    }
                    context.SaveChanges();
                }
             
            }
        }
    }
}
