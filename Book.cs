using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolgozat
{
    class Book
    {
        public string Title { get; private set; }
        public string Genre { get; private set; }

        public Book(string title, string genre)
        {
            Title = title;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"{Title}: {Genre}";
        }
    }
}
