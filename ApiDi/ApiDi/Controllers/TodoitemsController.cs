using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDi.Data;
using ApiDi.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace ApiDi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoitemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodoitemsController> _logger;
        private readonly IWebHostEnvironment _env;

        public TodoitemsController(TodoContext context,ILogger<TodoitemsController> logger,IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }

        // GET: api/Todoitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todoitem>>> Gettodoitems()
        {
            return await _context.todoitems.ToListAsync();
        }

        // GET: api/Todoitems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todoitem>> GetTodoitem(long id)
        {
            var todoitem = await _context.todoitems.FindAsync(id);

            if (todoitem == null)
            {
                return NotFound();
            }

            return todoitem;
        }

        // PUT: api/Todoitems/5
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

        // POST: api/Todoitems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Todoitem>> PostTodoitem([FromBody]Todoitem todoitem)
        {
            if (_env.EnvironmentName == "Development")
            {
                _logger.LogWarning(2001, "Post方法被呼叫，傳入資料為:" + JsonConvert.SerializeObject(todoitem));
            }
           
            _context.todoitems.Add(todoitem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoitem", new { id = todoitem.Id }, todoitem);
        }

        // DELETE: api/Todoitems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Todoitem>> DeleteTodoitem(long id)
        {
            var todoitem = await _context.todoitems.FindAsync(id);
            if (todoitem == null)
            {
                return NotFound();
            }

            _context.todoitems.Remove(todoitem);
            await _context.SaveChangesAsync();

            return todoitem;
        }

        private bool TodoitemExists(long id)
        {
            return _context.todoitems.Any(e => e.Id == id);
        }
    }
}
