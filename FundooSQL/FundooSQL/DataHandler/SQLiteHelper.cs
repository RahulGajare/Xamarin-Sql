using FundooSQL.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooSQL.DataHandler
{
   public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Note>().Wait();
        }

        public Task<int> SaveItemAsync(Note note)
        {
            if (note.NoteID != 0)
            {
                return db.UpdateAsync(note);
            }
            else
            {
                return db.InsertAsync(note);
            }
        }

        public  Task<List<Note>> GetItemsAsync()
        {
            return db.Table<Note>().ToListAsync();
        }

        public async Task<Note> GetItemAsync(string noteId)
        {
            int id = Convert.ToInt32(noteId);


            return await db.Table<Note>().Where(i => i.NoteID == id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteItemAsync(Note note)
        {
            return db.DeleteAsync(note);
        }

    }
}
