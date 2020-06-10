﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JimFilmsTake2.Model;
using Newtonsoft.Json;
namespace JimFilmsTake2.Db
{
    public class FilmRepository
    {

        private JsonModel _database { get; set; }


        public FilmRepository()
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

        public void AddFilm(Film film)
        {
            // Checken of de bioscoop niet al bestaat, dit doet de contains functie voor ons.
            foreach (var _film in _database.Films)
            {
                if (_film.Titel == film.Titel)
                {
                    throw new Exception($"Film met de naam {film.Titel} bestaat al in de database");
                }
            }
            _database.Films.Add(film);
        }
        public IList<Film> GetFilms()
        {
            return _database.Films;
        }

        public void StartMenu()

        {
            Console.WriteLine("\nSelecteer uit de volgende opties:\n");

            Console.WriteLine("\n(1) beschikbare films aanpassen.\n(2) film toevoegen.\n(3) film verwijderen.\n(4) omzet tonen.\n(5) films tonen");
            Console.Write("Optie : ");

            int OptieKiezen = Int32.Parse(Console.ReadLine());
            Optie(OptieKiezen);
        }

        public void Optie(int OptieKiezen)

        {
            Console.WriteLine("\nU heeft gekozen voor optie (" + (OptieKiezen) + ")");
            if (OptieKiezen == 1)
            {
                // verwijzing naar functie 1 
                FilmAanpassen();
            }
            else if (OptieKiezen == 2)
            {
                // verwijzing naar functie 2 
                FilmToevoegen();
            }
            else if (OptieKiezen == 3)
            {
                // verwijzing naar functie 3 
                FilmVerwijderen();
            }
            else if (OptieKiezen == 4)
            {
                // verwijzing naar functie 4 
                //Programs.TijdenAanpassen(totalfilms); 
            }
            else if (OptieKiezen == 5)
            {
                // verwijzing naar functie 5 
                Console.WriteLine("De beschikbare films zijn:");
                VolledigFilmsTonen();
                StartMenu();

            }
            else if (OptieKiezen == 6)

            {
                //verwijzing naar functie 6

                Console.WriteLine("\n\nU heeft niet gekozen uit de beschikbare opties.");
                //Programs.StartMenu(totalfilms); 
            }
        }
        public void AdminMenu()
        {
            Console.WriteLine("\nSelecteer uit de volgende opties\n");
            Console.WriteLine("(1) Omzet tonen.\n(2) Tickets aanpassen.\n(3) Beschikbare films aanpassen.\n(4) Film toevoegen.\n(5) Film verwijderen.\n(6) Films tonen.");
            Console.Write("\nOptie : ");
            int AdminOptie = Int32.Parse(Console.ReadLine());
            GekozenAdminOptie(AdminOptie);
        }
        public void GekozenAdminOptie(int AdminOptie)
        {
            if (AdminOptie == 1)
            {
                // verwijzing naar functie omzet tonen
                // is nog niet aangemaakt
            }
            else if (AdminOptie == 2)
            {
                // verwijzing naar functie Tickets aanpassen
                TicketAanpassen();
                // is nog aangemaakt
            }
            else if (AdminOptie == 3)
            {
                // verwijzing naar functie FilmAanpassen
                FilmAanpassen();
            }
            else if (AdminOptie == 4)
            {
                // verwijzing naar functie FilmToevoegen
                FilmToevoegen();
            }
            else if (AdminOptie == 5)
            {
                // verwijzing naar functie FilmVerwijderen
                FilmVerwijderen();
            }
            else if (AdminOptie == 6)
            {
                // verwijzing naar functie Films tonen
                // Programs.TijdenAanpassen(totalfilms); 
            }
        }
        public void FilmsTonen() //Films tonen
        {
            int Index = 1;
            Console.WriteLine("\nAlle beschikbare films: ");
            foreach (Film i in this._database.Films)
            {
                Console.WriteLine($"({Index}) {i.Titel}");
                Index++;
            }
        }
        public void VolledigFilmsTonen()
        {
            int Index = 1;
            Console.WriteLine("\nAlle beschikbare films: ");
            foreach (Film i in this._database.Films)
            {
                Console.WriteLine($"({Index}) {i.Titel}");
                Index++;
            }
            Console.WriteLine("Kies met index voor meer informatie over de film.");
            int indexfilm = Convert.ToInt32(Console.ReadLine());
            var filmkies = this._database.Films[indexfilm];
            if(indexfilm > Index || indexfilm <= 0)
            {
                Console.WriteLine("de index klopt niet, probeer het nog een keer");
                VolledigFilmsTonen();
            }
            else
            {
                Console.WriteLine($"Titel: {filmkies.Titel},\n");
                Console.WriteLine("Terug naar films tonen of naar het menu? F/M");
                string keuze = Console.ReadLine();
                if(keuze == "F" || keuze == "f")
                {
                    Console.WriteLine("Welkom terug bij de optie: films tonen");
                    VolledigFilmsTonen();
                }
                else if(keuze == "M" || keuze == "m")
                {
                    Console.WriteLine("Welkom terug bij het menu");
                    StartMenu();
                }
                else
                {
                    Console.WriteLine("We sturen u terug naar het menu");
                    StartMenu();
                }
            }
        }
        public void TicketsTonen() //Tickets tonen
        {
            int Index = 1;
            foreach (Ticket i in this._database.Tickets)
            {
                Console.WriteLine($"({Index}) {i.TypeTicket} : {i.TicketPrijs}");
                Index++;
            }
        }
        public void TicketAanpassen() //Tickets aanpassen
        {
            Console.WriteLine("\nBeschikbare tickets\n");
            int Index = 1;

            foreach (Ticket i in this._database.Tickets)
            {
                Console.WriteLine($"({Index}) {i.TypeTicket} : {i.TicketPrijs}");
                Index++;
            }
            Console.WriteLine("\nWelk ticket wilt u aanpassen?");
            Console.Write("\nTyp het cijfer van de ticket in : ");
            int ticketkiezen = Convert.ToInt32(Console.ReadLine());
            var ticketkeuze = this._database.Tickets[ticketkiezen - 1];
            Console.WriteLine($"Wilt u {ticketkeuze.TypeTicket} aanpassen?");
            Console.Write("Typ 'Ja' of 'Nee' : ");
            string beslissing = Console.ReadLine();
            if (beslissing == "ja" || beslissing == "Ja")
            {
                Console.WriteLine($"\nWat wilt u aanpassen aan de {ticketkeuze.TypeTicket}?");
                Console.WriteLine("Wilt u (1) de ticket naam aanpassen of (2) de ticketprijs aanpassen?");
                Console.Write("\nTyp het cijfer van uw keuze in : ");
                int ticketaanpassing = Convert.ToInt32(Console.ReadLine());
                if (ticketaanpassing == 1)
                {
                    Console.WriteLine($"\nU wilt de ticket naam ({ticketkeuze.TypeTicket}) aanpassen.");
                    Console.Write($"Typ de nieuwe ticket naam in voor ({ticketkeuze.TypeTicket}) : ");
                    string nieuweticketnaam = Console.ReadLine();
                    Console.WriteLine($"Wilt u de ticket naam: {ticketkeuze.TypeTicket} veranderen in : {nieuweticketnaam}?");
                    Console.Write("Typ 'Ja' of 'Nee' : ");
                    string beslissing2 = Console.ReadLine();
                    if (beslissing2 == "Ja" || beslissing2 == "ja")
                    {
                        Console.WriteLine($"\n{ticketkeuze.TypeTicket} is veranderd naar : {nieuweticketnaam}.\n");
                        ticketkeuze.TypeTicket = nieuweticketnaam;
                        UpdateData();
                        TicketsTonen();
                        AdminMenu();
                    }
                    else
                    {
                        Console.WriteLine("\nWilt u een andere ticket aanpassen?");
                        Console.Write("Typ 'Ja' of 'Nee' : ");
                        string beslissing3 = Console.ReadLine();
                        if (beslissing3 == "Ja" || beslissing3 == "ja")
                        {
                            TicketAanpassen();
                        }
                        else
                        {
                            Console.WriteLine("U wordt doorgestuurd naar het Administrator menu.");
                            AdminMenu();
                        }
                    }
                }
                else if (ticketaanpassing == 2)
                {
                    Console.WriteLine($"\nU wilt de ticketprijs aanpassen van ({ticketkeuze.TypeTicket})\nhuidige ticketprijs voor deze ticket = ({ticketkeuze.TicketPrijs})");
                    Console.Write($"Typ de nieuwe ticketprijs in voor ({ticketkeuze.TypeTicket}) : ");
                    double nieuweticketprijs = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine($"Wilt u de huidige ticketprijs : ({ticketkeuze.TicketPrijs}) veranderen naar : ({nieuweticketprijs})?");
                    Console.Write("Typ 'Ja' of 'Nee' : ");
                    string beslissing4 = Console.ReadLine();
                    if (beslissing4 == "Ja" || beslissing4 == "ja")
                    {
                        Console.WriteLine($"\nDe ticketprijs van ({ticketkeuze.TypeTicket}) is veranderd naar ({nieuweticketprijs})");
                        ticketkeuze.TicketPrijs = nieuweticketprijs;
                        UpdateData();
                        TicketsTonen();
                        AdminMenu();
                    }
                    else
                    {
                        Console.WriteLine("\nWilt u een andere ticket aanpassen?");
                        Console.Write("Typ 'Ja' of 'Nee' : ");
                        string beslissing3 = Console.ReadLine();
                        if (beslissing3 == "Ja" || beslissing3 == "ja")
                        {
                            TicketAanpassen();
                        }
                        else
                        {
                            Console.WriteLine("U wordt doorgestuurd naar het Administrator menu.");
                            AdminMenu();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("U wordt doorgestuurd naar het Administrator menu.");
                    AdminMenu();
                }
            }
            else
            {
                Console.WriteLine("U wordt doorgestuurd naar het Administrator menu.");
                AdminMenu();
            }

        }

        public void FilmAanpassen() //Films aanpassen 
        {

            Console.WriteLine("\nWelkom bij de optie: film aanpassen.");
            FilmsTonen();
            Console.WriteLine("Welke film wilt u aanpassen? Typ het cijfer van de film");
            int filmaanpassen = Convert.ToInt32(Console.ReadLine());
            if (filmaanpassen > this._database.Films.Count || filmaanpassen <= 0)
            {
                Console.WriteLine("Probeer het nog een keer");
                FilmAanpassen();
            }
            else
            {
                var Filmaanpas = this._database.Films[filmaanpassen - 1];
                Console.WriteLine($"Wilt u {Filmaanpas.Titel} aanpassen? Ja/Nee");
                string antwoord2 = Console.ReadLine();
                if (antwoord2 == "ja" || antwoord2 == "Ja")
                {
                    Console.Clear();
                    Console.WriteLine("Titel: " + Filmaanpas.Titel + "\nGenre: " + Filmaanpas.Genre + "\nBeschrijving: " + Filmaanpas.Beschrijving + "\nDatum: " + Filmaanpas.Datum + "\nTijd: " + Filmaanpas.Tijd + "\nSchermtype: " + Filmaanpas.Schermtype + "\nSpeelduur: " + Filmaanpas.SpeelDuur);
                    Console.WriteLine($"Wat wilt u aanpassen aan {Filmaanpas.Titel}?");
                    Console.WriteLine($"Wat wilt u aanpassen:\n(1)Titel\n(2)Genre\n(3)Beschrijving\n(4)Datum\n(5)Tijd\n(6)Schermtype\n(7)Speelduur\n(8)Alles aanpassen");
                    int intantwoord = Convert.ToInt32(Console.ReadLine());
                    if (intantwoord == 1)
                    {
                        Console.WriteLine($"U wilt de titel van: {Filmaanpas.Titel} aanpassen.");
                        Console.WriteLine($"Typ de nieuwe titel van de film:");
                        string nieuwtitels = Console.ReadLine();
                        Console.WriteLine($"Wilt u de titel: {Filmaanpas.Titel} in {nieuwtitels} veranderen? Ja/Nee");
                        string antwoord5 = Console.ReadLine();
                        if (antwoord5 == "Ja" || antwoord5 == "ja")
                        {
                            Console.WriteLine($"De film {Filmaanpas.Titel} is veranderd in {nieuwtitels}.");
                            Filmaanpas.Titel = nieuwtitels;
                            UpdateData();
                            FilmsTonen();
                            StartMenu();
                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                FilmAanpassen();
                            }
                            else
                            {
                                Console.WriteLine("We sturen u terug naar het Startmenu.");
                                StartMenu();
                            }

                        }
                    }
                    else if (intantwoord == 2)
                    {
                        Console.WriteLine($"(2) U wilt het genre van {Filmaanpas.Titel} aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft het genre: {Filmaanpas.Genre}");
                        Console.WriteLine("Kies welke genre de film" + Filmaanpas.Titel + " heeft:");
                        List<string> genre = new List<string>()
                    {
                    "Horror","Comedie","Actie", "Documentaire", "Romantiek", "Animatie", "Drama", "Familiefilm"

                    };
                        int index = 1;
                        for (int i = 0; i < genre.Count; i++)
                        {
                            Console.WriteLine($"{index} {genre[i]}");
                            index++;
                        }
                        Console.WriteLine($"Typ de index van het nieuwe genre van de Film");
                        int nieuwgenre1 = Convert.ToInt32(Console.ReadLine());
                        string genrefilm = genre[nieuwgenre1 - 1];
                        Console.WriteLine($"Wilt u {Filmaanpas.Titel} met het genre {Filmaanpas.Genre} veranderen in {genrefilm}? Ja/Nee");
                        string antwoord1 = Console.ReadLine();
                        if (antwoord1 == "Ja" || antwoord1 == "ja")
                        {
                            Console.WriteLine($"De film {Filmaanpas.Titel} heeft het genre {genrefilm}");
                            Filmaanpas.Genre = genrefilm;
                            UpdateData();
                            FilmsTonen();
                            StartMenu();
                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                FilmAanpassen();
                            }
                            else
                            {
                                Console.WriteLine("We sturen u terug naar het Startmenu.");
                                Console.Clear();
                                StartMenu();
                            }

                        }

                    }
                    else if (intantwoord == 3)
                    {
                        Console.WriteLine($"(3) U wilt de beschrijving van {Filmaanpas.Titel} aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft als beschrijving:\n {Filmaanpas.Beschrijving}");
                        Console.WriteLine($"Typ de nieuwe beschrijving van de film:  {Filmaanpas.Beschrijving}");
                        string nieuwbeschrijving = Console.ReadLine();
                        Console.WriteLine($"Wilt u de oude beschrijving:\n {Filmaanpas.Beschrijving} \nveranderen naar de nieuwe beschrijving:\n {nieuwbeschrijving}\n ? Ja/Nee");
                        string antwoord3 = Console.ReadLine();
                        if (antwoord3 == "Ja" || antwoord3 == "ja")
                        {
                            Console.WriteLine($"\nDe film {Filmaanpas.Titel}:\nBeschrijving: {nieuwbeschrijving}");
                            Filmaanpas.Datum = nieuwbeschrijving;
                            UpdateData();
                            FilmsTonen();
                            StartMenu();
                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord6 = Console.ReadLine();
                            if (antwoord6 == "Ja" || antwoord6 == "ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                FilmAanpassen();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij het menu.");
                                StartMenu();
                            }

                        }
                    }
                    else if (intantwoord == 4)
                    {
                        Console.WriteLine($"(3) U wilt de datum van {Filmaanpas.Titel} aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} draait op {Filmaanpas.Datum}");
                        Console.WriteLine($"Typ de nieuwe datum van de film:  {Filmaanpas.Titel}");
                        string nieuwdatum2 = Console.ReadLine();
                        Console.WriteLine($"Wilt u {Filmaanpas.Titel} op {Filmaanpas.Datum} veranderen in {Filmaanpas.Titel} om {nieuwdatum2}? Ja/Nee");
                        string antwoorden = Console.ReadLine();
                        if (antwoorden == "Ja" || antwoorden == "ja")
                        {
                            Console.WriteLine($"\nDe film {Filmaanpas.Titel} draait op {nieuwdatum2}");
                            Filmaanpas.Datum = nieuwdatum2;
                            UpdateData();
                            FilmsTonen();
                            StartMenu();
                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                FilmAanpassen();
                            }
                            else
                            {
                                Console.WriteLine("We sturen u terug naar het Startmenu.");
                                StartMenu();
                            }

                        }
                    }

                    else if (intantwoord == 5)
                    {
                        Console.WriteLine($"(2) U wilt de tijd van {Filmaanpas.Titel} aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} draait om {Filmaanpas.Tijd}");
                        Console.WriteLine($"Typ de nieuwe tijd van de Film");
                        string nieuwtijden = Console.ReadLine();
                        Console.WriteLine($"Wilt u {Filmaanpas.Titel} om {Filmaanpas.Tijd} veranderen in {Filmaanpas.Titel} om {nieuwtijden}? Ja/Nee");
                        string tijd = Console.ReadLine();
                        if (tijd == "Ja" || tijd == "ja")
                        {
                            Console.WriteLine($"De film {Filmaanpas.Titel} draait om {nieuwtijden}");
                            Filmaanpas.Tijd = nieuwtijden;
                            UpdateData();
                            FilmsTonen();
                            StartMenu();
                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                FilmAanpassen();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij het menu.");
                                StartMenu();
                            }

                        }
                    }
                    else if (intantwoord == 6)
                    {
                        Console.WriteLine($"(2) U wilt het schermtype van {Filmaanpas.Titel} aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft het schermtype: {Filmaanpas.Schermtype}.");
                        Console.WriteLine("Kies welke schermtype" + Filmaanpas.Titel + "heeft: ");
                        int Index2 = 1;
                        foreach (Ticket i in this._database.Tickets)
                        {
                            Console.WriteLine($"({Index2}) {i.TypeTicket}");
                            Index2++;
                        }
                        Console.WriteLine("Kies de index van het nieuwe schermtype:");
                        int schermtypes = Convert.ToInt32(Console.ReadLine());
                        var schermkeuze = this._database.Tickets[schermtypes - 1].TypeTicket;
                        Console.WriteLine($"Wilt u {Filmaanpas.Titel} met het schermtype: {Filmaanpas.Schermtype} veranderen in {schermkeuze}? Ja/Nee");
                        string scherm = Console.ReadLine();
                        if (scherm == "Ja" || scherm == "ja")
                        {
                            Console.WriteLine($"De film {Filmaanpas.Titel} heeft het schermtype: {schermkeuze}");
                            Filmaanpas.Schermtype = schermkeuze;
                            UpdateData();
                            FilmsTonen();
                            StartMenu();
                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                FilmAanpassen();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij het menu.");
                                StartMenu();
                            }

                        }
                    }
                    else if (intantwoord == 7)
                    {
                        Console.WriteLine($"(7) U wilt de speelduur van {Filmaanpas.Titel} aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft een speelduur van: {Filmaanpas.SpeelDuur} minuten.");
                        Console.WriteLine("Typ de nieuwe speelduur van " + Filmaanpas.Titel + "in minuten. Voorbeeld: 172");
                        int speelduur = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine($"Wilt u {Filmaanpas.Titel} met een speelduur van {Filmaanpas.SpeelDuur} minuten veranderen in {speelduur} minuten? Ja/Nee");
                        string scherm = Console.ReadLine();
                        if (scherm == "Ja" || scherm == "ja")
                        {
                            Console.WriteLine($"De film {Filmaanpas.Titel} heeft een speelduur van: {speelduur} minuten");
                            Filmaanpas.SpeelDuur = speelduur;
                            UpdateData();
                            FilmsTonen();
                            StartMenu();
                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                FilmAanpassen();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij het menu.");
                                StartMenu();
                            }

                        }

                    }
                    else if (intantwoord == 8)
                    {
                        Console.WriteLine($"(8) U wilt alles aanpassen van de film {Filmaanpas.Titel}.");
                        Console.WriteLine("\nTyp de nieuwe Titel van de film: " + Filmaanpas.Titel);
                        string filmtoevoegen = Console.ReadLine();
                        Console.WriteLine("Kies welke genre de film" + filmtoevoegen + " heeft:");
                        List<string> genre = new List<string>()
                    {
                        "Horror","Comedie","Actie", "Documentaire", "Romantiek", "Animatie", "Drama", "Familiefilm"

                    };
                        int index = 1;
                        for (int i = 0; i < genre.Count; i++)
                        {
                            Console.WriteLine($"{index} {genre[i]}");
                            index++;
                        }
                        Console.WriteLine("Typ de index van het nieuwe genre: ");
                        int gekozen = Convert.ToInt32(Console.ReadLine());
                        string genrefilm = genre[gekozen - 1];
                        Console.WriteLine("Typ de nieuwe beschrijving van de film " + filmtoevoegen + " heeft:");
                        string beschrijvingfilm = Console.ReadLine();
                        Console.WriteLine("Typ op welke nieuwe datum " + filmtoevoegen + " draait: voorbeeld 23-03-2020");
                        string datumfilm = Console.ReadLine();
                        Console.WriteLine("Typ om welke tijd de film" + filmtoevoegen + " draait, voorbeeld: 12.00");
                        string tijdfilm = Console.ReadLine();
                        Console.WriteLine("Kies welke schermtype bij de film moet, typ de index");
                        int Index2 = 1;
                        foreach (Ticket i in this._database.Tickets)
                        {
                            Console.WriteLine($"({Index2}) {i.TypeTicket}");
                            Index2++;
                        }
                        int schermtypes = Convert.ToInt32(Console.ReadLine());
                        var schermkeuze = this._database.Tickets[schermtypes - 1].TypeTicket;
                        Console.WriteLine("Typ de speelduur van de film in minuten, voorbeeld: 183");
                        int speelmin = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("\nDe gegevens van de nieuwe film:");
                        Console.WriteLine("Titel: " + filmtoevoegen + "\nGenre: " + genrefilm + "\nBeschrijving: " + beschrijvingfilm + "\nDatum: " + datumfilm + "\nTijd: " + tijdfilm + "\nSchermtype: " + schermkeuze + "\nSpeelduur: " + speelmin);
                        Console.WriteLine("Wilt u de nieuwe gegevens toevoegen aan " + filmtoevoegen + "? Ja/Nee");
                        var nieuwfilm = Console.ReadLine();
                        if (nieuwfilm == "Ja" || nieuwfilm == "ja")
                        {
                            Filmaanpas.Titel = filmtoevoegen;
                            Filmaanpas.Tijd = tijdfilm;
                            Filmaanpas.Datum = datumfilm;
                            Filmaanpas.Genre = genrefilm;
                            Filmaanpas.Beschrijving = beschrijvingfilm;
                            Filmaanpas.Schermtype = schermkeuze;
                            Filmaanpas.SpeelDuur = speelmin;
                            Console.WriteLine("De nieuwe gegevens zijn toegevoegd");
                            UpdateData();
                            FilmsTonen();
                            StartMenu();

                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                FilmAanpassen();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij het menu.");
                                StartMenu();
                            }

                        }

                    }
                }
            }
        }

    public void FilmToevoegen()
        {
            int Index = 1;
            Console.WriteLine("Alle beschikbare films:");
            foreach (Film i in this._database.Films)
            {
                Console.WriteLine(i.Titel);
                Index++;
            }
            Console.WriteLine("\nTyp de Titel van de film die u wilt toevoegen:");
            string filmtoevoegen = Console.ReadLine();
            Console.WriteLine("\nWilt u " + filmtoevoegen + " toevoegen? Ja/Nee");
            string antwoord = Console.ReadLine();
            if (antwoord == "Ja" || antwoord == "ja")
            {
                Console.WriteLine("Kies welke genre de film" + filmtoevoegen + " heeft:");
                List<string> genre = new List<string>()
                {
                    "Horror","Comedie","Actie", "Documentaire", "Romantiek", "Animatie", "Drama", "Familiefilm"

                };
                int index = 1;
                for (int i = 0; i < genre.Count; i++)
                {
                    Console.WriteLine($"{index} {genre[i]}");
                    index++;
                }
                Console.WriteLine("Typ de index van het genre: ");
                int gekozen = Convert.ToInt32(Console.ReadLine());
                string genrefilm = genre[gekozen - 1];
                Console.WriteLine("Typ de beschrijving van de film " + filmtoevoegen + " heeft:");
                string beschrijvingfilm = Console.ReadLine();
                Console.WriteLine("Typ op welke datum " + filmtoevoegen + " draait: voorbeeld 23-03-2020");
                string datumfilm = Console.ReadLine();
                Console.WriteLine("Typ om welke tijd de film" + filmtoevoegen + " draait, voorbeeld: 12.00");
                string tijdfilm = Console.ReadLine();
                Console.WriteLine("Kies welke schermtype bij de film moet, typ de index");
                int Index2 = 1;
                foreach (Ticket i in this._database.Tickets)
                {
                    Console.WriteLine($"({Index2}) {i.TypeTicket}");
                    Index2++;
                }
                int schermtypes = Convert.ToInt32(Console.ReadLine());
                var schermkeuze = this._database.Tickets[schermtypes - 1].TypeTicket;
                Console.WriteLine("Typ de speelduur van de film in minuten, voorbeeld: 183");
                int speelmin = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("De gegevens van de nieuwe film:");
                Console.WriteLine("Titel: " + filmtoevoegen + "\nGenre: " + genrefilm + "\nBeschrijving: " + beschrijvingfilm + "\nDatum: " + datumfilm + "\nTijd: " + tijdfilm + "\nSchermtype: " + schermkeuze + "\nSpeelduur: " + speelmin);


                Console.WriteLine("\nWilt u de film met de bovenstaande gegevens toevoegen? Ja/Nee");
                string finalfilm = Console.ReadLine();
                string beschrijving = Console.ReadLine();
                if (finalfilm == "ja" || finalfilm == "Ja")
                {

                    Console.WriteLine("De film is succesvol toegevoegd");
                    var nieuwfilm = new Film(filmtoevoegen, tijdfilm, datumfilm, genrefilm, beschrijving, schermkeuze, speelmin);
                    AddFilm(nieuwfilm);
                    UpdateData();
                    int Indexz = 1;
                    foreach (Film i in this._database.Films)
                    {
                        Console.WriteLine(i.Titel);
                        Indexz++;
                    }
                    StartMenu();

                }
                else if (finalfilm == "Nee" || finalfilm != "ja" || finalfilm != "Ja")
                {
                    Console.WriteLine("Wilt u een andere film toevoegen? Ja/ Nee");
                    string anderefilm = Console.ReadLine();
                    if (anderefilm == "ja" || anderefilm == "Ja")
                    {
                        FilmToevoegen();
                    }
                    else
                    {
                        StartMenu();
                    }
                }

            }
            else if (antwoord == "nee" || antwoord != "ja")
            {
                StartMenu();
            }

        }
        public void FilmVerwijderen()
        {
            Console.WriteLine("\nAlle beschikbare films: ");
            FilmsTonen();
            Console.WriteLine("\nWilt u een film verwijderen? Ja/Nee");
            string verwijder = Console.ReadLine();
            if (verwijder == "Ja" || verwijder == "ja")
            {
                Console.WriteLine("\nWelke film wilt u verwijderen? Typ het cijfer van de film");
                int verwijderfilm = Int32.Parse(Console.ReadLine());
                Console.Clear();
                if ((verwijderfilm - 1) >= this._database.Films.Count)
                {
                    Console.Write("De ingevoerde index klopt niet, probeer nog een keer");
                    FilmVerwijderen();
                }
                else
                {
                    Console.WriteLine($"Wilt u { this._database.Films[verwijderfilm - 1].Titel} verwijderen? Ja/Nee");
                    var gekozenfilm = this._database.Films[verwijderfilm - 1];
                    string removefilm = Console.ReadLine();
                    if (removefilm == "ja" || removefilm == "Ja")
                    {
                        Console.WriteLine("De film: " + this._database.Films[verwijderfilm - 1].Titel + " is verwijderd. ");
                        this._database.Films.Remove(gekozenfilm);
                        UpdateData();
                        Console.WriteLine("\nUw beschibare films zijn:");
                        FilmsTonen();
                        StartMenu();
                    }
                    else if (removefilm == "nee" || removefilm != "Ja")
                    {
                        Console.WriteLine("\nWilt u een andere film verwijderen of terug naar startmenu? V/S ");
                        string startantwoord = Console.ReadLine();
                        if (startantwoord == "V" || startantwoord == "v")
                        {
                            Console.WriteLine("Welkom terug bij de optie: film verwijderen.");
                            FilmVerwijderen();
                        }
                        else if (startantwoord == "S" || startantwoord == "s")
                        {
                            Console.Clear();
                            Console.WriteLine("Welkom terug bij het menu.");
                            StartMenu();
                        }
                        else
                        {
                            Console.WriteLine("\nWelkom terug bij het menu");
                            StartMenu();
                        }

                    }

                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nWelkom terug bij het menu");
                        StartMenu();
                    }
                }
            }

        }

    }
}


