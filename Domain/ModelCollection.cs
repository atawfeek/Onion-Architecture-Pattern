using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLS.Domain
{
    public class ModelCollection<T>
    {
        public int Page { get; set; } = 1;

        public bool HasMoreItems { get; set; }

        public IEnumerable<T> Items { get; set; }

    }
}
