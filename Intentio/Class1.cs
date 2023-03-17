using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intentio
{
    abstract class IUser
    {
        public virtual string ID { get; set; }
        public virtual string Name { get; set; }
    }

    sealed class Child : IUser
    {
    }

    sealed class Parent : IUser 
    {
    }
}
