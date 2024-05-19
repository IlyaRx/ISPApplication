using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISAuto.Model
{
    public partial class DatabaseAutoContext : DbContext
    {
        private static DatabaseAutoContext _context;

        public static DatabaseAutoContext GetContext()
        {
            if (_context == null)
                _context = new DatabaseAutoContext();
            return _context;
        }
    }
}
