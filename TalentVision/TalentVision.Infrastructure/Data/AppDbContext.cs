using Microsoft.EntityFrameworkCore;
using TalentVision.Domain.Entities;

namespace TalentVision.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Competencia> Competencias => Set<Competencia>();
    public DbSet<UsuarioCompetencia> UsuarioCompetencias => Set<UsuarioCompetencia>();
    public DbSet<Vaga> Vagas => Set<Vaga>();
    public DbSet<VagaCompetencia> VagaCompetencias => Set<VagaCompetencia>();
    public DbSet<Candidatura> Candidaturas => Set<Candidatura>();
    public DbSet<LogAuditoria> LogsAuditoria => Set<LogAuditoria>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // =============== USUARIO ======================
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("USUARIO");

            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario)
                  .HasColumnName("ID_USUARIO");

            entity.Property(e => e.Nome)
                  .HasColumnName("NOME");

            entity.Property(e => e.Email)
                  .HasColumnName("EMAIL");

            entity.Property(e => e.SenhaHash)
                  .HasColumnName("SENHA_HASH");

            entity.Property(e => e.TipoUsuario)
                  .HasColumnName("TIPO_USUARIO");

            entity.Property(e => e.CriadoEm)
                  .HasColumnName("CRIADO_EM");
        });

        // =============== COMPETENCIA ==================
        modelBuilder.Entity<Competencia>(entity =>
        {
            entity.ToTable("COMPETENCIA");

            entity.HasKey(e => e.IdCompetencia);

            entity.Property(e => e.IdCompetencia)
                  .HasColumnName("ID_COMPETENCIA");

            entity.Property(e => e.Nome)
                  .HasColumnName("NOME");

            entity.Property(e => e.Categoria)
                  .HasColumnName("CATEGORIA");
        });

        // =============== USUARIO_COMPETENCIA ==========
        modelBuilder.Entity<UsuarioCompetencia>(entity =>
        {
            entity.ToTable("USUARIO_COMPETENCIA");

            entity.HasKey(e => new { e.IdUsuario, e.Competencia });

            entity.Property(e => e.IdUsuario)
                  .HasColumnName("ID_USUARIO");

            entity.Property(e => e.Competencia)
                  .HasColumnName("COMPETENCIA"); // enum -> NUMBER

            entity.Property(e => e.Nivel)
                  .HasColumnName("NIVEL");
        });

        // =============== VAGA =========================
        modelBuilder.Entity<Vaga>(entity =>
        {
            entity.ToTable("VAGA");

            entity.HasKey(e => e.IdVaga);

            entity.Property(e => e.IdVaga)
                  .HasColumnName("ID_VAGA");

            entity.Property(e => e.Titulo)
                  .HasColumnName("TITULO");

            entity.Property(e => e.Descricao)
                  .HasColumnName("DESCRICAO");

            entity.Property(e => e.Empresa)
                  .HasColumnName("EMPRESA");

            entity.Property(e => e.Localizacao)
                  .HasColumnName("LOCALIZACAO");

            entity.Property(e => e.IdRecrutador)
                  .HasColumnName("ID_RECRUTADOR");

            entity.Property(e => e.PublicadoEm)
                  .HasColumnName("PUBLICADO_EM");
        });

        // =============== VAGA_COMPETENCIA =============
        modelBuilder.Entity<VagaCompetencia>(entity =>
        {
            entity.ToTable("VAGA_COMPETENCIA");

            entity.HasKey(e => new { e.IdVaga, e.Competencia });

            entity.Property(e => e.IdVaga)
                  .HasColumnName("ID_VAGA");

            entity.Property(e => e.Competencia)
                  .HasColumnName("COMPETENCIA");

            entity.Property(e => e.Peso)
                  .HasColumnName("PESO");
        });

        // =============== CANDIDATURA ==================
        modelBuilder.Entity<Candidatura>(entity =>
        {
            entity.ToTable("CANDIDATURA");

            entity.HasKey(e => e.IdCandidatura);

            entity.Property(e => e.IdCandidatura)
                  .HasColumnName("ID_CANDIDATURA");

            entity.Property(e => e.IdUsuario)
                  .HasColumnName("ID_USUARIO");

            entity.Property(e => e.IdVaga)
                  .HasColumnName("ID_VAGA");

            entity.Property(e => e.DataCandidatura)
                  .HasColumnName("DATA_CANDIDATURA");

            entity.Property(e => e.Status)
                  .HasColumnName("STATUS");

            entity.Property(e => e.Compatibilidade)
                  .HasColumnName("COMPATIBILIDADE");
        });

        // =============== LOG_AUDITORIA ================
        modelBuilder.Entity<LogAuditoria>(entity =>
        {
            entity.ToTable("LOG_AUDITORIA");

            entity.HasKey(e => e.IdLog);

            entity.Property(e => e.IdLog)
                  .HasColumnName("ID_LOG");

            entity.Property(e => e.TabelaAfetada)
                  .HasColumnName("TABELA_AFETADA");

            entity.Property(e => e.Operacao)
                  .HasColumnName("OPERACAO");

            entity.Property(e => e.DataOperacao)
                  .HasColumnName("DATA_OPERACAO");

            entity.Property(e => e.IdUsuarioAcao)
                  .HasColumnName("ID_USUARIO_ACAO");

            entity.Property(e => e.Descricao)
                  .HasColumnName("DESCRICAO");
        });

        base.OnModelCreating(modelBuilder);
    }
}
