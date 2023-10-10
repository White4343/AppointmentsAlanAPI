using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlanAPI.Context;
using AlanAPI.Models;

namespace AlanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramMessagesController : ControllerBase
    {
        private readonly DataContext _context;

        public TelegramMessagesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TelegramMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelegramMessage>>> GetTelegramMessages()
        {
          if (_context.TelegramMessages == null)
          {
              return NotFound();
          }
            return await _context.TelegramMessages.ToListAsync();
        }

        // GET: api/TelegramMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TelegramMessage>> GetTelegramMessage(int id)
        {
          if (_context.TelegramMessages == null)
          {
              return NotFound();
          }
            var telegramMessage = await _context.TelegramMessages.FindAsync(id);

            if (telegramMessage == null)
            {
                return NotFound();
            }

            return telegramMessage;
        }

        // PUT: api/TelegramMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelegramMessage(int id, TelegramMessage telegramMessage)
        {
            if (id != telegramMessage.MessageId)
            {
                return BadRequest();
            }

            _context.Entry(telegramMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelegramMessageExists(id))
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

        // POST: api/TelegramMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TelegramMessage>> PostTelegramMessage(TelegramMessage telegramMessage)
        {
          if (_context.TelegramMessages == null)
          {
              return Problem("Entity set 'DataContext.TelegramMessages'  is null.");
          }
            _context.TelegramMessages.Add(telegramMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTelegramMessage", new { id = telegramMessage.MessageId }, telegramMessage);
        }

        // DELETE: api/TelegramMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelegramMessage(int id)
        {
            if (_context.TelegramMessages == null)
            {
                return NotFound();
            }
            var telegramMessage = await _context.TelegramMessages.FindAsync(id);
            if (telegramMessage == null)
            {
                return NotFound();
            }

            _context.TelegramMessages.Remove(telegramMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelegramMessageExists(int id)
        {
            return (_context.TelegramMessages?.Any(e => e.MessageId == id)).GetValueOrDefault();
        }
    }
}
