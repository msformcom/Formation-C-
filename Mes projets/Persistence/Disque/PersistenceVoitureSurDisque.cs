using Metier.Concession;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Disque
{

    // cette fonction est un générateur d'id
    // parametre : Un catalogue de voiture
    // return => un nouvel id
    // Cela va servir pour la génération des id
    public delegate TKey GenerateurId<TKey>(Dictionary<TKey, Voiture> catalogue);

    public class SearchResult<TKey> : ISearchResult<TKey>
    {
        public TKey Id { get; set; }
        public string Libelle { get; set; }
        public string Indication { get; set; }
    }

    public class PersistenceVoitureSurDisque<TKey> : IPersistenceVoiture<TKey> 
    {
        private readonly IConfiguration config;
        private readonly GenerateurId<TKey> generateNextId;

        // Cette classe va avoir besoin d'accéder à la configuration
        // Cette config peut être réalisée de différente maniere
        // appsettings.json ou app.config ou BDD ou variables environnement
        // Mais ici Je ne veux pas savoir => faiblement couplé
        public PersistenceVoitureSurDisque(
            IConfiguration config,
            GenerateurId<TKey> generateNextId
            )
        {
            this.config = config;
            this.generateNextId = generateNextId;
        }



        Dictionary<TKey, Voiture> RestoreCatalogue()
        {
            // Je vais chercher les infos dans la config
            // je ne sais pas dans quoi s'est enregistré
            var directoryPath = config.GetSection("directoryPath").Value;
            var fileName = config.GetSection("fileName").Value;
            var fi = new FileInfo(Path.Combine(directoryPath,fileName));
            if (!fi.Exists)
            {
                return new Dictionary<TKey, Voiture>();
            }

            // Le fichier existe
            using (var fs = fi.OpenRead())
            {
                var serializer = new DataContractJsonSerializer(typeof(Dictionary<TKey, Voiture>));
                var catalogue = (Dictionary<TKey,Voiture>)serializer.ReadObject(fs);
                return catalogue;
            }


           

        }

        private void SauvegardeCatalogue(Dictionary<TKey, Voiture> catalogue)
        {
            var directoryPath = config.GetSection("directoryPath").Value;
            var fileName = config.GetSection("fileName").Value;
            // 1) S'assure que le dossier de sauvegarde existe => c:\Data
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var fi = new FileInfo(Path.Combine(directoryPath,fileName));
            // fs est un stream => permet d'ecrire ou lire progressivement dans le fichier
            // IDisposable est l'interface qui est commune à tous les objets
            // Qui necessitent un nettoyage avant d'être orphelin
            using (var fs = fi.Open(FileMode.OpenOrCreate))
            {
                // Prendre l'objet data et le sérialiser dans le stream
                // DataContractSerializer => Xml
                var serializer = new DataContractJsonSerializer(typeof(Dictionary<TKey, Voiture>));
                // ecriture de l'objet dans le stream
                serializer.WriteObject(fs, catalogue);

                // Attention le fs doit être fermé sinon le fichier reste ouvert
                // => plantages assurés

                //fs.Dispose(); // Méthode qui s'assure que toutes les resources extérieur
                // sont fermées pour cet objet => IDisposable
                // IDisposable => Dispose()

                // Exception

                //fs.Close();

            }
        }

        public Task<TKey> AddVoiture(Voiture voiture)
        {
            var catalogue = RestoreCatalogue();
            TKey nextId=generateNextId(catalogue);
            catalogue.Add(nextId, voiture);
            SauvegardeCatalogue(catalogue);

            // Retourne la clé de la dernière valeur dans le dictionnaire
            return Task.FromResult(catalogue.Last().Key);
        }

        public Task<IEnumerable<ISearchResult<TKey>>> Find(ISearchVoitureModel filter)
        {
            var catalogue = RestoreCatalogue();
            // Ici, vue est un ienumerable à partir du dcitionnaire
            var vue = catalogue.AsEnumerable();
            // Si PrixMin est renseigné, j'ajoute un filtre à vue 
            if (filter.PrixMin != null)
            {
                vue = vue.Where(v => v.Value.Prix <= filter.PrixMin);
            }
            // Si texte renseigné, j'ajoute un filtre à vue 
            if (filter.Texte !=null )
            {
                vue=vue.Where(v => v.Value.Marque.Contains(filter.Texte));
            }
            // 
            return Task.FromResult(vue    // vue = les couples clé/valeurs retenus dans le dictionnaire
                                    .Select(keyValue=> // Je transforme couple clé/valeur en SearchResult<TKey>
                                                        new SearchResult<TKey>() 
                                                        { Id=keyValue.Key,
                                                            Libelle=keyValue.Value.Modele,
                                                            Indication= keyValue.Value.Prix.ToString("{0:C}") 
                                                        }as ISearchResult<TKey>
            ) );
                
        }

        public Task<Voiture> GetVoiture(TKey id)
        {
            var catalogue = RestoreCatalogue();
            return Task.FromResult(catalogue[id]);
        }

        public Task RemoveVoiture(TKey id)
        {
            var catalogue = RestoreCatalogue();
            catalogue.Remove(id);
            SauvegardeCatalogue(catalogue);
            return Task.CompletedTask;
        }

        public Task SetVoiture(TKey id, Voiture voiture)
        {
            var catalogue = RestoreCatalogue();
            catalogue[id] = voiture;
            SauvegardeCatalogue(catalogue);
            return Task.CompletedTask;
        }
    }
}
