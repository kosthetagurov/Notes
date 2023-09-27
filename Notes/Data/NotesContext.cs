using Microsoft.EntityFrameworkCore;

using Notes.Data.Models;

namespace Notes.Data
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions<NotesContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
    }
}
