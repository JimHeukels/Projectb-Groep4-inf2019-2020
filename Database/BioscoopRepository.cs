using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JimFilmsTake2.Model;
using Newtonsoft.Json;
using ProjectB.Utils;

namespace JimFilmsTake2.Db
{
    public class BioscoopRepository
    {

        private JsonModel _database { get; set; }






        public BioscoopRepository()
        {

            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var FILEPATH = Path.Combine(projectFolder, @"Database/db.json");
            var jsonString = File.ReadAllText(FILEPATH);
            this._database = new JsonModel();
            if (jsonString == "")
            {
               UpdateData();
            }
            else
            {
                this._database = JsonConvert.DeserializeObject<JsonModel>(jsonString);
            }
        }
        public void UpdateData()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var FILEPATH = Path.Combine(projectFolder, @"db.json");
            File.WriteAllText(FILEPATH, JsonConvert.SerializeObject(this._database));
        }

        

        public void AddBioscoop(Bioscoop bioscoop)
        {
            // Checken of de bioscoop niet al bestaat, dit doet de contains functie voor ons.
            if (!_database.Bioscopen.Contains(bioscoop))
            {
                // bestaat deze niet al voegen we hem toe
                this._database.Bioscopen.Add(bioscoop);
            }
            // bestaat die wel doen we een error gooien met info over de desbetreffende bioscoop
            else
            {
                throw new Exception($"Bioscoop met naam {bioscoop.Naam} bestaat al in de database");
            }
        }
        public void AddScherm(Bioscoop bioscoop, Scherm scherm)
        {
            // loop door de bioscopen heen in de database
            foreach (var _bioscoop in _database.Bioscopen)
            {
                // We hebben de juiste bioscoop gevonden
                if (_bioscoop == bioscoop)
                {
                    // We kijken of de bioscoop niet toevallig al eenzelfde scherm bevat, we checken nu of het hele schermobject hetzelfde is, 
                    // niet of er bijvoorbeeld al een scherm bestaat met eenzelfde nummer
                    if (!_bioscoop.Schermen.Contains(scherm))
                    {
                        _bioscoop.Schermen.Add(scherm);
                    }
                    // Anders gooien we een error, deze komt dan naar boven als we een scherm dubbel proberen toe te voegen
                    else
                    {
                        throw new Exception($"Bioscoop met naam {bioscoop.Naam} bestaat heeft al een scherm met nummer {scherm.Nummer}");
                    }
                }

            }
        }

        public void ToonBioscopen()
        {
            Console.WriteLine("Dit zijn de huidige bioscopen in het systeem:");
            int BiosIndex = 1;
            foreach (var _bioscoop in _database.Bioscopen)
            {
                Console.WriteLine($"({BiosIndex}) {_bioscoop.Naam}");
                BiosIndex++;
            }
        }
        /*
        public void ToonStoel()
        {
            Console.WriteLine("dit zijn de stoelen waaruit je kan kiezen.");
            int StoelIndex = 1;
            foreach (var _stoel in _database.Zitplaatsen)
            {
                Console.WriteLine($"{_stoel.Rij} {_stoel.Nummer}");
                StoelIndex++;
            }
        }
        */
        public void ToonFilms()
        {
            int FilmIndex = 1;
            foreach (var _film in _database.Films)
            {
                Console.WriteLine($"({FilmIndex}) {_film.Titel}");
                FilmIndex++;
            }
        }

        public void VerwijderBioscoop()
        {
            Console.WriteLine("wilt u een bioscoop verwijderen? J/N");
            var antwoord = Console.ReadLine();

            if (antwoord == "J")
            {
                ToonBioscopen();

                Console.WriteLine("Welke bioscoop wilt u verwijderen? Typ cijfer van bios");
                var BioscoopNummer = Convert.ToInt32(Console.ReadLine());
                var gekozenBioscoop = this._database.Bioscopen[BioscoopNummer - 1];

                Console.WriteLine($"Wilt u {gekozenBioscoop.Naam} verwijderen? J/N");
                var verwijderAntwoord = Console.ReadLine();

                if (verwijderAntwoord == "J")
                {
                    this._database.Bioscopen.Remove(gekozenBioscoop);
                    UpdateData();
                    Console.WriteLine($"Bioscoop {gekozenBioscoop.Naam} is verwijderd");
                    Console.WriteLine("\n----\n");
                    ToonBioscopen();

                }

            }
            else if (antwoord == "N")
            {
                Console.WriteLine("Nou dan niet toch?");
            }

            {

            }
        }

