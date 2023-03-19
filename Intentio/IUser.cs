using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intentio
{
    internal interface IUser
    {
        string Username { get; }
        string Password { get; }


        record Child(string Username, string Password) : IUser
        {
        }

        record Parent(string Username, string Password) : IUser
        {
        }

    }
}
