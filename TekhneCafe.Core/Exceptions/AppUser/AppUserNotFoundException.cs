using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekhneCafe.Core.Exceptions.AppUser
{
    public class AppUserNotFoundException : NotFoundException
    {
        public AppUserNotFoundException() : base("AppUser not found exception!")
        {

        }

        public AppUserNotFoundException(string message) : base(message)
        {

        }
    }
}
