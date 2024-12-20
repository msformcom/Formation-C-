using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class SearchResult<TKey> : ISearchResult<TKey>
    {
        public TKey Id { get; set; }
        public string Libelle { get; set; }
        public string Indication { get; set; }
    }
}
