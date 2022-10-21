using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings.Base
{
    public class EntityBaseMap<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasIndex(x => x.Id);

            builder
                .Property(x => x.CreatedAt)
                .IsRequired();

            builder
                .Property(x => x.UpdatedAt)
                .IsRequired(false);
        }
    }
}
