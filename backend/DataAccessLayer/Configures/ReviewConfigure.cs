using DataAccessLayer.ModelContetxt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace DataAccessLayer.Configures
{
    public class ReviewConfigure : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            

            builder.HasKey(r => r.ReviewID);

           builder
        .HasOne(r => r.Reiewer)
        .WithMany(t => t.Reviews)
        .HasForeignKey(r => r.TenantId)
        .HasPrincipalKey(t => t.UsserId);


        }
    }
}
