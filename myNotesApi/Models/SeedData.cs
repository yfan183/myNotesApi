using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myNotesApi.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new myNotesApiContext(
                serviceProvider.GetRequiredService<DbContextOptions<myNotesApiContext>>()))
            {
                // Look for any movies.
                if (context.NoteItem.Count() > 0)
                {
                    return;   // DB has been seeded
                }

                context.NoteItem.AddRange(
                    new NoteItem
                    {
                        title = "CS101",
                        noteInfo = "blah blah blah blah blah blah blah"
                    }


                );
                context.SaveChanges();
            }
        }
    }
}
