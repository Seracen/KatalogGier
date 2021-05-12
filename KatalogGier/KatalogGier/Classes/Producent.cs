using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatalogGier.Classes
{
    public class Producent
    {
        int Producent_Id { get; set; }
        string Nazwa { get; set; }
        string Kraj { get; set; }
        List<Gra> listaGier { get; set; }

        public Producent() { }
        public Producent(int pi, string n, string k,string wg)
        {
            Producent_Id = pi;
            Nazwa = n;
            Kraj = k;
            if (wg != "")
            {
                listaGier = new List<Gra>();
                string[] w = wg.Split(';');
                foreach(var id in w)
                {

                    listaGier.Add(DbConnection.GetGraById(Int32.Parse(id)));
                }
                
            }
        }

        public int GetProducent_Id() { return Producent_Id; }
        public void SetProducent_Id(int pi) { Producent_Id = pi; }
        public string GetNazwa() { return Nazwa; }
        public void SetNazwa(string n) { Nazwa = n; }
        public string GetKraj() { return Kraj; }
        public void SetKraj(string k) { Kraj = k; }
        public List<Gra> GetListaGier() { return listaGier; }
        public void SetListaGier(List<Gra> lista) { listaGier = lista; }
    }
}
