using Metier.Concession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Disque
{
    public class PersistenceVoitureSurDisque<TKey> : IPersistenceVoiture<TKey> 
    {


        public Task<TKey> AddVoiture(Voiture voiture)
        {
            // 1) S'assure que le dossier de sauvegarde existe => c:\Data
            if (!Directory.Exists(@"c:\Data"))
            {
                Directory.CreateDirectory(@"c:\Data");
            }

            //var dir = new DirectoryInfo(@"c:\Data");
            //if (!dir.Exists)
            //{
            //    dir.Create();
            //}


            // 2) Ouvrir le fichier Catalogue.json => créer si pas exister
            var fi = new FileInfo(@"c:\Data\Catalogue.json");

            Dictionary<TKey, Voiture> data=null;

            
            if (!fi.Exists)
            {
                data = new Dictionary<TKey, Voiture>();
         

                data.Add(default(TKey), voiture);

            }
            else
            {

            }

            // fs est un stream => permet d'ecrire ou lire progressivement dans le fichier
            // IDisposable est l'interface qui est commune à tous les objets
            // Qui necessitent un nettoyage avant d'être orphelin
            using (var fs = fi.Open(FileMode.OpenOrCreate))
            {
                // Prendre l'objet data et le sérialiser dans le stream
                // DataContractSerializer => Xml
                var serializer = new DataContractJsonSerializer(typeof(Voiture));
                // ecriture de l'objet dans le stream
                serializer.WriteObject(fs, voiture);

                // Attention le fs doit être fermé sinon le fichier reste ouvert
                // => plantages assurés

                //fs.Dispose(); // Méthode qui s'assure que toutes les resources extérieur
                              // sont fermées pour cet objet => IDisposable
                              // IDisposable => Dispose()

                // Exception

                //fs.Close();

            }
            // Retourne la clé de la dernière valeur dans le dictionnaire
            return Task.FromResult(data.Last().Key);

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
