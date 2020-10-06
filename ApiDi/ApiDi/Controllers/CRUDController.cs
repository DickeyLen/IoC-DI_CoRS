using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDi.Data;
using ApiDi.Models;

namespace ApiDi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly TodoitemContext _context;

        public CRUDController(TodoitemContext context)
        {
            _context = context;
        }

        // GET: api/CRUD
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todoitem>>> GetTodoitem()
        {
            return await _context.Todoitem.ToListAsync();
        }

        // GET: api/CRUD/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todoitem>> GetTodoitem(long id)
        {
            var todoitem = await _context.Todoitem.FindAsync(id);

            if (todoitem == null)
            {
                return NotFound();
            }

            return todoitem;
        }

        // PUT: api/CRUD/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoitem(long id, Todoitem todoitem)
        {
            if (id != todoitem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoitem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoitemExists(id))
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

        // POST: api/CRUD
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Todoitem>> PostTodoitem([FromBody]Todoitem todoitem)
        {
            _context.Todoitem.Add(todoitem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoitem", new { id = todoitem.Id }, todoitem);
        }

        // DELETE: api/CRUD/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Todoitem>> DeleteTodoitem(long id)
        {
            var todoitem = await _context.Todoitem.FindAsync(id);
            if (todoitem == null)
            {
                return NotFound();
            }

            _context.Todoitem.Remove(todoitem);
            await _context.SaveChangesAsync();

            return todoitem;
        }

        private bool TodoitemExists(long id)
        {
            return _context.Todoitem.Any(e => e.Id == id);
        }
    }
}
