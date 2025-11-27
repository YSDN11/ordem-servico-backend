using Microsoft.EntityFrameworkCore;
using ordem_servico_backend.Models;

namespace ordem_servico_backend.Repositories.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<OrderTasksCompleted> OrderTasksCompleted { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(o => o.Id);

                entity.Property(o => o.Nome).HasMaxLength(255).IsRequired();
                entity.Property(o => o.Endereco).HasMaxLength(255).IsRequired();
                entity.Property(o => o.Cliente).HasMaxLength(255).IsRequired();
                entity.Property(o => o.Descricao).IsRequired();
                entity.Property(o => o.ObservacoesTecnico);
                entity.Property(o => o.QtdFotos).HasDefaultValue(0);
            });

            // TaskItem
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Titulo).HasMaxLength(255).IsRequired();
            });

            // Photo
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photos");
                entity.HasKey(f => f.Id);

                entity.Property(f => f.ConteudoBase64).IsRequired();
                entity.Property(f => f.SmallIndex).IsRequired();

                entity.HasOne(f => f.Order)
                      .WithMany(o => o.Fotos)
                      .HasForeignKey(f => f.OrderId);
            });

            // OrderTasksCompleted (tabela de ligação)
            modelBuilder.Entity<OrderTasksCompleted>(entity =>
            {
                entity.ToTable("OrderTasksCompleted");

                entity.HasKey(ot => new { ot.OrderId, ot.TaskId });

                entity.HasOne(ot => ot.Order)
                      .WithMany(o => o.TarefasConcluidas)
                      .HasForeignKey(ot => ot.OrderId);

                entity.HasOne(ot => ot.Task)
                      .WithMany(t => t.OrdersCompleted)
                      .HasForeignKey(ot => ot.TaskId);
            });
        }
    }
}
