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
        public string Titel { get; set; }
        public string Tijd { get; set; }
        public string Datum { get; set; }
        public string Genre { get; set; }
        public int SpeelDuur { get; set; }
        public string Beschrijving { get; set; }
        public string Schermtype { get; set; }
        public FilmRating Rating { get; set; }

        public Film(string titel, string tijd, string datum, string genre, string beschrijving, string schermtype, int speelduur)
        {
            this.Titel = titel;
            this.Tijd = tijd;
            this.Datum = datum;
            this.Genre = genre;
            this.Beschrijving = beschrijving;
            this.Schermtype = schermtype;
            this.SpeelDuur = speelduur;



        }
    }
    public class Stoel
    {
        public int Rij { get; set; }
        public int Nummer { get; set; }
        public int ReserveringsNummer { get; set; }
        public string Naam { get; set; }
        public bool Bezet { get; set; }
        public bool PremiumReservering { get; set; }
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
    public class Ticket
    {
        public IList<Ticket> Tickets { get; set; }

        public double TicketPrijs { get; set; }

        public string TypeTicket { get; set; }

        public Ticket(double TicketPrijs, string TypeTicket)
        {
            this.TicketPrijs = TicketPrijs;
            this.TypeTicket = TypeTicket;
        }
    }
    public class Korting
    {
        public IList<Korting> Kortingen { get; set; }

        public int VerrekendeKorting { get; set; }
        public string TypeKorting { get; set; }

        public Korting(int VerrekendeKorting, string TypeKorting)
        {
            this.VerrekendeKorting = VerrekendeKorting;
            this.TypeKorting = TypeKorting;
        }
    }
}
