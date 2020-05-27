using System;
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
                FilmsTonen(); 
            }
            else

            {
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
            foreach (Film i in this._database.Films)
            {
                Console.WriteLine($"({Index}) {i.Titel}, {i.Datum}, {i.Tijd}, {i.Genre}");
                Index++;
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
            Console.WriteLine("\nWelke ticket wilt u aanpassen?");
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

            Console.WriteLine("\nWelke film wilt u aanpassen?");
            int Index = 1;
            foreach (Film i in this._database.Films)
            {
                Console.WriteLine($"({Index}) {i.Titel}, {i.Datum}, {i.Tijd}, {i.Genre}");
                Index++;
            }
            Console.WriteLine("Welke film wilt u aanpassen? Typ het cijfer van de film");
            int filmaanpassen = Convert.ToInt32(Console.ReadLine());
            var Filmaanpas = this._database.Films[filmaanpassen - 1];
            Console.WriteLine($"Wilt u {Filmaanpas.Titel} aanpassen? Ja/Nee");
            string antwoord2 = Console.ReadLine();
            if (antwoord2 == "ja" || antwoord2 == "Ja")
            {
                Console.WriteLine($"Wat wilt u aanpassen aan {Filmaanpas.Titel}?");
                Console.WriteLine($"Wilt u de (1)Titel,(2)Tijd,(3)Datum,(4)Genre of (5)alles aanpassen? typ de index");
                int intantwoord = Convert.ToInt32(Console.ReadLine());
                if (intantwoord == 1)
                {
                    Console.WriteLine($"U wilt de titel van: {Filmaanpas.Titel} aanpassen.");
                    Console.WriteLine($"Typ de nieuwe titel van de film:");
                    string nieuwtitel = Console.ReadLine();
                    Console.WriteLine($"Wilt u de titel: {Filmaanpas.Titel} in {nieuwtitel} veranderen? Ja/Nee");
                    string antwoord = Console.ReadLine();
                    if (antwoord == "Ja" || antwoord == "ja")
                    {
                        Console.WriteLine($"De film {Filmaanpas.Titel} is veranderd in {nieuwtitel}.");
                        Filmaanpas.Titel = nieuwtitel;
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
                    Console.WriteLine($"(2) U wilt de tijd van {Filmaanpas.Titel} aanpassen.");
                    Console.WriteLine($"\nDe film: {Filmaanpas.Titel} draait om {Filmaanpas.Tijd}");
                    Console.WriteLine($"Typ de nieuwe tijd van de Film");
                    string nieuwtijd = Console.ReadLine();
                    Console.WriteLine($"Wilt u {Filmaanpas.Titel} om {Filmaanpas.Tijd} veranderen in {Filmaanpas.Titel} om {nieuwtijd}? Ja/Nee");
                    string antwoord = Console.ReadLine();
                    if (antwoord == "Ja" || antwoord == "ja")
                    {
                        Console.WriteLine($"De film {Filmaanpas.Titel} draait om {nieuwtijd}");
                        Filmaanpas.Tijd = nieuwtijd;
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
                else if (intantwoord == 3)
                {
                    Console.WriteLine($"(3) U wilt de datum van {Filmaanpas.Titel} aanpassen.");
                    Console.WriteLine($"\nDe film: {Filmaanpas.Titel} draait op {Filmaanpas.Datum}");
                    Console.WriteLine($"Typ de nieuwe datum van de film:  {Filmaanpas.Titel}");
                    string nieuwdatum = Console.ReadLine();
                    Console.WriteLine($"Wilt u {Filmaanpas.Titel} op {Filmaanpas.Datum} veranderen in {Filmaanpas.Titel} om {nieuwdatum}? Ja/Nee");
                    string antwoord = Console.ReadLine();
                    if (antwoord == "Ja" || antwoord == "ja")
                    {
                        Console.WriteLine($"\nDe film {Filmaanpas.Titel} draait op {nieuwdatum}");
                        Filmaanpas.Datum = nieuwdatum;
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
                else if (intantwoord == 4)
                {
                    Console.WriteLine($"(4) U wilt het genre van {Filmaanpas.Titel} aanpassen.");
                    Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft het genre: {Filmaanpas.Genre}");
                    Console.WriteLine($"Typ de nieuwe genre van de Film");
                    string nieuwgenre = Console.ReadLine();
                    Console.WriteLine($"Wilt u {Filmaanpas.Titel} met het genre {Filmaanpas.Genre} veranderen in {nieuwgenre}? Ja/Nee");
                    string antwoord = Console.ReadLine();
                    if (antwoord == "Ja" || antwoord == "ja")
                    {
                        Console.WriteLine($"De film {Filmaanpas.Titel} heeft het genre {nieuwgenre}");
                        Filmaanpas.Genre = nieuwgenre;
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
                    Console.WriteLine($"(5) U wilt alles van {Filmaanpas.Titel} aanpassen.");
                    Console.WriteLine($"\nDe film {Filmaanpas.Titel} heeft:");
                    Console.WriteLine($"De titel:{Filmaanpas.Titel}.");
                    Console.WriteLine($"De datum: {Filmaanpas.Datum}");
                    Console.WriteLine($"De tijd: {Filmaanpas.Tijd}");
                    Console.WriteLine($"Het genre: {Filmaanpas.Genre}");
                    Console.WriteLine($"\nTyp de nieuwe titel van de film:");
                    string nieuwtitel = Console.ReadLine();
                    Console.WriteLine("\nTyp de nieuwe datum van de film:");
                    string nieuwdatum = Console.ReadLine();
                    Console.WriteLine("\nTyp de nieuwe tijd van de film:");
                    string nieuwtijd = Console.ReadLine();
                    Console.WriteLine("\nTyp de ieuwe genre van de film");
                    string nieuwgenre = Console.ReadLine();
                    Console.WriteLine($"\nWilt u {Filmaanpas.Titel} aanpassen naar:");
                    Console.WriteLine("De titel: " + nieuwtitel);

                    string antwoord = Console.ReadLine();
                    if (antwoord == "Ja" || antwoord == "ja")
                    {
                        Console.WriteLine($"De film {Filmaanpas.Titel} heeft het genre {nieuwgenre}");
                        Filmaanpas.Genre = nieuwgenre;
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
            }




        }
        public void FilmToevoegen()
        {
            int Index = 1;
            foreach (Film i in this._database.Films)
            {
                Console.WriteLine(i.Titel);
                Index++;
            }
            Console.WriteLine("\nTyp de Titel van de film die u wilt toevoegen:");
            string filmtoevoegen = Console.ReadLine();
            Console.WriteLine("\nWilt u " + filmtoevoegen + "toevoegen? Ja/Nee");
            string antwoord = Console.ReadLine();
            if (antwoord == "Ja" || antwoord == "ja")
            {
                Console.WriteLine("Typ om welke tijd de film" + filmtoevoegen + "draait, voorbeeld: 12.00");
                string tijdfilm = Console.ReadLine();
                Console.WriteLine("Typ op welke datum " + filmtoevoegen + "draait: voorbeeld 23-03-2020");
                string datumfilm = Console.ReadLine();
                Console.WriteLine("Typ welke genre de film" + filmtoevoegen + "heeft:");
                string genrefilm = Console.ReadLine();
                Console.WriteLine("Wilt u de film met de titel " + filmtoevoegen + " en de tijd om " + tijdfilm + " op " + datumfilm + " met het genre " + genrefilm + " toevoegen? Ja/Nee");
                string finalfilm = Console.ReadLine();
                string beschrijving = Console.ReadLine();
                if (finalfilm == "ja" || finalfilm == "Ja")
                {
                    var nieuwfilm = new Film(filmtoevoegen, tijdfilm, datumfilm, genrefilm, beschrijving);
                    AddFilm(nieuwfilm);
                    UpdateData();
                    int Index2 = 1;
                    foreach (Film i in this._database.Films)
                    {
                        Console.WriteLine(i.Titel);
                        Index2++;
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
            int Index = 1;
            foreach (Film i in this._database.Films)
            {
                Console.WriteLine("(" + Index + ")" + i.Titel + ", " + i.Datum + ", " + i.Tijd + ".");
                Index++;
            }
            Console.WriteLine("Wilt u een film verwijderen? Ja/Nee");
            string verwijder = Console.ReadLine();
            if (verwijder == "Ja" || verwijder == "ja")
            {
                Console.WriteLine("Welke film wilt u verwijderen? Typ het cijfer van de film");
                int verwijderfilm = Int32.Parse(Console.ReadLine());
                Console.WriteLine($"Wilt u { this._database.Films[verwijderfilm - 1].Titel} verwijderen? Ja/Nee");
                var gekozenfilm = this._database.Films[verwijderfilm - 1];
                string removefilm = Console.ReadLine();
                if (removefilm == "ja" || removefilm == "Ja")
                {
                    this._database.Films.Remove(gekozenfilm);
                    UpdateData();
                    Console.WriteLine("\nUw beschibare films zijn:");
                    foreach (Film i in this._database.Films)
                    {
                        Console.WriteLine("(" + Index + ")" + i.Titel + ", " + i.Datum + ", " + i.Tijd + ".");
                        Index++;
                    }
                    StartMenu();
                }
                else if (removefilm == "nee" || removefilm != "Ja")
                {
                    Console.WriteLine("\nWilt u een andere film verwijderen of terug naar startmenu? V/S ");
                    string startantwoord = Console.ReadLine();
                    if (startantwoord == "V" || startantwoord == "v")
                    {
                        FilmVerwijderen();
                    }
                    else if (startantwoord == "S" || startantwoord == "s")
                    {
                        StartMenu();
                    }

                }
                else
                {
                    StartMenu();
                }
            }
            else
            {
                StartMenu();
            }
        }

    }

}


