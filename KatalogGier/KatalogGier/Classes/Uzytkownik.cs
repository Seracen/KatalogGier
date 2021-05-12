using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatalogGier.Classes
{
    public class Uzytkownik
    {
        string Login { get; set; }
        string Haslo { get; set; }
        string Wyswietlana_nazwa { get; set; }
        string Ocenione_gry { get; set; }

        public Uzytkownik() { }
        public Uzytkownik(string l, string h, string wn, string og)
        {
            Login = l;
            Haslo = h;
            Wyswietlana_nazwa = wn;
            Ocenione_gry = og;
        }

        public string GetLogin() { return Login; }
        public void SetLogin(string l) { Login = l; }
        public string GetHaslo() { return Haslo; }
        public void SetHaslo(string h) { Haslo = h; }
        public string GetWyswietlana_nazwa() { return Wyswietlana_nazwa; }
        public void SetWyswietlana_nazwa(string wn) { Wyswietlana_nazwa = wn; }
        public string GetOcenione_gry() { return Ocenione_gry; }
        public void SetOcenione_gry(string og) { Ocenione_gry = og; }
        public int GetRateByGameId(int Game_Id)
        {
            string[] w = Ocenione_gry.Split(';');
            foreach(var item in w)
            {
                string[] pom = item.Split(':');
                int ocena = Int32.Parse(pom[1]);
                if(Int32.Parse(pom[0]) == Game_Id)
                {
                    return ocena;
                }
            }
            return -1;
        }
    }
}
