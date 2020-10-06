using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiDi.Models;

namespace ApiDi.Data
{
    public class TodoitemContext : DbContext
    {
        public TodoitemContext (DbContextOptions<TodoitemContext> options)
            : base(options)
        {
        }

        public DbSet<ApiDi.Models.Todoitem> Todoitem { get; set; }
    }
}
