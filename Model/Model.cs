using System;
using System.Collections.Generic;
using System.Text;

namespace JimFilmsTake2.Model
{
    public enum FilmRating
    {
        plus3,
        plus6,
        plus9,
        plus12,
        plus16,
        plus18
    }
    public enum WerknemerFunctie
    {
        Administator,
        Werknemer
    }
    public class Film
    {
        public string Naam { get; set; }
        public FilmRating Rating { get; set; }
        public int SpeelDuur { get; set; }
        public string Beschrijving { get; set; }
        public bool DrieD { get; set; }
    }
    public class Stoel
    {
        public int Rij { get; set; }
        public int Nummer { get; set; }
        public int ReserveringsNummer { get; set; }
        public string Naam { get; set; }
        public bool Beschikbaar { get; set; }
        public Stoel(int rij, int nummer)
        {
            this.Rij = rij;
            this.Nummer = nummer;
        }
    }
    public class Scherm
    {
        public IDictionary<string, Vertoning> Vertoningen { get; set; }
        public int Nummer { get; set; }
        public Scherm(int nummer)
        {
            this.Nummer = nummer;
            this.Vertoningen = new Dictionary<string, Vertoning>();
        }
    }
    public class Vertoning
    {
        public int Prijs { get; set; }
        public Film Film { get; set; }
        public IList<Stoel> Zitplaatsen { get; set; }
        public DateTime AanvangsTijd { get; set; }
        public Vertoning(int rijen, int stoelen, DateTime aanvangsTijd)
        {

            this.AanvangsTijd = aanvangsTijd;

            this.Zitplaatsen = new List<Stoel>();
            for (var r = 0; r < rijen; r++)
            {
                for (var s = 0; s < stoelen; s++)
                {

                    this.Zitplaatsen.Add(new Stoel(r, s));
                }
            }

        }
    }
    public class Werknemer
    {
        public int Nummer { get; set; }
        public WerknemerFunctie Functie { get; set; }
        public string Naam { get; set; }
    }
    public class Bioscoop
    {
        // owned members
        public IList<Film> BeschikbareFilms { get; set; }
        public IList<Scherm> Schermen { get; set; }
        public IList<Werknemer> Werknemers { get; set; }
        // props
        public string Naam { get; set; }
        public string Locatie { get; set; }

        public Bioscoop(string naam, string locatie)
        {
            this.BeschikbareFilms = new List<Film>();
            this.Schermen = new List<Scherm>();
            this.Werknemers = new List<Werknemer>();
            this.Naam = naam;
            this.Locatie = locatie;
        }
    }
}
