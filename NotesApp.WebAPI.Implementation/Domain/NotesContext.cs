using Microsoft.EntityFrameworkCore;
using NotesApp.Lib.Shared;
using NotesApp.WebAPI.Implementation.Domain.Entities;
using NotesApp.WebAPI.Implementation.Domain.Interfaces;

namespace NotesApp.WebAPI.Implementation.Domain
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions<NotesContext> contextOptions) : base(contextOptions)
        {

        }

        public virtual DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Note>()
                .Property(n => n.CreatedAt)
                .HasField("_createdAt");

            modelBuilder.Entity<Note>()
                .Property(n => n.LastUpdatedAt)
                .HasField("_lastUpdatedAt");

            modelBuilder.Entity<Note>().Property<DateTime>("CreatedAt");
            modelBuilder.Entity<Note>().Property<DateTime>("LastUpdatedAt");

            modelBuilder.Entity<Note>()
                .Property(n => n.Title)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .Property(n => n.Title)
                .HasMaxLength(255);

            modelBuilder.Entity<Note>()
                .Property(n => n.Description)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .Property(n => n.Description)
                .HasMaxLength(2000);

            modelBuilder.Entity<Note>()
                .Property(n => n.Type)
                .HasDefaultValue(NoteType.Normal);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.CurrentValues["LastUpdatedAt"] = now;
                            break;

                        case EntityState.Added:
                            entry.CurrentValues["CreatedAt"] = now;
                            entry.CurrentValues["LastUpdatedAt"] = now;
                            break;
                    }
                }
            }
        }
    }
}