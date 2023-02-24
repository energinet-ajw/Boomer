using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boomer.Domain
{
    public class Boomer
    {
        public Boomer(int id) {
            Id = id;
        }

        public int Id { get; }
    }
}
