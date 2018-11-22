using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myNotesApi.Models
{
    public class NoteItem
    {
        public int id { get; set; }
        public string title { get; set; }
        public string noteInfo { get; set; }
    }
}
