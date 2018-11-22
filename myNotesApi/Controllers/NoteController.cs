using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myNotesApi.Models;

namespace myNotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly myNotesApiContext _context;

        public NoteController(myNotesApiContext context)
        {
            _context = context;
        }

        // GET: api/Note
        [HttpGet]
        public IEnumerable<NoteItem> GetNoteItem()
        {
            return _context.NoteItem;
        }

        // GET: api/Note/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var noteItem = await _context.NoteItem.FindAsync(id);

            if (noteItem == null)
            {
                return NotFound();
            }

            return Ok(noteItem);
        }

        // PUT: api/Note/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoteItem([FromRoute] int id, [FromBody] NoteItem noteItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noteItem.id)
            {
                return BadRequest();
            }

            _context.Entry(noteItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Note
        [HttpPost]
        public async Task<IActionResult> PostNoteItem([FromBody] NoteItem noteItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.NoteItem.Add(noteItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoteItem", new { id = noteItem.id }, noteItem);
        }

        // DELETE: api/Note/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var noteItem = await _context.NoteItem.FindAsync(id);
            if (noteItem == null)
            {
                return NotFound();
            }

            _context.NoteItem.Remove(noteItem);
            await _context.SaveChangesAsync();

            return Ok(noteItem);
        }

        private bool NoteItemExists(int id)
        {
            return _context.NoteItem.Any(e => e.id == id);
        }

        // GET: api/Note/title

        [HttpGet]
        [Route("title")]
        public async Task<List<NoteItem>> GetTagsItem([FromQuery] string title)
        {
            var memes = from m in _context.NoteItem
                        select m; //get all the memes


            if (!String.IsNullOrEmpty(title)) //make sure user gave a tag to search
            {
                memes = memes.Where(s => s.title.ToLower().Contains(title.ToLower())); // find the entries with the search tag and reassign
            }

            var returned = await memes.ToListAsync(); //return the memes

            return returned;
        }

    }
}