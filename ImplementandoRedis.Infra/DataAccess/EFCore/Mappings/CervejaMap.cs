using ImplementandoRedis.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImplementandoRedis.Infra.DataAccess.EFCore.Mappings;

public class CervejaMap : IEntityTypeConfiguration<Cerveja>
{
    public void Configure(EntityTypeBuilder<Cerveja> builder)
    {
        builder.ToTable("Cerveja");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.Fabricante)
            .IsRequired()
            .HasColumnName("Fabricante")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.Artesanal)
            .IsRequired()
            .HasColumnName("Artesanal")
            .HasColumnType("bit")
            .HasDefaultValue(0);

        builder.Property(x => x.TipoCervejaId)
            .IsRequired()
            .HasColumnName("TipoId")
            .HasColumnType("INT");

        builder.Property(x => x.Descricao)
            .HasColumnName("Descricao")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.Artesanal)
            .HasColumnName("AnoLancamento")
            .HasColumnType("int");

        builder.Property(x => x.DataCriacao)
            .IsRequired()
            .HasColumnName("DataCriacao")
            .HasColumnType("DATETIME");

        builder.Property(x => x.DataAtualizacao)
            .HasColumnName("DataAtualizacao")
            .HasColumnType("DATETIME");


        builder.HasOne(x => x.TipoCerveja)
            .WithMany(x => x.Cerveja)
            .HasConstraintName("FK_Cerveja_TipoCerveja");
    }
}
