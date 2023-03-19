using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intentio
{
    public abstract class Database
    {
        public static Database Instance { get; private set; }
        
        private Database()
        {
        }
    }
}
