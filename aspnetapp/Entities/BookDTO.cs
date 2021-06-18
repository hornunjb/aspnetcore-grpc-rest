using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetapp.Entities
{
    public class BookDTO
    {
        public string title { get; set; }
        public string author { get; set; }
    }

    public class BookListDTO
    {
        public List<BookDTO> books { get; set; }

    }
}
