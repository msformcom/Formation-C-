using Metier.Concession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Disque
{
    public class PersistenceVoitureSurDisque<TKey> : IPersistenceVoiture<TKey>
    {
        #region Propriété Data

        private Dictionary<TKey,Voiture> _Data;

        public Dictionary<TKey,Voiture> Data
        {
            get { return _Data; }
            set
            {
                // TODO : Check value
                _Data = value;
            }
        }
        #endregion

        public Task<TKey> AddVoiture(Voiture voiture)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ISearchResult<TKey>>> Find(ISearchVoitureModel filter)
        {
            throw new NotImplementedException();
        }

        public Task<Voiture> GetVoiture(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveVoiture(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task SetVoiture(TKey id, Voiture voiture)
        {
            throw new NotImplementedException();
        }
    }
}
