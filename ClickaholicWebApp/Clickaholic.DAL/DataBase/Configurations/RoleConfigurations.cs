using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClickaholicWebApp.BLL.DomainModel.Entities;

namespace ClickaholicWebApp.DAL.DataBase.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        private int _idCounter = 1;

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
               .ToTable("Roles");

            builder
                .HasData(
                    new Role
                    {
                        Id = _idCounter++,
                        Name = "admin",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Role
                    {
                        Id = _idCounter++,
                        Name = "user",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                     new Role
                     {
                         Id = _idCounter++,
                         Name = "moderator",
                         CreatedAt = DateTime.Now,
                         UpdatedAt = DateTime.Now
                     }

                );
        }
    }
}
