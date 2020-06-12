using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Transactions;
using JimFilmsTake2.Model;
using Newtonsoft.Json;
using ProjectB.Utils;
using registratie88888888;

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
            var FILEPATH = Path.Combine(projectFolder, @"Database/db.json");
            var jsonString = File.ReadAllText(FILEPATH);
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

        public void toonBeschikbareFilms(int keuze)
        {
            int filmIndex = 1;
            foreach(var _films in this._database.Bioscopen[keuze].BeschikbareFilms)
            {
                Console.WriteLine($"({filmIndex}) {_films.Titel}");
                filmIndex += 1;

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
            Console.Clear();
            //normale klant
            Console.WriteLine("Naar welke locatie wilt u?");
            Console.WriteLine("Als u 9 typt gaat u terug naar het hoofdmenu.");
            int BiosIndex = 1;
            var Bios = _database.Bioscopen.Count;
            foreach (var _bioscoop in _database.Bioscopen)
            {
                Console.WriteLine($"({BiosIndex}) {_bioscoop.Naam}");
                BiosIndex++;
            }

            var GekozenBios = Console.ReadLine();

            var isNumeric = int.TryParse(GekozenBios, out int GekozenBiosV2);
            GekozenBiosV2 -= 1;

            if (GekozenBiosV2 == 8)
            {
                var Startmenu = new Classq();
                Startmenu.test();
            }

            while (GekozenBiosV2 < 0 || GekozenBiosV2 >= Bios)
            {
                Console.WriteLine("Ongeldige input, probeer het nog een keer.");
                GekozenBios = Console.ReadLine();
                isNumeric = int.TryParse(GekozenBios, out GekozenBiosV2);
                GekozenBiosV2 -= 1;

            }
            if (GekozenBiosV2 <= Bios && GekozenBiosV2 >= 0)
            {
                var BioscoopBezoekNaam = this._database.Bioscopen[GekozenBiosV2];
                Console.WriteLine($"U heeft gekozen voor {BioscoopBezoekNaam.Naam}.");
                string GekozenBioscoop = BioscoopBezoekNaam.Naam;
                FilmKiezen(GekozenBioscoop, GekozenBiosV2, BioscoopBezoekNaam.BeschikbareFilms);
            }

        }
        public void FilmKiezen(string bioscoop, int BiosIndex, IList<Film> DoorGeefTest)
        {
            Console.Clear();
            //hier komt films.json
            Console.WriteLine(bioscoop);
            Console.WriteLine("Typ het nummer van de film die u wilt zien, als u 99 typt kunt u een andere bioscoop kiezen.\nFilms die nu te zien zijn:\n");

            int FilmIndex = 1;
            foreach (var _film in DoorGeefTest)
            {
                Console.WriteLine($"({FilmIndex}) {_film.Titel}");
                FilmIndex++;

            }
            Console.WriteLine("\n(99) Bioscopen");

            var GekozenFilm = Console.ReadLine();

            var isNumeric = int.TryParse(GekozenFilm, out int GekozenFilmv2);
            
            if (GekozenFilmv2 == 99)
            {
                BioscoopKiezen();
            }

            while (GekozenFilmv2 <= 0 || GekozenFilmv2 >= FilmIndex)
            {
                //deze while loop zorgt dat je alleen een getal kan invoeren waar een film aan gebonden is als je dat niet doet moet je opnieuw een nummer invoeren.
                Console.WriteLine("Ongeldige input, probeer het nog een keer.");
                GekozenFilm = Console.ReadLine();
                isNumeric = int.TryParse(GekozenFilm, out GekozenFilmv2);
            }

            if (GekozenFilmv2 >= 0 && GekozenFilmv2 < FilmIndex)
            {
                var FilmBezoekNaam = this._database.Bioscopen[BiosIndex].BeschikbareFilms[GekozenFilmv2 - 1];
                Console.Clear();
                Console.WriteLine(bioscoop);
                string FilmNaam = FilmBezoekNaam.Titel;
                Console.WriteLine($"Titel: {FilmBezoekNaam.Titel}.");
                string FilmGenre = FilmBezoekNaam.Genre;
                Console.WriteLine($"Genre: {FilmBezoekNaam.Genre}");
                Console.WriteLine($"Schermtype: {FilmBezoekNaam.Schermtype}");
                Console.WriteLine($"Beschrijving: {FilmBezoekNaam.Beschrijving}");

                string TweedeGekozenFilm = FilmBezoekNaam.Titel;
                Console.WriteLine("\nKlopt het dat u " + TweedeGekozenFilm + " wilt zien?");
                Console.WriteLine("\nTyp 1 als u de tijden van deze film wilt zien, typ 2 als u gerelateerde films wilt zien of typ 3 als u een andere film wilt selecteren.");
                var FilmCheck = Console.ReadLine();

                var isNumeric2 = int.TryParse(FilmCheck, out int FilmCheckV2);

                while (FilmCheckV2 < 1 || FilmCheckV2 > 3)
                {
                    Console.WriteLine("U kunt alleen maar 1, 2 of 3 kiezen.");
                    FilmCheck = Console.ReadLine();
                    isNumeric2 = int.TryParse(FilmCheck, out FilmCheckV2);

                }

                if (FilmCheckV2 == 1)
                {
                    FilmVertoningen(bioscoop, BiosIndex, TweedeGekozenFilm, GekozenFilmv2, DoorGeefTest);
                }

                if (FilmCheckV2 == 2)
                {
                    GerelateerdeFilms(FilmGenre, FilmNaam, DoorGeefTest, bioscoop, BiosIndex);
                }

                if (FilmCheckV2 == 3)
                {
                    FilmKiezen(bioscoop, BiosIndex, DoorGeefTest);
                }
            }
        }

        public void GerelateerdeFilms(string Genre, string Film, IList<Film> VoorTerugFunctie, string bioscoop, int BiosIndex)
        {
            Console.Clear();
            Console.WriteLine(bioscoop);
            Console.WriteLine("Films met hetzelfde genre");
            int FilmIndex = 0;
            int count = 1;
            int GerelateerdeFilmsv2 = 0;
            var FilmTuple = new List<Tuple<int, string>>
            {

            };

            foreach (var film in VoorTerugFunctie)
            {
                if (film.Genre == Genre && film.Titel != Film)
                {
                    FilmTuple.Add(new Tuple<int, string>(FilmIndex, film.Titel));
                    Console.WriteLine($"({count}) {film.Titel}");
                    GerelateerdeFilmsv2++;
                    count++;
                }
                FilmIndex++;

                if (FilmIndex == VoorTerugFunctie.Count && GerelateerdeFilmsv2 > 0)
                {
                    var GekozenFilm = Console.ReadLine();
                    var isNumeric = int.TryParse(GekozenFilm, out int GekozenFilmV2);
                    GekozenFilmV2 -= 1;

                    while (GekozenFilmV2 < 0 || GekozenFilmV2 > GerelateerdeFilmsv2 - 1)
                    {
                        Console.WriteLine("Ongeldige input, probeer het nog een keer");
                        GekozenFilm = Console.ReadLine();
                        isNumeric = int.TryParse(GekozenFilm, out GekozenFilmV2);
                        GekozenFilmV2 -= 1;

                    }
                    var TweedeGekozenFilm = FilmTuple[GekozenFilmV2].Item2;

                    Console.Clear();
                    Console.WriteLine($"U heeft gekozen voor {FilmTuple[GekozenFilmV2].Item2}");
                    Console.WriteLine("Typ 1 als u de tijden voor deze film wilt zien, typ 2 als u een andere film wilt zien of typ 3 als u een andere bioscoop wilt selecteren");
                    var FilmCheck = Console.ReadLine();
                    var isNumericV2 = int.TryParse(FilmCheck, out int FilmCheckV2);

                    while (FilmCheckV2 < 1 || FilmCheckV2 > 3)
                    {
                        Console.WriteLine("U kunt alleen maar 1, 2 of 3 kiezen.");
                        FilmCheck = Console.ReadLine();
                        isNumericV2 = int.TryParse(FilmCheck, out FilmCheckV2);

                    }

                    if (FilmCheckV2 == 1)
                    {
                        FilmVertoningen(bioscoop, BiosIndex, TweedeGekozenFilm, FilmTuple[GekozenFilmV2].Item1, VoorTerugFunctie);
                    }
                    if (FilmCheckV2 == 2)
                    {
                        FilmKiezen(bioscoop, BiosIndex, VoorTerugFunctie);
                    }
                    if (FilmCheckV2 == 3)
                    {
                        BioscoopKiezen();
                    }
                }
            }

            if (GerelateerdeFilmsv2 == 0)
            {
                Console.WriteLine("Er draaien momenteel geen films met hetzelfde genre.\nDruk op enter om terug naar de films te gaan");
                Console.ReadLine();
                FilmKiezen(bioscoop, BiosIndex, VoorTerugFunctie);
            }
        }

        public void FilmVertoningen(string bioscoop, int BiosIndex, string TweedeGekozenFilm, int GekozenFilm, IList<Film> VoorTerugFunctie)
        {
            Console.Clear();
            Console.WriteLine($"{bioscoop}");
            var Bioscoop = _database.Bioscopen[BiosIndex];
            Console.WriteLine($"{TweedeGekozenFilm}\n");
            var poging = this._database.Bioscopen[BiosIndex].Schermen.Count;
            int count = 1;
            int CountForExit = 1;
            var TupleTest = new List<Tuple<int, string>>
            {

            };

            foreach (var schermen in Bioscoop.Schermen)
            {
                var Scherm = schermen.Nummer - 1;
                foreach (var vertoningen in schermen.Vertoningen)
                {
                    var placeholder = vertoningen.Key;
                    var testje = this._database.Bioscopen[BiosIndex].Schermen[Scherm].Vertoningen[placeholder].Film.Titel;

                    if (TweedeGekozenFilm == testje)
                    {
                        var Doorgeven = vertoningen.Key;
                        Console.WriteLine($"({count}) {vertoningen.Key}");
                        count++;
                        TupleTest.Add(new Tuple<int, string>(Scherm, vertoningen.Key));
                        
                    }
                    CountForExit++;
                    if (CountForExit == poging + 1)
                    {
                        Console.WriteLine("Typ het nummer van de tijd die u wilt bezoeken");
                        var GekozenVoorstelling = Console.ReadLine();
                        var isNumeric = int.TryParse(GekozenVoorstelling, out int GekozenVoorstellingV2);

                        while (GekozenVoorstellingV2 < 1 || GekozenVoorstellingV2 >= count)
                        {
                            Console.WriteLine("Ongeldige input, probeer het nog een keer");
                            GekozenVoorstelling = Console.ReadLine();
                            isNumeric = int.TryParse(GekozenVoorstelling, out GekozenVoorstellingV2);

                        }
                        int GekozenScherm = TupleTest[GekozenVoorstellingV2 - 1].Item1;
                        string VertoningKey = TupleTest[GekozenVoorstellingV2 - 1].Item2;
                        var test = this._database.Bioscopen[BiosIndex].Schermen[GekozenScherm].Vertoningen[VertoningKey];

                        Console.WriteLine($"U heeft gekozen voor de voorstelling van {VertoningKey}");

                        Console.WriteLine("Typ 1 als dit klopt, typ 2 als dit niet klopt of typ 3 als u een andere film wilt kiezen.");
                        var VertoningCheck = Console.ReadLine();
                        var isNumericV2 = int.TryParse(VertoningCheck, out int VertoningCheckV2);

                        while (VertoningCheckV2 < 1 || VertoningCheckV2 > 3)
                        {
                            Console.WriteLine("U kunt alleen maar 1, 2 of 3 kiezen.");
                            VertoningCheck = Console.ReadLine();
                            isNumericV2 = int.TryParse(VertoningCheck, out VertoningCheckV2);

                        }

                        if (VertoningCheckV2 == 1)
                        {
                            StoelenKiezen(TweedeGekozenFilm, bioscoop, GekozenFilm, BiosIndex, VoorTerugFunctie, GekozenScherm, VertoningKey);
                        }
                        if (VertoningCheckV2 == 2)
                        {
                            FilmVertoningen(bioscoop, BiosIndex, TweedeGekozenFilm, GekozenFilm, VoorTerugFunctie);
                        }
                        if (VertoningCheckV2 == 3)
                        {
                            FilmKiezen(bioscoop, BiosIndex, VoorTerugFunctie);
                        }
                    }
                }
            }
        }

        public void StoelenKiezen(string Film, string Bioscoop, int FilmIndex, int BiosIndex, IList<Film> VoorTerugFunctie, int GekozenScherm, string VertoningKey)
        {
            Console.Clear();
            var StoelKiezen1 = this._database.Bioscopen[BiosIndex].Schermen[GekozenScherm].Vertoningen[VertoningKey].Zitplaatsen;

            string UserName = Environment.UserName;

            Console.WriteLine(Bioscoop);
            Console.WriteLine("Film: " + Film);
            Console.WriteLine($"Tijd: {VertoningKey}");
            int BeschikbareZitplaatsen = 0;
            foreach (var i in StoelKiezen1)
            {
                if (i.Bezet == false)
                {
                    BeschikbareZitplaatsen++;
                }
            }
            Console.WriteLine("Zitplaatsen op rij 1 zijn love seats, deze zitplaatsen zijn bedoeld voor 2 personen\n");
            Console.WriteLine($"Er zijn {BeschikbareZitplaatsen} beschikbare zitplaatsen, hoeveel wilt u er reserveren\nU kunt ook 999 typen om een andere film kiezen");
            var ResetList = new List<int>();

            var Hoeveelheid = Console.ReadLine();
            var isNumeric = int.TryParse(Hoeveelheid, out int HoeveelheidV2);

            if (HoeveelheidV2 == 999)
            {
                FilmKiezen(Bioscoop, BiosIndex, VoorTerugFunctie);
            }

            while (HoeveelheidV2 < 1 || HoeveelheidV2 > StoelKiezen1.Count)
            {
                Console.WriteLine("Ongeldige input, probeer het nog een keer.");
                Hoeveelheid = Console.ReadLine();
                isNumeric = int.TryParse(Hoeveelheid, out HoeveelheidV2);

            }

            Console.WriteLine("Beschikbare zitplaatsen.\nTyp het linker nummer van de zitplaats die u wilt.");
            int StoelIndex = 1;
            string GekozenStoelen = "";
            string test = "";
            int HoeveelheidStoelen = StoelKiezen1.Count;
            var probeersel = ((HoeveelheidStoelen) / (StoelKiezen1[HoeveelheidStoelen - 1].Rij + 1));

            //Dit laat de stoelen en rijen zien in de console
            while (StoelIndex <= StoelKiezen1.Count)
            {
                int CurrentSeat = StoelIndex;

                if ((StoelIndex - 1) % probeersel == 0)
                {
                    test += ($"\nRij {StoelKiezen1[StoelIndex].Rij + 1}: ");
                }

                if (StoelKiezen1[StoelIndex - 1].Bezet == true)
                {
                    test += $"(-X-), ";
                }

                if (StoelKiezen1[StoelIndex - 1].Rij != 9)
                {
                    test += $"({CurrentSeat}:{StoelKiezen1[StoelIndex - 1].Nummer + 1}), ";
                }
                else
                {
                    test += "Premium  ";
                }
                StoelIndex++;
                if (StoelIndex == (StoelKiezen1.Count + 1))
                {
                    Console.WriteLine(test);
                }
            }

            while (HoeveelheidV2 > 0)
            {
                var GekozenStoel = Console.ReadLine();
                var isNumericV2 = int.TryParse(GekozenStoel, out int GekozenStoelV2);

                while (GekozenStoelV2 < 1 || GekozenStoelV2 > StoelKiezen1.Count)
                {
                    Console.WriteLine("Ongeldige input, probeer het nog een keer");
                    GekozenStoel = Console.ReadLine();
                    isNumericV2 = int.TryParse(GekozenStoel, out GekozenStoelV2);

                }
                while (StoelKiezen1[GekozenStoelV2 - 1].Bezet == true)
                {
                    Console.WriteLine("Ongeldige input, probeer het nog een keer");
                    GekozenStoel = Console.ReadLine();
                    isNumericV2 = int.TryParse(GekozenStoel, out GekozenStoelV2);
                }

                if (StoelKiezen1[GekozenStoelV2 - 1].Bezet == false)
                {
                    Console.WriteLine($"{StoelKiezen1[GekozenStoelV2 - 1].Rij + 1} {StoelKiezen1[GekozenStoelV2 - 1].Nummer}");
                    StoelKiezen1[GekozenStoelV2 - 1].Bezet = true;
                    StoelKiezen1[GekozenStoelV2 - 1].Naam = UserName;
                    ResetList.Add(GekozenStoelV2 - 1);
                    UpdateData();
                }
                Console.Clear();
                Console.WriteLine(Bioscoop);
                Console.WriteLine("Film: " + Film);
                Console.WriteLine($"U kunt nog {HoeveelheidV2 - 1} zitplaatsen kiezen.\nTyp het linker nummer van de zitplaats die u wilt.");


                StoelIndex = 1;
                test = "";
                while (StoelIndex <= StoelKiezen1.Count)
                {
                    int CurrentSeat = StoelIndex;

                    if ((StoelIndex - 1) % probeersel == 0)
                    {
                        test += ($"\nRij {StoelKiezen1[StoelIndex].Rij + 1}: ");
                    }

                    if (StoelKiezen1[StoelIndex - 1].Bezet == true)
                    {
                        test += $"(-X-), ";
                    }

                    if (StoelKiezen1[StoelIndex - 1].Rij != 9)
                    {
                        test += $"({CurrentSeat}:{StoelKiezen1[StoelIndex - 1].Nummer + 1}), ";
                    }
                    else
                    {
                        test += "Premium  ";
                    }
                    StoelIndex++;
                    if (StoelIndex == (StoelKiezen1.Count + 1))
                    {
                        Console.WriteLine(test);
                    }
                }
                string text = $"({StoelKiezen1[GekozenStoelV2 - 1].Rij + 1}:{StoelKiezen1[GekozenStoelV2 - 1].Nummer + 1})\n";
                GekozenStoelen += text;
                Console.WriteLine($"Gekozen stoelen:\n{GekozenStoelen}");

                if (StoelKiezen1[GekozenStoelV2 - 1].Rij == 0)
                {
                    if (HoeveelheidV2 < 2)
                    {
                        Console.WriteLine("Als u een Love Seat wilt moet u 2 zitplaatsen selecteren.\nDruk op enter om door te gaan.");
                        Console.ReadLine();
                        foreach (var Stoel in ResetList)
                        {
                            StoelKiezen1[Stoel].Bezet = false;
                        }
                        UpdateData();

                        StoelenKiezen(Film, Bioscoop, FilmIndex, BiosIndex, VoorTerugFunctie, GekozenScherm, VertoningKey);
                    }
                    else
                    {
                        HoeveelheidV2 -= 2;
                    }
                }
                else
                {
                    HoeveelheidV2--;
                }
            }
            if (HoeveelheidV2 == 0)
            {
                Console.Clear();
                Console.WriteLine(Bioscoop);
                Console.WriteLine($"Gekozen stoelen:\n{GekozenStoelen}");
                Console.WriteLine("Klopt dit?\nTyp 1 als het klopt of typ 2 als het niet klopt.");
                var DoorGeven = Console.ReadLine();
                var isNumericV2 = int.TryParse(DoorGeven, out int DoorGevenV2);

                while (DoorGevenV2 < 1 || DoorGevenV2 > 2)
                {
                    Console.WriteLine("U kunt alleen maar 1 of 2 kiezen");
                    DoorGeven = Console.ReadLine();
                    isNumericV2 = int.TryParse(DoorGeven, out DoorGevenV2);
                }

                if (DoorGevenV2 == 1)
                {
                    Kosten(Film, Bioscoop, ResetList, BiosIndex, GekozenScherm, VertoningKey);
                }
                else if (DoorGevenV2 == 2)
                {
                    foreach (var Stoel in ResetList)
                    {
                        StoelKiezen1[Stoel].Bezet = false;
                        StoelKiezen1[Stoel].Naam = null;
                    }
                    ResetList.Clear();
                    UpdateData();
                    StoelenKiezen(Film, Bioscoop, FilmIndex, BiosIndex, VoorTerugFunctie, GekozenScherm, VertoningKey);
                }
            }
        }

        public void Kosten(string Film, string Bioscoop, List<int> GekozenStoelen, int BiosIndex, int GekozenScherm, string VertoningKey)
        {
            Console.Clear();
            Console.WriteLine(Bioscoop);
            var StoelKiezen1 = this._database.Bioscopen[BiosIndex].Schermen[GekozenScherm].Vertoningen[VertoningKey].Zitplaatsen;

            var SoortScherm = this._database.Bioscopen[BiosIndex].Schermen[GekozenScherm].Vertoningen[VertoningKey].Film.Schermtype;
            //var Tickets = this._database.Tickets[0].TypeTicket;
            int LSCount = 0;
            int RSCount = 0;
            double TicketPrijs = 0;
            double Kosten = 0;
            //Console.WriteLine(Tickets);
            foreach (var Type in _database.Tickets)
            {
                if (Type.TypeTicket == SoortScherm)
                {
                    TicketPrijs = Type.TicketPrijs;
                }
            }
            Console.WriteLine($"De basisprijs van een {SoortScherm} film is {TicketPrijs} euro.\nEen Love Seat kost 2.50 extra\n");
            Console.WriteLine("Uw zitplaatsen:");

            foreach (int Stoel in GekozenStoelen)
            {
                if (StoelKiezen1[Stoel].Rij == 0)
                {
                    Console.WriteLine($"Love Seat:({StoelKiezen1[Stoel].Rij + 1}:{StoelKiezen1[Stoel].Nummer + 1})");
                    LSCount++;
                }
                else
                {
                    Console.WriteLine($"Reguliere zitplaats: ({StoelKiezen1[Stoel].Rij + 1}:{StoelKiezen1[Stoel].Nummer + 1})");
                    RSCount++;
                }
            }
            Console.WriteLine("\nHeeft u nog een korting die u kunt toepassen?");
            Console.Write("Typ 'Ja' of 'Nee' : ");
            string keuze = Console.ReadLine();
            if (keuze == "ja" || keuze == "Ja")
            {
                Console.WriteLine("\nBeschikbare kortingen");
                int Index = 1;
                foreach (Korting i in this._database.Kortingen)
                {
                    Console.WriteLine($"({Index}) {i.TypeKorting} : {i.VerrekendeKorting}");
                    Index++;
                }
                Console.Write("Typ de index van de korting die u wilt gebruiken : ");
                int keuze2 = Convert.ToInt32(Console.ReadLine());
                var kortingkeuze = this._database.Kortingen[keuze2 - 1];
                Console.WriteLine($"Wilt u {kortingkeuze.TypeKorting} gebruiken?");
                Console.Write("Typ 'Ja' of 'Nee' : ");
                string beslissing = Console.ReadLine();
                if (beslissing == "ja" || beslissing == "Ja")
                {
                    Kosten = ((TicketPrijs * GekozenStoelen.Count) + (LSCount * 2.50)) - ((kortingkeuze.VerrekendeKorting / 100) * (TicketPrijs * GekozenStoelen.Count));
                }
                else
                {
                    Kosten = TicketPrijs * GekozenStoelen.Count + (LSCount * 2.50);
                }

            }
            else
            {
                Kosten = TicketPrijs * GekozenStoelen.Count + (LSCount * 2.50);
            }
            Console.WriteLine($"\nDe totale kosten bedragen {Kosten} euro.");
            Console.WriteLine("\nDruk op 1 om terug te gaan naar het hoofdmenu.\nDruk op enter om het programma te verlaten");
            var Afsluiten = Console.ReadLine();
            var isNumeric = int.TryParse(Afsluiten, out int AfsluitenV2);

            if (isNumeric == false)
            {
                Environment.Exit(0);
            }
            if (AfsluitenV2 == 1)
            {
                var Startmenu = new Classq();
                Startmenu.test();
            }


        }



        public void BioscoopKiezenPremium()
        {
            Console.Clear();
            //normale klant
            Console.WriteLine("Naar welke locatie wilt u?");
            Console.WriteLine("Als u 9 typt gaat u terug naar het hoofdmenu.");
            int BiosIndex = 1;
            var Bios = _database.Bioscopen.Count;
            foreach (var _bioscoop in _database.Bioscopen)
            {
                Console.WriteLine($"({BiosIndex}) {_bioscoop.Naam}");
                BiosIndex++;
            }

            var GekozenBios = Console.ReadLine();

            var isNumeric = int.TryParse(GekozenBios, out int GekozenBiosV2);
            GekozenBiosV2 -= 1;

            if (GekozenBiosV2 == 8)
            {
                var Startmenu = new Classq();
                Startmenu.test();
            }

            while (GekozenBiosV2 < 0 || GekozenBiosV2 >= Bios)
            {
                Console.WriteLine("Ongeldige input, probeer het nog een keer.");
                GekozenBios = Console.ReadLine();
                isNumeric = int.TryParse(GekozenBios, out GekozenBiosV2);
                GekozenBiosV2 -= 1;

            }
            if (GekozenBiosV2 <= Bios && GekozenBiosV2 >= 0)
            {
                var BioscoopBezoekNaam = this._database.Bioscopen[GekozenBiosV2];
                Console.WriteLine($"U heeft gekozen voor {BioscoopBezoekNaam.Naam}.");
                string GekozenBioscoop = BioscoopBezoekNaam.Naam;
                FilmKiezenPremium(GekozenBioscoop, GekozenBiosV2, BioscoopBezoekNaam.BeschikbareFilms);
            }

        }
        public void FilmKiezenPremium(string bioscoop, int BiosIndex, IList<Film> DoorGeefTest)
        {
            Console.Clear();
            //hier komt films.json
            Console.WriteLine(bioscoop);
            Console.WriteLine("Typ het nummer van de film die u wilt zien, als u 99 typt kunt u een andere bioscoop kiezen.\nFilms die nu te zien zijn:\n");

            int FilmIndex = 1;
            foreach (var _film in DoorGeefTest)
            {
                Console.WriteLine($"({FilmIndex}) {_film.Titel}");
                FilmIndex++;

            }
            Console.WriteLine("\n(99) Bioscopen");

            var GekozenFilm = Console.ReadLine();

            var isNumeric = int.TryParse(GekozenFilm, out int GekozenFilmv2);

            if (GekozenFilmv2 == 99)
            {
                BioscoopKiezenPremium();
            }

            while (GekozenFilmv2 <= 0 || GekozenFilmv2 >= FilmIndex)
            {
                //deze while loop zorgt dat je alleen een getal kan invoeren waar een film aan gebonden is als je dat niet doet moet je opnieuw een nummer invoeren.
                Console.WriteLine("Ongeldige input, probeer het nog een keer.");
                GekozenFilm = Console.ReadLine();
                isNumeric = int.TryParse(GekozenFilm, out GekozenFilmv2);
            }

            if (GekozenFilmv2 >= 0 && GekozenFilmv2 < FilmIndex)
            {
                var FilmBezoekNaam = this._database.Bioscopen[BiosIndex].BeschikbareFilms[GekozenFilmv2 - 1];
                Console.Clear();
                Console.WriteLine(bioscoop);
                string FilmNaam = FilmBezoekNaam.Titel;
                Console.WriteLine($"Titel: {FilmBezoekNaam.Titel}.");
                string FilmGenre = FilmBezoekNaam.Genre;
                Console.WriteLine($"Genre: {FilmBezoekNaam.Genre}");
                Console.WriteLine($"Schermtype: {FilmBezoekNaam.Schermtype}");
                Console.WriteLine($"Beschrijving: {FilmBezoekNaam.Beschrijving}");

                string TweedeGekozenFilm = FilmBezoekNaam.Titel;
                Console.WriteLine("\nKlopt het dat u " + TweedeGekozenFilm + " wilt zien?");
                Console.WriteLine("\nTyp 1 als u de tijden van deze film wilt zien, typ 2 als u gerelateerde films wilt zien of typ 3 als u een andere film wilt selecteren.");
                var FilmCheck = Console.ReadLine();

                var isNumeric2 = int.TryParse(FilmCheck, out int FilmCheckV2);

                while (FilmCheckV2 < 1 || FilmCheckV2 > 3)
                {
                    Console.WriteLine("U kunt alleen maar 1, 2 of 3 kiezen.");
                    FilmCheck = Console.ReadLine();
                    isNumeric2 = int.TryParse(FilmCheck, out FilmCheckV2);

                }

                if (FilmCheckV2 == 1)
                {
                    FilmVertoningenPremium(bioscoop, BiosIndex, TweedeGekozenFilm, GekozenFilmv2, DoorGeefTest);
                }

                if (FilmCheckV2 == 2)
                {
                    GerelateerdeFilmsPremium(FilmGenre, FilmNaam, DoorGeefTest, bioscoop, BiosIndex);
                }

                if (FilmCheckV2 == 3)
                {
                    FilmKiezenPremium(bioscoop, BiosIndex, DoorGeefTest);
                }
            }
        }

        public void GerelateerdeFilmsPremium(string Genre, string Film, IList<Film> VoorTerugFunctie, string bioscoop, int BiosIndex)
        {
            Console.Clear();
            Console.WriteLine(bioscoop);
            Console.WriteLine("Films met hetzelfde genre");
            int FilmIndex = 0;
            int count = 1;
            int GerelateerdeFilmsv2 = 0;
            var FilmTuple = new List<Tuple<int, string>>
            {

            };

            foreach (var film in VoorTerugFunctie)
            {
                if (film.Genre == Genre && film.Titel != Film)
                {
                    FilmTuple.Add(new Tuple<int, string>(FilmIndex, film.Titel));
                    Console.WriteLine($"({count}) {film.Titel}");
                    GerelateerdeFilmsv2++;
                    count++;
                }
                FilmIndex++;

                if (FilmIndex == VoorTerugFunctie.Count && GerelateerdeFilmsv2 > 0)
                {
                    var GekozenFilm = Console.ReadLine();
                    var isNumeric = int.TryParse(GekozenFilm, out int GekozenFilmV2);
                    GekozenFilmV2 -= 1;

                    while (GekozenFilmV2 < 0 || GekozenFilmV2 > GerelateerdeFilmsv2 - 1)
                    {
                        Console.WriteLine("Ongeldige input, probeer het nog een keer");
                        GekozenFilm = Console.ReadLine();
                        isNumeric = int.TryParse(GekozenFilm, out GekozenFilmV2);
                        GekozenFilmV2 -= 1;

                    }
                    var TweedeGekozenFilm = FilmTuple[GekozenFilmV2].Item2;

                    Console.Clear();
                    Console.WriteLine($"U heeft gekozen voor {FilmTuple[GekozenFilmV2].Item2}");
                    Console.WriteLine("Typ 1 als u de tijden voor deze film wilt zien, typ 2 als u een andere film wilt zien of typ 3 als u een andere bioscoop wilt selecteren");
                    var FilmCheck = Console.ReadLine();
                    var isNumericV2 = int.TryParse(FilmCheck, out int FilmCheckV2);

                    while (FilmCheckV2 < 1 || FilmCheckV2 > 3)
                    {
                        Console.WriteLine("U kunt alleen maar 1, 2 of 3 kiezen.");
                        FilmCheck = Console.ReadLine();
                        isNumericV2 = int.TryParse(FilmCheck, out FilmCheckV2);

                    }

                    if (FilmCheckV2 == 1)
                    {
                        FilmVertoningenPremium(bioscoop, BiosIndex, TweedeGekozenFilm, FilmTuple[GekozenFilmV2].Item1, VoorTerugFunctie);
                    }
                    if (FilmCheckV2 == 2)
                    {
                        FilmKiezenPremium(bioscoop, BiosIndex, VoorTerugFunctie);
                    }
                    if (FilmCheckV2 == 3)
                    {
                        BioscoopKiezenPremium();
                    }
                }
            }

            if (GerelateerdeFilmsv2 == 0)
            {
                Console.WriteLine("Er draaien momenteel geen films met hetzelfde genre.\nDruk op enter om terug naar de films te gaan");
                Console.ReadLine();
                FilmKiezenPremium(bioscoop, BiosIndex, VoorTerugFunctie);
            }
        }

        public void FilmVertoningenPremium(string bioscoop, int BiosIndex, string TweedeGekozenFilm, int GekozenFilm, IList<Film> VoorTerugFunctie)
        {
            Console.Clear();
            Console.WriteLine($"{bioscoop}");
            var Bioscoop = _database.Bioscopen[BiosIndex];
            Console.WriteLine($"{TweedeGekozenFilm}\n");
            var poging = this._database.Bioscopen[BiosIndex].Schermen.Count;
            int count = 1;
            int CountForExit = 1;
            var TupleTest = new List<Tuple<int, string>>
            {

            };

            foreach (var schermen in Bioscoop.Schermen)
            {
                var Scherm = schermen.Nummer - 1;
                foreach (var vertoningen in schermen.Vertoningen)
                {
                    var placeholder = vertoningen.Key;
                    var testje = this._database.Bioscopen[BiosIndex].Schermen[Scherm].Vertoningen[placeholder].Film.Titel;

                    if (TweedeGekozenFilm == testje)
                    {
                        var Doorgeven = vertoningen.Key;
                        Console.WriteLine($"({count}) {vertoningen.Key}");
                        count++;
                        TupleTest.Add(new Tuple<int, string>(Scherm, vertoningen.Key));

                    }
                    CountForExit++;
                    if (CountForExit == poging + 1)
                    {
                        Console.WriteLine("Typ het nummer van de tijd die u wilt bezoeken");
                        var GekozenVoorstelling = Console.ReadLine();
                        var isNumeric = int.TryParse(GekozenVoorstelling, out int GekozenVoorstellingV2);

                        while (GekozenVoorstellingV2 < 1 || GekozenVoorstellingV2 >= count)
                        {
                            Console.WriteLine("Ongeldige input, probeer het nog een keer");
                            GekozenVoorstelling = Console.ReadLine();
                            isNumeric = int.TryParse(GekozenVoorstelling, out GekozenVoorstellingV2);

                        }
                        int GekozenScherm = TupleTest[GekozenVoorstellingV2 - 1].Item1;
                        string VertoningKey = TupleTest[GekozenVoorstellingV2 - 1].Item2;
                        var test = this._database.Bioscopen[BiosIndex].Schermen[GekozenScherm].Vertoningen[VertoningKey];

                        Console.WriteLine($"U heeft gekozen voor de voorstelling van {VertoningKey}");

                        Console.WriteLine("Typ 1 als dit klopt, typ 2 als dit niet klopt of typ 3 als u een andere film wilt kiezen.");
                        var VertoningCheck = Console.ReadLine();
                        var isNumericV2 = int.TryParse(VertoningCheck, out int VertoningCheckV2);

                        while (VertoningCheckV2 < 1 || VertoningCheckV2 > 3)
                        {
                            Console.WriteLine("U kunt alleen maar 1, 2 of 3 kiezen.");
                            VertoningCheck = Console.ReadLine();
                            isNumericV2 = int.TryParse(VertoningCheck, out VertoningCheckV2);

                        }

                        if (VertoningCheckV2 == 1)
                        {
                            StoelenKiezenPremium(TweedeGekozenFilm, bioscoop, GekozenFilm, BiosIndex, VoorTerugFunctie, GekozenScherm, VertoningKey);
                        }
                        if (VertoningCheckV2 == 2)
                        {
                            FilmVertoningenPremium(bioscoop, BiosIndex, TweedeGekozenFilm, GekozenFilm, VoorTerugFunctie);
                        }
                        if (VertoningCheckV2 == 3)
                        {
                            FilmKiezenPremium(bioscoop, BiosIndex, VoorTerugFunctie);
                        }
                    }
                }
            }
        }

        public void StoelenKiezenPremium(string Film, string Bioscoop, int FilmIndex, int BiosIndex, IList<Film> VoorTerugFunctie, int GekozenScherm, string VertoningKey)
        {
            Console.Clear();
            var StoelKiezen1 = this._database.Bioscopen[BiosIndex].Schermen[GekozenScherm].Vertoningen[VertoningKey].Zitplaatsen;

            string UserName = Environment.UserName;

            Console.WriteLine(Bioscoop);
            Console.WriteLine("Film: " + Film);
            Console.WriteLine($"Tijd: {VertoningKey}");
            int BeschikbareZitplaatsen = 0;
            foreach (var i in StoelKiezen1)
            {
                if (i.Bezet == false)
                {
                    BeschikbareZitplaatsen++;
                }
            }
            Console.WriteLine("Zitplaatsen op rij 1 zijn love seats, deze zitplaatsen zijn bedoeld voor 2 personen\n");
            Console.WriteLine($"Er zijn {BeschikbareZitplaatsen} beschikbare zitplaatsen, hoeveel wilt u er reserveren\nU kunt ook 999 typen om een andere film kiezen");
            var ResetList = new List<int>();

            var Hoeveelheid = Console.ReadLine();
            var isNumeric = int.TryParse(Hoeveelheid, out int HoeveelheidV2);

            if (HoeveelheidV2 == 999)
            {
                FilmKiezen(Bioscoop, BiosIndex, VoorTerugFunctie);
            }

            while (HoeveelheidV2 < 1 || HoeveelheidV2 > StoelKiezen1.Count)
            {
                Console.WriteLine("Ongeldige input, probeer het nog een keer.");
                Hoeveelheid = Console.ReadLine();
                isNumeric = int.TryParse(Hoeveelheid, out HoeveelheidV2);

            }

            Console.WriteLine("Beschikbare zitplaatsen.\nTyp het linker nummer van de zitplaats die u wilt.");
            int StoelIndex = 1;
            string GekozenStoelen = "";
            string test = "";
            int HoeveelheidStoelen = StoelKiezen1.Count;
            var probeersel = ((HoeveelheidStoelen) / (StoelKiezen1[HoeveelheidStoelen - 1].Rij + 1));

            //Dit laat de stoelen en rijen zien in de console
            while (StoelIndex <= StoelKiezen1.Count)
            {
                int CurrentSeat = StoelIndex;

                if ((StoelIndex - 1) % probeersel == 0)
                {
                    test += ($"\nRij {StoelKiezen1[StoelIndex].Rij + 1}: ");
                }

                if (StoelKiezen1[StoelIndex - 1].Bezet == true)
                {
                    test += $"(-X-), ";
                }

                else
                {
                    test += $"({CurrentSeat}:{StoelKiezen1[StoelIndex - 1].Nummer + 1}), ";
                }
                StoelIndex++;
                if (StoelIndex == (StoelKiezen1.Count + 1))
                {
                    Console.WriteLine(test);
                }
            }

            while (HoeveelheidV2 > 0)
            {
                var GekozenStoel = Console.ReadLine();
                var isNumericV2 = int.TryParse(GekozenStoel, out int GekozenStoelV2);

                while (GekozenStoelV2 < 1 || GekozenStoelV2 > StoelKiezen1.Count)
                {
                    Console.WriteLine("Ongeldige input, probeer het nog een keer");
                    GekozenStoel = Console.ReadLine();
                    isNumericV2 = int.TryParse(GekozenStoel, out GekozenStoelV2);

                }
                while (StoelKiezen1[GekozenStoelV2 - 1].Bezet == true)
                {
                    Console.WriteLine("Ongeldige input, probeer het nog een keer");
                    GekozenStoel = Console.ReadLine();
                    isNumericV2 = int.TryParse(GekozenStoel, out GekozenStoelV2);
                }

                if (StoelKiezen1[GekozenStoelV2 - 1].Bezet == false)
                {
                    Console.WriteLine($"{StoelKiezen1[GekozenStoelV2 - 1].Rij + 1} {StoelKiezen1[GekozenStoelV2 - 1].Nummer}");
                    StoelKiezen1[GekozenStoelV2 - 1].Bezet = true;
                    StoelKiezen1[GekozenStoelV2 - 1].PremiumReservering = true;
                    StoelKiezen1[GekozenStoelV2 - 1].Naam = "Premium " + UserName;
                    ResetList.Add(GekozenStoelV2 - 1);
                    UpdateData();
                }
                Console.Clear();
                Console.WriteLine(Bioscoop);
                Console.WriteLine("Film: " + Film);
                Console.WriteLine($"U kunt nog {HoeveelheidV2 - 1} zitplaatsen kiezen.\nTyp het linker nummer van de zitplaats die u wilt.");


                StoelIndex = 1;
                test = "";
                while (StoelIndex <= StoelKiezen1.Count)
                {
                    int CurrentSeat = StoelIndex;

                    if ((StoelIndex - 1) % probeersel == 0)
                    {
                        test += ($"\nRij {StoelKiezen1[StoelIndex].Rij + 1}: ");
                    }
                    if (StoelKiezen1[StoelIndex - 1].Bezet == true)
                    {
                        test += $"(-X-), ";
                    }
                    else
                    {
                        test += $"({CurrentSeat}:{StoelKiezen1[StoelIndex - 1].Nummer + 1}), ";
                    }
                    StoelIndex++;
                    if (StoelIndex == (StoelKiezen1.Count + 1))
                    {
                        Console.WriteLine(test);
                    }
                }
                string text = $"({StoelKiezen1[GekozenStoelV2 - 1].Rij + 1}:{StoelKiezen1[GekozenStoelV2 - 1].Nummer + 1})\n";
                GekozenStoelen += text;
                Console.WriteLine($"Gekozen stoelen:\n{GekozenStoelen}");

                if (StoelKiezen1[GekozenStoelV2 - 1].Rij == 0)
                {
                    if (HoeveelheidV2 < 2)
                    {
                        Console.WriteLine("Als u een Love Seat wilt moet u 2 zitplaatsen selecteren.\nDruk op enter om door te gaan.");
                        Console.ReadLine();
                        foreach (var Stoel in ResetList)
                        {
                            StoelKiezen1[Stoel].Bezet = false;
                        }
                        UpdateData();

                        StoelenKiezenPremium(Film, Bioscoop, FilmIndex, BiosIndex, VoorTerugFunctie, GekozenScherm, VertoningKey);
                    }
                    else
                    {
                        HoeveelheidV2 -= 2;
                    }
                }
                else
                {
                    HoeveelheidV2--;
                }
            }
            if (HoeveelheidV2 == 0)
            {
                Console.Clear();
                Console.WriteLine(Bioscoop);
                Console.WriteLine($"Gekozen stoelen:\n{GekozenStoelen}");
                Console.WriteLine("Klopt dit?\nTyp 1 als het klopt of typ 2 als het niet klopt.");
                var DoorGeven = Console.ReadLine();
                var isNumericV2 = int.TryParse(DoorGeven, out int DoorGevenV2);

                while (DoorGevenV2 < 1 || DoorGevenV2 > 2)
                {
                    Console.WriteLine("U kunt alleen maar 1 of 2 kiezen");
                    DoorGeven = Console.ReadLine();
                    isNumericV2 = int.TryParse(DoorGeven, out DoorGevenV2);
                }

                if (DoorGevenV2 == 1)
                {
                    KostenPremium(Film, Bioscoop, ResetList, BiosIndex, GekozenScherm, VertoningKey);
                }
                else if (DoorGevenV2 == 2)
                {
                    foreach (var Stoel in ResetList)
                    {
                        StoelKiezen1[Stoel].Bezet = false;
                        StoelKiezen1[Stoel].Naam = null;
                        StoelKiezen1[Stoel].PremiumReservering = false;
                    }
                    ResetList.Clear();
                    UpdateData();
                    StoelenKiezenPremium(Film, Bioscoop, FilmIndex, BiosIndex, VoorTerugFunctie, GekozenScherm, VertoningKey);
                }
            }
        }

        public void KostenPremium(string Film, string Bioscoop, List<int> GekozenStoelen, int BiosIndex, int GekozenScherm, string VertoningKey)
        {
            Console.Clear();
            Console.WriteLine(Bioscoop);
            var StoelKiezen1 = this._database.Bioscopen[BiosIndex].Schermen[GekozenScherm].Vertoningen[VertoningKey].Zitplaatsen;

            var SoortScherm = this._database.Bioscopen[BiosIndex].Schermen[GekozenScherm].Vertoningen[VertoningKey].Film.Schermtype;
            //var Tickets = this._database.Tickets[0].TypeTicket;
            int LSCount = 0;
            int RSCount = 0;
            double TicketPrijs = 0;
            double Kosten = 0;
            //Console.WriteLine(Tickets);
            foreach (var Type in _database.Tickets)
            {
                if (Type.TypeTicket == SoortScherm)
                {
                    TicketPrijs = Type.TicketPrijs;
                }
            }
            Console.WriteLine($"De basisprijs van een {SoortScherm} film is {TicketPrijs} euro.\nEen Love Seat kost 2.50 extra\n");
            Console.WriteLine("Uw zitplaatsen:");

            foreach (int Stoel in GekozenStoelen)
            {
                if (StoelKiezen1[Stoel].Rij == 0)
                {
                    Console.WriteLine($"Love Seat:({StoelKiezen1[Stoel].Rij + 1}:{StoelKiezen1[Stoel].Nummer + 1})");
                    LSCount++;
                }
                else
                {
                    Console.WriteLine($"Reguliere zitplaats: ({StoelKiezen1[Stoel].Rij + 1}:{StoelKiezen1[Stoel].Nummer + 1})");
                    RSCount++;
                }
            }

            Kosten = TicketPrijs * GekozenStoelen.Count + (LSCount * 2.50);
            Console.WriteLine($"\nDe totale kosten zouden {Kosten} euro bedragen, maar u als premium klant betaald niks!");
            Console.WriteLine("\nDruk op 1 om terug te gaan naar het hoofdmenu.\nDruk op enter om het programma te verlaten");
            var Afsluiten = Console.ReadLine();
            var isNumeric = int.TryParse(Afsluiten, out int AfsluitenV2);

            if (isNumeric == false)
            {
                Environment.Exit(0);
            }
            if (AfsluitenV2 == 1)
            {
                var Startmenu = new Classq();
                Startmenu.test();
            }


        }

        //jim functies

        public void filmNaarBeschikbaar()
        {
            
            Console.WriteLine("Welkom bij het filmaanbod aanpas register schema! \n Aan welke Bioscoop wilt u de film toevoegen?");
            ToonBioscopen();
            int biosKeuze = Convert.ToInt32(Console.ReadLine());
            biosKeuze -= 1;

            Console.WriteLine($"\nDit zijn de huidige beschikbare films bij {_database.Bioscopen[biosKeuze].Naam}\n");

            //int filmIndex = 1;
            /*
            foreach (var _film in _database.Bioscopen[biosKeuze].BeschikbareFilms)
            {
                Console.WriteLine($"({filmIndex}) {_film.Titel}");
            }
            */

            _database.Bioscopen[biosKeuze].BeschikbareFilms.PrintAllWithIndex(x => x.Titel);
            Console.WriteLine("\n\n");


            Console.WriteLine("Deze films bevinden zich in de database en nog niet in het aanbod van deze bioscoop:\n");
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
                Console.WriteLine("\n\nWelke film wilt u toevoegen aan het bioscoop aanbod?");
                int filmKeuze = Convert.ToInt32(Console.ReadLine());
                if (witlijst.Contains(filmKeuze))
                {
                    _database.Bioscopen[biosKeuze].BeschikbareFilms.Add(_database.Films[filmKeuze]);
                    UpdateData();
                    
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
        public void beschikbareFilmsNaarVertoning()
        {
            Console.WriteLine("Welkom bij het voorstellings menu! \n");
            ToonBioscopen();
            Console.WriteLine("Aan welke bioscoop wilt u een voorstelling toevoegen? \n");

            int biosKeuze = Convert.ToInt32(Console.ReadLine());
            biosKeuze -= 1;

            var _bioscoop = _database.Bioscopen[biosKeuze];

            Console.WriteLine($"Dit zijn de huidige voorstellingen bij {_database.Bioscopen[biosKeuze].Naam}");

            int schermIndex = 1;

            foreach (var _schermen in _bioscoop.Schermen)
            {
                Console.WriteLine($"Voorstelling({schermIndex}):");
                schermIndex += 1;
                foreach (var _vertoning in _schermen.Vertoningen)
                {
                    var huidigeKey = _vertoning.Key;
                    Console.WriteLine(_schermen.Vertoningen[huidigeKey].Film.Titel);
                    Console.WriteLine(_schermen.Vertoningen[huidigeKey].Film.Datum);
                    Console.WriteLine("\n");
                    
                }
            }

            Console.WriteLine("Wilt u een vertoning toevoegen? \n Voer 'ja' in om door te gaan. Voer 'nee' in om terug te gaan naar het menu.");
            var keuze = Console.ReadLine();
            if (keuze == "Ja" || keuze == "ja")
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Welke Film uit het aanbod van deze bioscoop wilt u toevoegen als voorstelling?\n");
                toonBeschikbareFilms(biosKeuze);
                Console.WriteLine("Voer het nummer in van de film die u wilt toevoegen\n");
                var toevoegKeuze = Convert.ToInt32(Console.ReadLine()) -1;

                Console.WriteLine(toevoegKeuze);


                Console.WriteLine($"Op de locatie {_database.Bioscopen[biosKeuze].Naam} bevinden zich de volgende zalen: \n");
                foreach(var _scherm in _bioscoop.Schermen)
                {
                    Console.WriteLine($"Zaal nummer ({_scherm.Nummer})");
                }
                Console.WriteLine("Aan welke zaal wilt u een voorstelling toevoegen?\n");
                int zaalKeuze = Convert.ToInt32(Console.ReadLine()) -1;
                
                var gekozenzaal = _bioscoop.Schermen[zaalKeuze];
                int aantalRijen = 5;
                int stoelenPerRij = 10;

                //in de database staan de stoelen + rijen momenteel gekoppeld aan "zitplaatsen" wat zich binnenin een vertoning bevind
                //hierdoor zijn stoelen/rijen niet direct gekoppeld aan een zaal. Hierdoor kan ik ze nu niet makkelijk ophalen zonder een voorgaande vertoning aan te roepen
                //dit betekent dat ik dus niet een nieuwe vertoning aan kan maken met het aantal rijen/stoelen uit een zaal, zonder voorgaande vertoning
                //hierdoor kies ik nu er dus even voor om hard coded met 5 rijen aan stoelen te werken, met elk 10 stoelen per rij.

                //todo: geef zitplaatsen een eigen plek binnen de zaal in de database ipv de zitplaatsen in een vertoning te hebben staan.

                Console.WriteLine("Typ op welke datum de voorstelling moet draaien: voorbeeld 23-03-2020");
                string toevoegFilmDatum = Console.ReadLine();

                Console.WriteLine("Typ om welke tijd de voorstelling moet draaien: voorbeeld: 12:00");
                string toevoegFilmTijd = Console.ReadLine();

                string datum = toevoegFilmDatum + " " + toevoegFilmTijd;

                DateTime datum2 = Convert.ToDateTime(datum);

                //nieuwe vertoning aanmaken met aantal rijen, aantal stoelen, datetime

                var nieuweVertoning = new Vertoning(aantalRijen, stoelenPerRij, datum2);
               
                var toevoegFilm = _database.Bioscopen[biosKeuze].BeschikbareFilms[toevoegKeuze];
                toevoegFilm.Datum = toevoegFilmDatum;
                toevoegFilm.Tijd = toevoegFilmTijd;


                nieuweVertoning.Film = toevoegFilm;

                
                string toevoegKey = toevoegFilmDatum + " " + toevoegFilmTijd;

                Console.WriteLine(toevoegKey);

                
                _database.Bioscopen[biosKeuze].Schermen[zaalKeuze].Vertoningen.Add(toevoegKey, nieuweVertoning);

                UpdateData();

            }
            else if (keuze == "Nee" || keuze == "nee")
            {
                beschikbareFilmsNaarVertoning();

            }

            else
            {
                Console.WriteLine("foutieve invoer.");
                //Dit zou wellicht anders afgevangen kunnen worden
                beschikbareFilmsNaarVertoning();
            }
        }


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
        {
            //Volgens mij is deze functie werkend, alleen heb ik het alleen nog maar getest met weinig schermen / vertoningen
            //Zal dit nader testen als ik andere functies werkend heb waarmee ik die toe kan voegen aan de database.
            // ook kan ik nog geen film gegevens ophalen per vertoning omdat films nog niet gekoppeld zijn aan vertoningen :)
            Console.WriteLine("Van welke bioscoop wilt u de huidige voorstellingen zien?");
            ToonBioscopen();

            Console.WriteLine("\n Voer het nummer van de Bioscoop in waarvan u de voorstellingen wilt zien.");
            int antwoord = Convert.ToInt32(Console.ReadLine()) - 1;

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