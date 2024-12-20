using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.BDD.DAO;

namespace VoitureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoitureController : ControllerBase
    {
        private readonly ConcessionContext _context;

        public VoitureController(ConcessionContext context)
        {
            _context = context;
        }

        // GET: api/Voiture
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetVoitures()
        {
            return await _context.Voitures
                .Select(dao=>new {
                    id=dao.Id,
                    libelle=dao.Modele,
                    description = dao.Marque
                })
                
                .ToListAsync();
        }

        // GET: api/Voiture/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoitureDAO>> GetVoitureDAO(Guid id)
        {
            var voitureDAO = await _context.Voitures.FindAsync(id);

            if (voitureDAO == null)
            {
                return NotFound();
            }

            return voitureDAO;
        }

        // PUT: api/Voiture/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoitureDAO(Guid id, VoitureDAO voitureDAO)
        {
            if (id != voitureDAO.Id)
            {
                return BadRequest();
            }

            _context.Entry(voitureDAO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoitureDAOExists(id))
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

        // POST: api/Voiture
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VoitureDAO>> PostVoitureDAO(VoitureDAO voitureDAO)
        {
            _context.Voitures.Add(voitureDAO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoitureDAO", new { id = voitureDAO.Id }, voitureDAO);
        }

        // DELETE: api/Voiture/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoitureDAO(Guid id)
        {
            var voitureDAO = await _context.Voitures.FindAsync(id);
            if (voitureDAO == null)
            {
                return NotFound();
            }

            _context.Voitures.Remove(voitureDAO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoitureDAOExists(Guid id)
        {
            return _context.Voitures.Any(e => e.Id == id);
        }
    }
}
