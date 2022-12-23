using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otzovik.ClassRoom
{
    class Helper
    {
        private static FeerSSQEntities _context;
        public static FeerSSQEntities GetContext()
        {
            if (_context == null)
                _context = new FeerSSQEntities();
            return _context;
        }
    }

}
