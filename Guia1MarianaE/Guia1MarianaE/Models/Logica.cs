using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guia1MarianaE.Models
{
    public class Logica
    {
        public bool paroimpar(int numero)
        {
            if ((numero % 2) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
