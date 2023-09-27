using Microsoft.EntityFrameworkCore;

using Notes.Data.Models;

using System;

namespace Notes.Data.Services
{
    public class NoteService : IAsyncCrudService<Note>
    {
        NotesContext _context;
        public NoteService(NotesContext context)
        {
            _context = context;
        }

        public async Task<Note> CreateAsync(Note model)
        {
            model.CreatedAt = DateTime.Now;
            model.ModifiedAt = model.CreatedAt;

            model = _context.Notes.Add(model).Entity;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task DeleteAsync(Note model)
        {
            _context.Notes.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Note>> FindAsync(Func<Note, bool> predicate)
        {
            return _context.Notes.Where(predicate).ToList();
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            return _context.Notes.ToList();
        }

        public async Task UpdateAsync(Note model)
        {
            model.ModifiedAt = DateTime.Now;

            _context.Notes.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