        public void BioscoopKiezen()
        {
            //normale klant
            Console.WriteLine("Naar welke locatie wilt u?");
            int BiosIndex = 1;
            foreach (var _bioscoop in _database.Bioscopen)
            {
                Console.WriteLine($"({BiosIndex}) {_bioscoop.Naam}");
                BiosIndex++;
            }

            var BioscoopBezoekAns = Convert.ToInt32(Console.ReadLine());

            while (BioscoopBezoekAns <= 0 || BioscoopBezoekAns >= BiosIndex)
            {
                Console.WriteLine("U heeft een ongeldig nummer ingevoerd.\nProbeer het nog een keer.");
                BioscoopBezoekAns = Convert.ToInt32(Console.ReadLine());
            }
            if (BioscoopBezoekAns <= BiosIndex & BioscoopBezoekAns >= 0)
            {
                var BioscoopBezoekNaam = this._database.Bioscopen[BioscoopBezoekAns - 1];
                Console.WriteLine($"U heeft gekozen voor {BioscoopBezoekNaam.Naam}.");
                string GekozenBioscoop = BioscoopBezoekNaam.Naam;
                FilmKiezen(GekozenBioscoop, BioscoopBezoekAns, BioscoopBezoekNaam.BeschikbareFilms);
            }

        }
        public void FilmKiezen(string bioscoop, int BiosIndex, IList<Film> test)
        {
            Console.Clear();
            //hier komt films.json
            Console.WriteLine(bioscoop);
            Console.WriteLine("\nFilms die nu te zien zijn:\n");

            int FilmIndex = 1;
            foreach (var _film in test)
            {
                Console.WriteLine($"({FilmIndex}) {_film.Titel}");
                FilmIndex++;
                
            }

            var GekozenFilm = Convert.ToInt32(Console.ReadLine());

            if (GekozenFilm == 99)
            {
                BioscoopKiezen();
            }

            while (GekozenFilm <= 0 || GekozenFilm >= FilmIndex)
            {
                //deze while loop zorgt dat je alleen een getal kan invoeren waar een film aan gebonden is als je dat niet doet moet je opnieuw een nummer invoeren.
                Console.WriteLine("U heeft een ongeldig nummer ingevoerd.\nProbeer het nog een keer.");
                GekozenFilm = Convert.ToInt32(Console.ReadLine());
            }

            if (GekozenFilm >= 0 && GekozenFilm < FilmIndex)
            {
                var FilmBezoekNaam = this._database.Bioscopen[BiosIndex - 1].BeschikbareFilms[GekozenFilm - 1];
                Console.Clear();
                Console.WriteLine(bioscoop);
                Console.WriteLine($"Titel: {FilmBezoekNaam.Titel}.");
                Console.WriteLine($"Genre: {FilmBezoekNaam.Genre}");
                Console.WriteLine($"Beschrijving: {FilmBezoekNaam.Beschrijving}");
                Console.WriteLine($"Datum: {FilmBezoekNaam.Datum}");
                Console.WriteLine($"Tijd: {FilmBezoekNaam.Tijd}");

                string TweedeGekozenFilm = FilmBezoekNaam.Titel;
                Console.WriteLine("\nKlopt het dat u " + TweedeGekozenFilm + " wilt zien?");
                Console.WriteLine("\nType 1 als dit klopt of type 2 als dit niet klopt");
                int FilmCheck = Convert.ToInt32(Console.ReadLine());

                while (FilmCheck < 1 || FilmCheck > 2)
                {
                    Console.WriteLine("U kunt alleen maar 1 of 2 kiezen.");
                    FilmCheck = Convert.ToInt32(Console.ReadLine());
                }

                if (FilmCheck == 1)
                {
                    StoelenKiezen(TweedeGekozenFilm, bioscoop, GekozenFilm, BiosIndex - 1);
                }

                else if (FilmCheck == 2)
                {
                    FilmKiezen(bioscoop, BiosIndex, test);
                }
            }
        }

