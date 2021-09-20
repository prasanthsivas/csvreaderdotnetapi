using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using csvreaderdotnetapi.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using csvreaderdotnetapi.Models.Repository;

namespace csvreaderdotnetapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptionsController : ControllerBase
    {
        private readonly CaptionContext _context;
        private readonly IDataRepository<Caption> _captionRepository;

        public CaptionsController(IDataRepository<Caption> captionRepository)
        {
            _captionRepository = captionRepository;
        }

        // GET: api/Captions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caption>>> GetCaptions()
        {
            IEnumerable<Caption> captions = _captionRepository.GetAll();
            return Ok(captions);
            //return await _context.Captions.ToListAsync();
        }

        // GET: api/Captions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Caption>> GetCaption(long id)
        {
            Caption caption = _captionRepository.Get(id);

            if (caption == null)
            {
                return NotFound("The Caption record couldn't be found.");
            }

            return caption;
        }

        // PUT: api/Captions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaption(long id, [FromBody] Caption caption)
        {
            if (id != caption.captionId)
            {
                return BadRequest();
            }

            if(caption == null)
            {
                return BadRequest("Caption is null.");
            }

            Caption captionToUpdate = _captionRepository.Get(id);
            if (captionToUpdate == null)
            {
                return NotFound("The Caption couldn't be found.");
            }
            

            try
            {
                _captionRepository.Update(captionToUpdate, caption);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaptionExists(id))
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


        // POST: api/Captions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostCaption(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Please enter a valid file");

            using (StreamReader sReader = new StreamReader(file.OpenReadStream()))
            {
                List<Caption> csvRows = readCSVStream(sReader);
                _captionRepository.AddRange(csvRows);
               return CreatedAtAction(nameof(PostCaption), new { rowCount = csvRows.Count });

            }
        }


        // DELETE: api/Captions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaption(long id)
        {
            var caption = await _context.Captions.FindAsync(id);
            if (caption == null)
            {
                return NotFound();
            }

            _context.Captions.Remove(caption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CaptionExists(long id)
        {
            return _context.Captions.Any(e => e.captionId == id);
        }


        // Read csv rows one line at a time
        private List<Caption> readCSVStream(StreamReader csvStream)
        {
            string currentLine = "";
            // read away the first line to skip the headers
            currentLine = csvStream.ReadLine();
            List<Caption> captionsList = new List<Caption>();

            while ((currentLine = csvStream.ReadLine()) != null)
            {
                string[] lineData = currentLine.Split("| ");
                Caption caption = new Caption();
                caption.imageName = lineData[0];
                caption.commentNumber = Int32.Parse(lineData[1]);
                caption.comment = lineData[2];
                captionsList.Add(caption);
            }
            return captionsList;
        }
    }
}
