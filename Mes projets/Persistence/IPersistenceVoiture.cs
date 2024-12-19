using Metier.Concession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface ISearchVoitureModel
    {
        public Decimal? PrixMin { get; set; }
        public string? Texte { get; set; }
    }

    public interface ISearchResult<TKey>
    {
        public TKey Id { get; set; }
        public string Libelle { get; set; }
        public string Indication { get; set; }

    }

    public interface IPersistenceVoiture<Tkey>
    {

        // SCRUD

        // Le type de l'iD est laissé à définir à l'utilisation
        // La voiture sera obtenu via une Task

        // Le retour doit
        //1) etre un ensemble
        // 2) chaque élément de cet ensemble=> Id, Libele, Indication
        Task<IEnumerable<ISearchResult<Tkey>>> Find(ISearchVoitureModel filter);
        Task<Voiture> GetVoiture(Tkey id);
        Task RemoveVoiture(Tkey id);
        Task SetVoiture(Tkey id, Voiture voiture);
        Task<Tkey> AddVoiture(Voiture voiture);


    }
}
