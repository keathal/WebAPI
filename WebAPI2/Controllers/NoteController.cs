using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        readonly NoteDbContext _context;

        public NoteController(NoteDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Note>> Get(int code = 0)
        {
            return code == 0 ? await _context.Notes.ToListAsync() : await _context.Notes.Where(a => a.Code == code).ToListAsync();
        } 

        [HttpPost]
        public async Task<IEnumerable<Note>?> Post([FromBody] List<PostObject> postObjects)
        {
            if (postObjects == null) return null;

            _context.Notes.RemoveRange(_context.Notes);

            postObjects.Sort((x,y) => x.code.CompareTo(y.code));

            List<Note> notes = new List<Note>();

            foreach (PostObject postObject in postObjects)
            {
                notes.Add(new Note(postObject.code, postObject.value));
            }

            await _context.Notes.AddRangeAsync(notes);
            await _context.SaveChangesAsync();

            return await _context.Notes.ToListAsync();
        }


        public class PostObject
        {
            public int code { get; set; }
            public string value { get; set; }
        }
    }

    
}
