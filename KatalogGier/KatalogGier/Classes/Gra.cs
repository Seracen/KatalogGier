using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KatalogGier.Classes
{
    public class Gra
    {
        public int Gra_Id { get; set; }
        public string Nazwa { get; set; }
        string Kategoria_wiekowa { get; set; }
        public string Typ { get; set; }
        string Wymagania { get; set; }
        public double Srednia_ocena { get; set; }
        int Ilosc_ocen { get; set; }
        public BitmapImage Image { get; set; }
        string Link { get; set; }
        public string Opis { get; set; } 

        public Gra() { }
        public Gra(int gi, string n, string kw, string t, string w, double so, int io,string i,string l, string o)
        {
            Gra_Id = gi;
            Nazwa = n;
            Kategoria_wiekowa = kw;
            Typ = t.Replace(';',' ');
            Wymagania = w;
            Srednia_ocena = so;
            Ilosc_ocen = io;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("/Icons/" + i, UriKind.Relative);
            bi.EndInit();
            Image = bi;
            Link = l;
            Opis = o;
        }
        public int GetGra_Id() { return Gra_Id; }
        public void SetGra_Id(int gi) { Gra_Id = gi; }
        public string GetNazwa() { return Nazwa; }
        public void SetNazwa(string n) { Nazwa = n; }
        public string GetKategoria_wiekowa() { return Kategoria_wiekowa; }
        public void SetKategoria_wiekowa(string kw) { Kategoria_wiekowa = kw; }
        public string GetTyp() { return Typ; }
        public void SetTyp(string t) { Typ = t; }
        public string GetWymagania() { return Wymagania; }
        public void SetWymagania(string w) { Wymagania = w; }
        public double GetSrednia_ocena() { return Srednia_ocena; }
        public void SetSrednia_ocena(float so) { Srednia_ocena = so; }
        public int GetIlosc_ocen() { return Ilosc_ocen; }
        public void SetIlosc_ocen(int io) { Ilosc_ocen = io; }
        public BitmapImage GetImage() { return Image; }
        public void SetImage(BitmapImage i) { Image = i; }
        public string GetLink() { return Link; }
        public void SetLink(string l) { Link = l; }
        public string GetOpis() { return Opis; }
        public void SetOpis(string o) { Opis = o; }
    }
}