        public void StoelenKiezen(string Film, string Bioscoop, int FilmIndex, int BiosIndex)
        {
            Console.Clear();
            var StoelKiezen = this._database.Bioscopen[BiosIndex].Schermen[0].Vertoningen[Film].Zitplaatsen;


            Console.WriteLine(Bioscoop);
            Console.WriteLine("Film: " + Film);
            Console.WriteLine("Beschrikbare zitplaatsen.");
            int StoelIndex = 1;
            foreach (var _stoel in _database.Bioscopen)
            {
                var test = _stoel.Schermen[0].Vertoningen[Film].Zitplaatsen[StoelIndex];
                Console.WriteLine(test);
                StoelIndex++;
            }



            /*
            List<string> stoel = new List<string>
                    {
                        "Reguliere zitplaats",
                        "Love-Seat"
                    };

            //hier kan je kiezen uit 1 van twee soorten zitplaatsen, voor een premium klant zullen er meer opties zijn
            Console.WriteLine("\nSoorten zitplaats:\n\n(1) " + stoel[0] + "\n(2) " + stoel[1] + "\n\nType het nummer van de door u gekozen stoel.\n\nAls u terug wilt naar de vorige stap type dan 9.");
            int GekozenStoel = Convert.ToInt32(Console.ReadLine()) - 1;

            if (GekozenStoel == 99)
            {
                FilmKiezen(Bioscoop);
            }

            while (GekozenStoel > 2 | GekozenStoel < 0)
            {
                //zorgt ervoor dat er alleen maar uit de twee eerder genoemde zitplaatsen gekozen kan worden, kiest iemand iets anders moeten ze opnieuw een nummer invoeren
                Console.WriteLine("U kunt alleen maar kiezen uit 1 of 2.");
                GekozenStoel = Convert.ToInt32(Console.ReadLine()) - 1;
            }

            if (GekozenStoel >= 0 && GekozenStoel < 2)
            {
                string GekozenStoelString = stoel[GekozenStoel];
                Kosten(Film, Bioscoop, GekozenStoelString, GekozenStoel);
            }
            */
        }
        /*
        public void Kosten(string Film, string Bioscoop, string Stoel, int StoelIndex)
        {
            Console.Clear();
            Console.WriteLine(Bioscoop);

            List<int> prijs = new List<int>
            {
                10,
                17
            };

            Console.WriteLine("\nU heeft gekozen voor de " + Stoel);
            Console.WriteLine("\n1 " + Stoel + " kost " + prijs[StoelIndex] + " Euro");
            Console.WriteLine("\nAls u een andere soort stoel wilt kiezen kunt u 99 typen.");
            Console.WriteLine("\nHoeveel zitplaatsen wilt u reserveren?\n");

            int Aantal = Convert.ToInt32(Console.ReadLine());
            if (Aantal == 99)
            {
                StoelenKiezen(Film, Bioscoop);
            }

            while (Aantal == 0 && Aantal > 50)
            {
                Console.WriteLine("Er zijn maar 50 zitplaatsen.");
                Aantal = Convert.ToInt32(Console.ReadLine());
            }

            if (Aantal > 0 && Aantal <= 50)
            {
                Console.WriteLine("\nU heeft gekozen voor " + Aantal + " " + Stoel);
                Console.WriteLine("\nDit kost " + (prijs[StoelIndex] * Aantal) + " euro");
                //PlaatsenKiezen(Aantal);
            }
            
        }
        */
        /*
        public void PlaatsenKiezen(int aantal)
        {

        }
        */

        //jim functies

