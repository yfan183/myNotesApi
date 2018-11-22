using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace myNotesApi.Models
{
    public class myNotesApiContext : DbContext
    {
        public myNotesApiContext (DbContextOptions<myNotesApiContext> options)
            : base(options)
        {
        }

        public DbSet<myNotesApi.Models.NoteItem> NoteItem { get; set; }
    }
}
