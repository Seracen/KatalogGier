using KatalogGier.Classes;
using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KatalogGier
{
    public class DbConnection
    {
        public static string connectionString { get; private set; } = "mongodb://localhost";
        public static MongoClient client = new MongoClient(connectionString);
        public static IMongoDatabase db = client.GetDatabase("KatalogGier");
        public static IMongoCollection<BsonDocument> collectionGra = db.GetCollection<BsonDocument>("Gra");
        public static IMongoCollection<BsonDocument> collectionProducent = db.GetCollection<BsonDocument>("Producent");
        static IMongoCollection<BsonDocument> collectionUzytkownik = db.GetCollection<BsonDocument>("Uzytkownik");
        public DbConnection() { }

        public static List<Gra> GetGraList()
        {
            var list = collectionGra.Find(new BsonDocument()).ToList();
            List<Gra> listaGier = new List<Gra>();
            Gra pom;
            foreach (var item in list)
            {
                pom = new Gra(item.AsBsonValue["Gra_Id"].AsInt32, item.AsBsonValue["Nazwa"].AsString, item.AsBsonValue["Kategoria_wiekowa"].AsString,
                    item.AsBsonValue["Typ"].AsString, item.AsBsonValue["Wymagania"].AsString, Double.Parse(item.AsBsonValue["Srednia_ocena"].AsString), item.AsBsonValue["Ilosc_ocen"].AsInt32,
                    item.AsBsonValue["Gra_Id"].AsInt32 + ".jpg", item.AsBsonValue["Link"].AsString, item.AsBsonValue["Opis"].AsString);
                listaGier.Add(pom);
            }
            return listaGier;
        }
        public static List<Producent> GetProducentList()
        {
            var list = collectionProducent.Find(new BsonDocument()).ToList();
            List<Producent> listaProducentow = new List<Producent>();
            Producent pom;
            foreach (var item in list)
            {
                pom = new Producent(item.AsBsonValue["Producent_Id"].AsInt32, item.AsBsonValue["Nazwa"].AsString, item.AsBsonValue["Kraj"].AsString, item.AsBsonValue["wyprodukowane_gry"].AsString);
                listaProducentow.Add(pom);
            }
            return listaProducentow;
        }
        public static IMongoCollection<BsonDocument> GetProducentCollection()
        {
            return collectionProducent;
        }

        public static Uzytkownik VerifyUser(string Login, string Haslo)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            Uzytkownik uzytkownik = null;
            var userFilter = filterBuilder.Eq("Login", Login) & filterBuilder.Eq("Haslo", Haslo);
            var projectionUser = Builders<BsonDocument>.Projection.Include("Login").Include("Haslo").Include("Wyswietlana_nazwa").Include("Ocenione_gry");
            var userDocument = collectionUzytkownik.Find(userFilter).Project(projectionUser).ToList();
            if (userDocument.Count != 0)
            {
                uzytkownik = new Uzytkownik(userDocument[0].AsBsonValue["Login"].AsString, userDocument[0].AsBsonValue["Haslo"].AsString,
                    userDocument[0].AsBsonValue["Wyswietlana_nazwa"].AsString, userDocument[0].AsBsonValue["Ocenione_gry"].AsString);
            }
            return uzytkownik;
        }
        private static bool CheckLogin(string Login)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var userFilter = filterBuilder.Eq("Login", Login);
            var userDocument = collectionUzytkownik.Find(userFilter).ToList();
            return userDocument.Count == 0;
        }
        public static void RegisterUser(string Login, string Haslo, string Wyswietlana_nazwa)
        {
            BsonDocument document = null;
            if (CheckLogin(Login))
            {
                document = new BsonDocument { { "Login", Login }, { "Haslo", Haslo }, { "Wyswietlana_nazwa", Wyswietlana_nazwa }, { "Ocenione_gry", "" } };
                collectionUzytkownik.InsertOne(document);
            }
        }
        public static Gra GetGraById(int Gra_Id)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var graFilter = filterBuilder.Eq("Gra_Id", Gra_Id);
            var graDocument = collectionGra.Find(graFilter).ToList();
            Gra gra = new Gra(graDocument[0].AsBsonValue["Gra_Id"].AsInt32, graDocument[0].AsBsonValue["Nazwa"].AsString, graDocument[0].AsBsonValue["Kategoria_wiekowa"].AsString,
                graDocument[0].AsBsonValue["Typ"].AsString, graDocument[0].AsBsonValue["Wymagania"].AsString, Double.Parse(graDocument[0].AsBsonValue["Srednia_ocena"].AsString),
                graDocument[0].AsBsonValue["Ilosc_ocen"].AsInt32, graDocument[0].AsBsonValue["Gra_Id"].AsInt32 + ".jpg",
                graDocument[0].AsBsonValue["Link"].AsString, graDocument[0].AsBsonValue["Opis"].AsString);
            return gra;
        }
        public static List<Gra> GetRatedGames(Uzytkownik uzytkownik)
        {
            List<Gra> listaOcenionychGier = null;
            Gra gra = null;
            if (uzytkownik.GetOcenione_gry() != "")
            {
                listaOcenionychGier = new List<Gra>();
                string[] gry = uzytkownik.GetOcenione_gry().Split(';');
                foreach (var item in gry)
                {
                    if (item != "")
                    {
                        string[] w = item.Split(':');
                        gra = GetGraById(Int32.Parse(w[0]));
                        listaOcenionychGier.Add(gra);
                    }
                }
            }
            return listaOcenionychGier;
        }

        public static void UpdateUser(Uzytkownik uzytkownik, string l, string h, string n)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var userFilter = filterBuilder.Eq("Login", uzytkownik.GetLogin());
            var update = Builders<BsonDocument>.Update.Set("Login", l).Set("Haslo", h).Set("Wyswietlana_nazwa", n);
            collectionUzytkownik.UpdateOne(userFilter, update);
            MessageBox.Show("Pomyślnie zmodyfikowane dane!");
        }

        public static Producent GetProducentByGameId(int game_id)
        {
            List<Producent> listaProducentow = GetProducentList();
            foreach (var producent in listaProducentow)
            {
                foreach (var game in producent.GetListaGier())
                {
                    if (game.GetGra_Id() == game_id)
                        return producent;
                }
            }
            return null;
        }
        public static void OcenGre(Uzytkownik uzytkownik, Gra gra, int ocena)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var userFilter = filterBuilder.Eq("Login", uzytkownik.GetLogin());
            var update = Builders<BsonDocument>.Update.Set("Ocenione_gry", uzytkownik.GetOcenione_gry() + gra.GetGra_Id() + ":" + ocena + ";");
            double sumaOcen = gra.GetIlosc_ocen() * gra.GetSrednia_ocena();
            double ilosc = gra.GetIlosc_ocen() + 1;
            double zmienionaOcena = (sumaOcen + ocena) / ilosc;
            var graFilter = filterBuilder.Eq("Gra_Id", gra.GetGra_Id());
            var updateGra = Builders<BsonDocument>.Update.Set("Srednia_ocena", Math.Round(zmienionaOcena, 2) + "").Set("Ilosc_ocen", gra.GetIlosc_ocen() + 1);
            collectionUzytkownik.UpdateOne(userFilter, update);
            collectionGra.UpdateOne(graFilter, updateGra);
            MessageBox.Show("Pomyślnie oceniono gre!");
        }
        public static void UsunOcene(Uzytkownik uzytkownik, Gra gra)
        {
            string oceny = uzytkownik.GetOcenione_gry();
            string[] w = oceny.Split(';');
            string nowa = "";
            int ocena = -1;
            foreach (var a in w)
            {
                if (a != "")
                {
                    string[] gra_ocena = a.Split(':');
                    if (gra_ocena[0] == (gra.GetGra_Id() + ""))
                    {
                        ocena = Int32.Parse(gra_ocena[1]);
                    }
                    else
                    {
                        nowa += gra_ocena[0] + ':' + gra_ocena[1] + ";";
                    }
                }
            }
            var filterBuilder = Builders<BsonDocument>.Filter;
            var userFilter = filterBuilder.Eq("Login", uzytkownik.GetLogin());
            var updateUser = Builders<BsonDocument>.Update.Set("Ocenione_gry", nowa);
            double ilosc = gra.GetIlosc_ocen() - 1;
            double suma = ((gra.GetSrednia_ocena() * gra.GetIlosc_ocen()) - ocena);
            double srednia = suma/ ilosc;
            var graFilter = filterBuilder.Eq("Gra_Id", gra.GetGra_Id());
            var updateGra = Builders<BsonDocument>.Update.Set("Ilosc_ocen", gra.GetIlosc_ocen() - 1).Set("Srednia_ocena", Math.Round(srednia) + "");
            collectionUzytkownik.UpdateOne(userFilter, updateUser);
            collectionGra.UpdateOne(graFilter, updateGra);

        }
        public async static void UsunKonto(Uzytkownik uzytkownik)
        {
            List<Gra> listaGier = GetRatedGames(uzytkownik);
            if (listaGier != null)
                foreach (var game in listaGier)
                {
                    UsunOcene(uzytkownik, game);
                }
            var filterBuilder = Builders<BsonDocument>.Filter;
            var userFilter = filterBuilder.Eq("Login", uzytkownik.GetLogin());
            await collectionUzytkownik.DeleteOneAsync(userFilter);
        }
    }
}
