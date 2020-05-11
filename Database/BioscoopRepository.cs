using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JimFilmsTake2.Model;
using Newtonsoft.Json;
namespace JimFilmsTake2.Db
{
    public class BioscoopRepository
    {

        private JsonModel _database { get; set; }
        public static readonly string FILEPATH = @"C:\Project_B\GitKraken\Projectb-Groep4-inf2019-2020\Database\db.json";

        public BioscoopRepository()
        {

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
                FilmKiezen(GekozenBioscoop);
            }

        }
        public void FilmKiezen(string bioscoop)
        {
            Console.Clear();
            //hier komt films.json
            Console.WriteLine(bioscoop);
            Console.WriteLine("\nFilms die nu te zien zijn:\n");
            int FilmIndex = 1;
            foreach (var _film in _database.Films)
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
                var FilmBezoekNaam = this._database.Films[GekozenFilm - 1];
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
                    StoelenKiezen(TweedeGekozenFilm, bioscoop);
                }

                else if (FilmCheck == 2)
                {
                    FilmKiezen(bioscoop);
                }
            }
        }

        public void StoelenKiezen(string Film, string Bioscoop)
        {
            Console.Clear();
            Console.WriteLine(Bioscoop);
            Console.WriteLine("Film: " + Film);
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
        }

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

        /*
        public void PlaatsenKiezen(int aantal)
        {

        }
        */

        public IList<Bioscoop> GetBioscopen()
        {
            return _database.Bioscopen;
        }
    }
}