using DataAccessLayer.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace DataAccessLayer.Configures
{
    public class TenantConfigure : IEntityTypeConfiguration<Tentant>
    {
        public void Configure(EntityTypeBuilder<Tentant> builder)
        {
            builder.ToTable("Tenant");
            builder.HasMany(t => t.Reviews).WithOne(r => r.Reiewer).OnDelete(DeleteBehavior.NoAction);

            

        }
    }
}