        //filmNaarBeschikbareFilms
            //Loop door totale film array heen
            //welke film wilt u toevoegen?
                //kies film
                //aan welke bioscoop wilt u deze film toevoegen?
                    //voeg toe aan beschikbare films
        public void filmNaarBeschikbaar()
        {
            
            Console.WriteLine("Welkom bij het filmaanbod aanpas register schema! \n Aan welke Bioscoop wilt u de film toevoegen?");
            ToonBioscopen();
            int biosKeuze = Convert.ToInt32(Console.ReadLine());
            biosKeuze -= 1;

            Console.WriteLine($"Dit zijn de huidige beschikbare films bij {_database.Bioscopen[biosKeuze].Naam}");

            int filmIndex = 1;
            /*
            foreach (var _film in _database.Bioscopen[biosKeuze].BeschikbareFilms)
            {
                Console.WriteLine($"({filmIndex}) {_film.Titel}");
            }
            */

            _database.Bioscopen[biosKeuze].BeschikbareFilms.PrintAllWithIndex(x => x.Titel);


            Console.WriteLine("dit zijn alle biosjes in het systeem");
            _database.Bioscopen.PrintAllWithIndex(x => x.Naam);

            Console.WriteLine("Deze films bevinden zich in de database en nog niet in het aanbod van deze bioscoop:");
            var witlijst = new List<int>();
            for(int i = 0; i < _database.Films.Count; i ++)
            {
                var _film = _database.Films[i];

                var filmBestaat = false;

                foreach (var _bioscoopFilm in _database.Bioscopen[biosKeuze].BeschikbareFilms)
                {
                    if (_bioscoopFilm.Titel == _film.Titel)
                    {
                        filmBestaat = true;
                        break;
                    }
                }
                if (!filmBestaat)
                {
                    Console.WriteLine($"({i}) {_film.Titel}");
                    witlijst.Add(i);
                }
                //voeg toe aan bios
                //uit whitelist haalt
            }
            var heeftGeantwoord = false;
            while (!heeftGeantwoord)
            {
                Console.WriteLine("Welke film wilt u toevoegen aan het bioscoop aanbod?");
                int filmKeuze = Convert.ToInt32(Console.ReadLine());
                if (witlijst.Contains(filmKeuze))
                {
                    _database.Bioscopen[biosKeuze].BeschikbareFilms.Add(_database.Films[filmKeuze]);
                    //UpdateData();
                    //updateData staat nu uitgecomment, zodat niet alle films gelijk worden weggeschreven naar het Json bestand
                    //dit is momenteel makkelijker voor testen aangezien ik nog geen functie heb die films verwijderd uit beschikbareFilms :)
                    heeftGeantwoord = true;
                }
                else
                {
                    Console.WriteLine("fout antwoord pannekoek, terug naar start!");
                }
            }


            Console.WriteLine("Wilt u nog een film toevoegen? Y/N");
            string antwoord = Console.ReadLine();
            if(antwoord.ToUpper() == "Y")
            {
                filmNaarBeschikbaar();
            }
            else
            {
                //verwijs terug naar een ander menu
            }

        }

        //filmUitBeschikbaar
            //kies bios
            //loop door beschikbareFilms
            //laat gebruiker een beschikbarefilm kiezen
                //verwijdert beschikbare film



        //beschikbareFilmsNaarVertoning
            //laat gebruiker een bios kiezen
                //loop door totale beschikbare films heen
                //laat gebruiker film kiezen
                    //laat gebruiker de rest van de data invoeren
                        //voeg film toe als vertoning bij bios
        


        public void vertoningenTonen()
        {
            Console.WriteLine("Welkom bij Nioscoop, dit zijn alle voorstellingen van al onze bioscopen");

            var _bioscopen = _database.Bioscopen;

            foreach (var _bioscoop in _bioscopen)
            {
                Console.WriteLine($"--* {_bioscoop.Naam} *--");
                foreach(var _schermen in _bioscoop.Schermen)
                {
                    foreach(var _vertoningen in _schermen.Vertoningen)
                    {
                        Console.WriteLine($"{_vertoningen.Key}");
                        Console.WriteLine("\n\n");
                    }
                }
            }
        }


        
        
        public void toonVertoningen()
            //Volgens mij is deze functie werkend, alleen heb ik het alleen nog maar getest met weinig schermen / vertoningen
            //Zal dit nader testen als ik andere functies werkend heb waarmee ik die toe kan voegen aan de database.
                // ook kan ik nog geen film gegevens ophalen per vertoning omdat films nog niet gekoppeld zijn aan vertoningen :)
        {
            Console.WriteLine("Van welke bioscoop wilt u de huidige voorstellingen zien?");
            ToonBioscopen();

            Console.WriteLine("\n Voer het nummer van de Bioscoop in waarvan u de voorstellingen wilt zien.");
            int antwoord = Convert.ToInt32(Console.ReadLine());

            var bioscopen = _database.Bioscopen;
            var gekozenBios = bioscopen[antwoord];

            int schermIndex = 0;
            int vertoningIndex = 0;
            foreach (var _schermen in gekozenBios.Schermen)
            {
                schermIndex += 1;
                Console.WriteLine($"Schermindex is nu {schermIndex}");


                foreach (var _vertoningen in _schermen.Vertoningen )
                {
                    vertoningIndex += 1;
                    Console.WriteLine($"Vertoningindex is nu {vertoningIndex}, voorstelling key is {_vertoningen.Key}");

                }
            }
        }


        public IList<Bioscoop> GetBioscopen()
        {
            return _database.Bioscopen;
        }
    }
}