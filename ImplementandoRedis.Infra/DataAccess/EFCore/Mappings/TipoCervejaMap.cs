using ImplementandoRedis.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImplementandoRedis.Infra.DataAccess.EFCore.Mappings;

public class TipoCervejaMap : IEntityTypeConfiguration<TipoCerveja>
{
    public void Configure(EntityTypeBuilder<TipoCerveja> builder)
    {
        builder.ToTable("TipoCerveja");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.Origem)
            .HasColumnName("Origem")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.Coloracao)
            .HasColumnName("Coloracao")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.TeorAlcoolico)
            .HasColumnName("TeorAlcoolico")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.Fermentacao)
            .HasColumnName("Fermentacao")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnName("Descricao")
            .HasColumnType("VARCHAR")
            .HasDefaultValue(1000);
    }
}
