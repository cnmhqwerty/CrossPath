using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPath.Models
{
    internal class AllNotes
    {
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public AllNotes() => LoadNotes();

        public void LoadNotes()
        {
            Notes.Clear();

            //Get the folder the notes are stored in
            string appDataPath = FileSystem.AppDataDirectory;

            //Linq ext to load the notes.txt files
            IEnumerable<Note> notes = Directory.EnumerateFiles(appDataPath, "*.notes.txt").Select(filename => new Note() { Filename = filename, Text = File.ReadAllText(filename), Date = File.GetCreationTime(filename) }).OrderBy(note => note.Date);

            foreach (Note note in notes)
            {
                Notes.Add(note);
            }
        }
    }
}
