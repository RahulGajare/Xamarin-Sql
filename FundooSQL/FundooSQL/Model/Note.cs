using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooSQL.Model
{
    public class Note
    {
        private string title;
        private string Info;
        private int noteID;

        public string Title { get => title; set => title = value; }
        public string Info1 { get => Info; set => Info = value; }

        [PrimaryKey, AutoIncrement]
        public int NoteID { get => noteID; set => noteID = value; }
    }
}
