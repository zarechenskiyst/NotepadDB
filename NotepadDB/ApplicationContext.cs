using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadDB
{
    class ApplicationContext: DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<File> Files { get; set; }
    }
}
