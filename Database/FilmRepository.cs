﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JimFilmsTake2.Model;
using Newtonsoft.Json;
using registratie88888888;

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

        public void StartMenu()//begin menu van medewerker

        {
            Console.WriteLine("\nSelecteer uit de volgende opties:");

            Console.WriteLine("\n(1) Beschikbare films aanpassen\n(2) Film toevoegen\n(3) Film verwijderen\n(4) Omzet tonen\n(5) Films tonen\n(6) Uitloggen");
            Console.Write("Optie : ");

            int OptieKiezen = Int32.Parse(Console.ReadLine());
            Optie(OptieKiezen);
        }

        public void Optie(int OptieKiezen) //hiermee roep je alle functies aan voor medewerker

        {
            Console.Clear();
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
                // OmzetTonen();
                // Geen Paypal sandbox geïmplementeerd
                StartMenu();
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
                Console.WriteLine("Wilt u uitloggen? Ja/Nee");
                string hoofdmenu = Console.ReadLine();
                if (hoofdmenu == "ja" || hoofdmenu == "Ja")
                {
                    var Startmenu2 = new Classq();
                    Startmenu2.test();

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Welkom terug bij het menu");
                    StartMenu();
                }


            }
            else
            {
                Console.Clear();
                Console.Write("De ingevoerde index klopt niet, probeer nog een keer");
            }
        }
        public void AdminMenu()
        {
            Console.WriteLine("\nSelecteer uit de volgende opties");
            Console.WriteLine("\n(1) Kortingen tonen.\n(2) Kortingen toevoegen/verwijderen.\n(3) Kortingen aanpassen.\n(4) Tickets tonen.\n(5) Tickets toevoegen/verwijderen.\n(6) Tickets aanpassen.\n(7) Films tonen.\n(8) Film toevoegen/verwijderen.\n(9) Film aanpassen.\n(10) Omzet tonen.\n(11) Uitloggen.");
            Console.Write("\nOptie : ");
            int AdminOptie = Int32.Parse(Console.ReadLine());
            GekozenAdminOptie(AdminOptie);
        }
        public void GekozenAdminOptie(int AdminOptie)
        {
            if (AdminOptie == 1)
            {
                KortingenTonen();
                AdminMenu();
            }
            else if (AdminOptie == 2)
            {
                KortingsMenu();
            }
            else if (AdminOptie == 3)
            {
                KortingenAanpassen();
            }
            else if (AdminOptie == 4)
            {
                TicketsTonen();
                AdminMenu();
            }
            else if (AdminOptie == 5)
            {
                TicketMenu();
            }
            else if (AdminOptie == 6)
            {
                TicketAanpassen();
            }
            else if (AdminOptie == 7)
            {
                FilmsTonen();
                AdminMenu();
            }
            else if (AdminOptie == 8)
            {
                FilmMenu();
            }
            else if (AdminOptie == 9)
            {
                FilmAanpassenAdmin();
            }
            else if (AdminOptie == 10)
            {
                //OmzetTonen();
                //Geen Paypal sandbox geïmplementeerd
                AdminMenu();
            }
            else if (AdminOptie == 11)
            {
                Classq.Menu();
            }
            else
            {
                Console.WriteLine("Niet gekozen uit de beschikbare opties");
                Console.Write("Wilt u het opnieuw proberen?\nTyp 'Ja' of 'Nee' : ");
                string Optie = Console.ReadLine();
                if (Optie == "Ja" || Optie == "ja")
                {
                    AdminMenu();
                }
                else
                {
                    Classq.Menu();
                }
            }
        }
        public void KortingenTonen()
        {
            int Index = 1;
            foreach (Korting i in this._database.Kortingen)
            {
                Console.WriteLine($"({Index}) {i.TypeKorting} : {i.VerrekendeKorting}");
                Index++;
            }
        }
        public void TicketsTonen() //Tickets tonen
        {
            int Index = 1;
            foreach (Ticket i in this._database.Tickets)
            {
                Console.WriteLine($"({Index}) {i.TypeTicket} Ticket : {i.TicketPrijs}");
                Index++;
            }
        }
        public void TicketMenu()
        {
            Console.WriteLine("\nBeschikbare Tickets\n");
            TicketsTonen();
            Console.WriteLine("\nWilt u :\n(1) Een ticket toevoegen.\n(2) Een ticket verwijderen.");
            Console.Write("\nOptie : ");
            int keuze = Convert.ToInt32(Console.ReadLine());
            if (keuze == 1)
            {
                TicketToevoegen();
            }
            else if (keuze == 2)
            {
                TicketVerwijderen();
            }
            else
            {
                Console.WriteLine("Error wilt u het nog eens proberen?");
                Console.Write("\nTyp 'Ja' of 'Nee' : ");
                string keuze2 = Console.ReadLine();
                if (keuze2 == "Ja" || keuze2 == "ja")
                {
                    TicketMenu();
                }
                else
                {
                    Console.WriteLine("U wordt doorgestuurd naar het Administrator menu");
                    AdminMenu();
                }
            }
        }

        public void TicketToevoegen()
        {
            Console.Clear();
            Console.Write("Typ de naam van de ticket in : ");
            string NieuweTicket = Console.ReadLine();
            Console.WriteLine($"Wilt u de ticket ({NieuweTicket}) toevoegen?");
            Console.Write("\nTyp 'Ja' of 'Nee' : ");
            string keuze = Console.ReadLine();
            if (keuze == "Ja" || keuze == "ja")
            {
                TicketPrijsToevoegen(NieuweTicket);
            }
            else
            {
                Console.WriteLine("Wilt u nogmaals een nieuwe korting toekennen?");
                Console.Write("Typ 'Ja' of 'Nee' : ");
                string keuze2 = Console.ReadLine();
                if (keuze2 == "Ja" || keuze2 == "ja")
                {
                    TicketToevoegen();
                }
                else
                {
                    Console.WriteLine("U wordt doorgestuurd naar het Administrator menu.");
                    AdminMenu();
                }
            }
        }

        public void TicketPrijsToevoegen(string NieuweTicket)
        {
            Console.Write("\nTyp de ticketprijs in : ");
            double NieuweTicketPrijs = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine($"Wilt u de ticketprijs ({NieuweTicketPrijs}) toepassen aan de korting ({NieuweTicket})?");
            Console.Write("Typ 'Ja' of 'Nee' : ");
            string keuze2 = Console.ReadLine();
            if (keuze2 == "Ja" || keuze2 == "ja")
            {
                Console.WriteLine($"\nNieuwe Ticket ({NieuweTicket}) aangemaakt.");
                var ToegekendeTicket = new Ticket(NieuweTicketPrijs, NieuweTicket);
                _database.Tickets.Add(ToegekendeTicket);
                UpdateData();
                TicketsTonen();
                AdminMenu();
            }
            else
            {
                Console.WriteLine($"\nNieuwe ticket ({NieuweTicket}) niet aangemaakt.\nWilt u :\n(1) Teruggaan naar ticket toevoegen/verwijderen.\n(2) Teruggaan naar het Administrator menu.");
                Console.Write("\nOptie : ");
                int Optie = Convert.ToInt32(Console.ReadLine());
                if (Optie == 1)
                {
                    TicketMenu();
                }
                else if (Optie == 2)
                {
                    AdminMenu();
                }
                else
                {
                    Console.WriteLine("Error, U wordt doorgestuurd naar het Administrator menu.");
                    AdminMenu();
                }
            }
        }

        public void TicketVerwijderen()
        {
            Console.Write("Typ de index van de ticket die u wilt verwijderen : ");
            int VerwijderTicket = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Wilt u { this._database.Tickets[VerwijderTicket - 1].TypeTicket} verwijderen?");
            Console.Write("Typ 'Ja' of 'Nee' : ");
            string keuze = Console.ReadLine();
            if (keuze == "Ja" || keuze == "ja")
            {
                Console.WriteLine($"\n{ this._database.Tickets[VerwijderTicket - 1].TypeTicket} wordt verwijderd.");
                var GekozenTicket = this._database.Tickets[VerwijderTicket - 1];
                _database.Tickets.Remove(GekozenTicket);
                UpdateData();
                TicketsTonen();
                AdminMenu();
            }
            else
            {
                Console.WriteLine("Wilt u nog een andere ticket verwijderen?");
                Console.Write("Typ 'Ja' of 'Nee' : ");
                string keuze2 = Console.ReadLine();
                if (keuze2 == "Ja" || keuze == "ja")
                {
                    TicketVerwijderen();
                }
                else
                {
                    Console.WriteLine("U wordt doorgestuurd naar het Administrator menu.");
                    AdminMenu();
                }
            }
        }
 
        public void KortingsMenu()
        {
            Console.WriteLine("\nBeschikbare kortingen\n");
            KortingenTonen();
            Console.WriteLine("\nWilt u :\n(1) Een korting toevoegen.\n(2) Een korting verwijderen.");
            Console.Write("\nOptie : ");
            int keuze = Convert.ToInt32(Console.ReadLine());
            if (keuze == 1)
            {
                KortingToevoegen();
            }
            else if (keuze == 2)
            {
                KortingVerwijderen();
            }
            else
            {
                Console.WriteLine("Error wilt u het nog eens proberen?");
                Console.Write("\nTyp 'Ja' of 'Nee' : ");
                string keuze2 = Console.ReadLine();
                if (keuze2 == "Ja" || keuze2 == "ja")
                {
                    KortingsMenu();
                }
                else
                {
                    Console.WriteLine("U wordt doorgestuurd naar het Administrator menu");
                    AdminMenu();
                }
            }
        }

        public void KortingToevoegen()
        {
            Console.Clear();
            Console.Write("Typ de naam van de korting in : ");
            string NieuweKorting = Console.ReadLine();
            Console.WriteLine($"Wilt u de korting ({NieuweKorting}) toevoegen?");
            Console.Write("\nTyp 'Ja' of 'Nee' : ");
            string keuze = Console.ReadLine();
            if (keuze == "Ja" || keuze == "ja")
            {
                KortingsPrijsToevoegen(NieuweKorting);
            }
            else
            {
                Console.WriteLine("Wilt u nogmaals een nieuwe korting toekennen?");
                Console.Write("Typ 'Ja' of 'Nee' : ");
                string keuze2 = Console.ReadLine();
                if (keuze2 == "Ja" || keuze2 == "ja")
                {
                    KortingToevoegen();
                }
                else
                {
                    Console.WriteLine("U wordt doorgestuurd naar het Administrator menu.");
                    AdminMenu();
                }
            }
        }
        public void KortingVerwijderen()
        {
            Console.Write("Typ de index van de korting die u wilt verwijderen : ");
            int VerwijderKorting = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Wilt u { this._database.Kortingen[VerwijderKorting - 1].TypeKorting} verwijderen?");
            Console.Write("Typ 'Ja' of 'Nee' : ");
            string keuze = Console.ReadLine();
            if (keuze == "Ja" || keuze == "ja")
            {
                Console.WriteLine($"\n{ this._database.Kortingen[VerwijderKorting - 1].TypeKorting} wordt verwijderd.");
                var GekozenKorting = this._database.Kortingen[VerwijderKorting - 1];
                _database.Kortingen.Remove(GekozenKorting);
                UpdateData();
                KortingenTonen();
                AdminMenu();
            }
            else
            {
                Console.WriteLine("Wilt u nog een andere korting verwijderen?");
                Console.Write("Typ 'Ja' of 'Nee' : ");
                string keuze2 = Console.ReadLine();
                if (keuze2 == "Ja" || keuze == "ja")
                {
                    KortingVerwijderen();
                }
                else
                {
                    Console.WriteLine("U wordt doorgestuurd naar het Administrator menu.");
                    AdminMenu();
                }
            }
        }
        public void KortingsPrijsToevoegen(string NieuweKorting)
        {
            Console.Write("\nTyp de hoeveelheid korting in : ");
            int NieuweKortingsPrijs = Convert.ToInt32(Console.ReadLine());
            if (NieuweKortingsPrijs > 100)
            {
                Console.WriteLine("100% is het maximum wat gegeven kan worden als korting");
                KortingsPrijsToevoegen(NieuweKorting);
            }
            Console.WriteLine($"Wilt u de verrekende korting ({NieuweKortingsPrijs}) toepassen aan de korting ({NieuweKorting})?");
            Console.Write("Typ 'Ja' of 'Nee' : ");
            string keuze2 = Console.ReadLine();
            if (keuze2 == "Ja" || keuze2 == "ja")
            {
                Console.WriteLine($"\nNieuwe korting ({NieuweKorting}) aangemaakt.");
                var ToegekendeKorting = new Korting(NieuweKortingsPrijs, NieuweKorting);
                _database.Kortingen.Add(ToegekendeKorting);
                UpdateData();
                KortingenTonen();
                AdminMenu();
            }
            else
            {
                Console.WriteLine($"\nNieuwe korting ({NieuweKorting}) niet aangemaakt.\nWilt u :\n(1) Teruggaan naar kortingen toevoegen/verwijderen.\n(2) Teruggaan naar het Administrator menu.");
                Console.Write("\nOptie : ");
                int Optie = Convert.ToInt32(Console.ReadLine());
                if (Optie == 1)
                {
                    KortingsMenu();
                }
                else if (Optie == 2)
                {
                    AdminMenu();
                }
                else
                {
                    Console.WriteLine("Error, U wordt doorgestuurd naar het Administrator menu.");
                    AdminMenu();
                }
            }
        }
        public void KortingenAanpassen() //Kortingen aanpassen
        {
            Console.WriteLine("\nBeschikbare kortingen\n");
            KortingenTonen();
            Console.WriteLine("\nWelke korting wilt u aanpassen?");
            Console.Write("\nTyp het cijfer van de soort korting in : ");
            int kortingkiezen = Convert.ToInt32(Console.ReadLine());
            if (kortingkiezen > this._database.Kortingen.Count)
            {
                Console.Clear();
                Console.WriteLine("Error probeer het opnieuw.");
                KortingenAanpassen();
            }
            var kortingkeuze = this._database.Kortingen[kortingkiezen - 1];
            Console.WriteLine($"Wilt u {kortingkeuze.TypeKorting} aanpassen?");
            Console.Write("Typ 'Ja' of 'Nee' : ");
            string beslissing = Console.ReadLine();
            if (beslissing == "ja" || beslissing == "Ja")
            {
                Console.WriteLine($"\nWat wilt u aanpassen aan {kortingkeuze.TypeKorting}?");
                Console.WriteLine("Wilt u (1) de naam van de korting aanpassen of (2) de hoeveelheid verrekende korting aanpassen");
                Console.Write("\nTyp het cijfer van uw keuze in : ");
                int kortingaanpassing = Convert.ToInt32(Console.ReadLine());
                if (kortingaanpassing == 1)
                {
                    Console.WriteLine($"\nU wilt de kortings naam ({kortingkeuze.TypeKorting}) aanpassen.");
                    Console.Write($"Typ de nieuwe kortings naam in voor ({kortingkeuze.TypeKorting}) : ");
                    string nieuwekortingsnaam = Console.ReadLine();
                    Console.WriteLine($"Wilt u de kortings naam : {kortingkeuze.TypeKorting} veranderen in : {nieuwekortingsnaam}?");
                    Console.Write("Typ 'Ja' of 'Nee' : ");
                    string beslissing2 = Console.ReadLine();
                    if (beslissing2 == "Ja" || beslissing2 == "ja")
                    {
                        Console.WriteLine($"\n{kortingkeuze.TypeKorting} is veranderd naar : {nieuwekortingsnaam}.\n");
                        kortingkeuze.TypeKorting = nieuwekortingsnaam;
                        UpdateData();
                        KortingenTonen();
                        AdminMenu();
                    }
                    else
                    {
                        Console.WriteLine($"\nWilt u een andere korting aanpassen?");
                        Console.Write("Typ 'Ja' of 'Nee' : ");
                        string beslissing3 = Console.ReadLine();
                        if (beslissing3 == "Ja" || beslissing3 == "ja")
                        {
                            KortingenAanpassen();
                        }
                        else
                        {
                            Console.WriteLine("U wordt doorgestuurd naar het Administrator menu.");
                            AdminMenu();
                        }
                    }
                }
                else if (kortingaanpassing == 2)
                {
                    Console.WriteLine($"\nU wilt de verrekende korting aanpassen van ({kortingkeuze.TypeKorting})\nHuidige verrekende korting = {kortingkeuze.VerrekendeKorting}%");
                    Console.Write($"Typ de nieuwe verrekende korting in voor ({kortingkeuze.TypeKorting}) (geen % neerzetten) : ");
                    int nieuwekorting = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"Wilt u de huidige verrekende korting : {kortingkeuze.VerrekendeKorting} veranderen naar : {nieuwekorting}?");
                    Console.Write("Typ 'Ja' of 'Nee' : ");
                    string beslissing4 = Console.ReadLine();
                    if (beslissing4 == "Ja" || beslissing4 == "ja")
                    {
                        Console.WriteLine($"\nDe verrekende korting van ({kortingkeuze.TypeKorting}) was {kortingkeuze.TypeKorting} is veranderd naar {nieuwekorting}.");
                        kortingkeuze.VerrekendeKorting = nieuwekorting;
                        UpdateData();
                        KortingenTonen();
                        AdminMenu();
                    }
                    else
                    {
                        Console.WriteLine("\nWilt u een andere korting aanpassen?");
                        Console.Write("Typ 'Ja' of 'Nee' : ");
                        string beslissing5 = Console.ReadLine();
                        if (beslissing5 == "Ja" || beslissing5 == "ja")
                        {
                            KortingenAanpassen();
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

        public void FilmMenu()
        {
            Console.WriteLine("\nBeschikbare Films\n");
            FilmsTonen();
            Console.WriteLine("\nWilt u :\n(1) Een film toevoegen.\n(2) Een film verwijderen.");
            Console.Write("\nOptie : ");
            int keuze = Convert.ToInt32(Console.ReadLine());
            if (keuze == 1)
            {
                FilmToevoegenAdmin();
            }
            else if (keuze == 2)
            {
                FilmVerwijderenAdmin();
            }
            else
            {
                Console.WriteLine("Error wilt u het nog eens proberen?");
                Console.Write("\nTyp 'Ja' of 'Nee' : ");
                string keuze2 = Console.ReadLine();
                if (keuze2 == "Ja" || keuze2 == "ja")
                {
                    FilmMenu();
                }
                else
                {
                    Console.WriteLine("U wordt doorgestuurd naar het Administrator menu");
                    AdminMenu();
                }
            }
        }
        public void FilmToevoegenAdmin()
        {
            int Index = 1;
            Console.WriteLine("Alle beschikbare films:");
            Console.WriteLine("\n");
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
                Console.Clear();
                Console.WriteLine("\nKies welke genre de film" + filmtoevoegen + " heeft:");
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
                Console.WriteLine("\nTyp de index van het genre: ");
                int gekozen = Convert.ToInt32(Console.ReadLine());
                string genrefilm = genre[gekozen - 1];
                Console.WriteLine("\nTyp de beschrijving van de film " + filmtoevoegen + ":");
                string beschrijvingfilm = Console.ReadLine();
                Console.WriteLine("\nTyp op welke datum " + filmtoevoegen + " draait: voorbeeld 23-03-2020");
                string datumfilm = Console.ReadLine();
                Console.WriteLine("\nTyp om welke tijd de film " + filmtoevoegen + " draait, voorbeeld: 12.00");
                string tijdfilm = Console.ReadLine();
                Console.WriteLine("\nKies welke schermtype bij de film moet, typ de index");
                int Index2 = 1;
                foreach (Ticket i in this._database.Tickets)
                {
                    Console.WriteLine($"({Index2}) {i.TypeTicket}");
                    Index2++;
                }
                int schermtypes = Convert.ToInt32(Console.ReadLine());
                if (schermtypes > this._database.Tickets.Count || schermtypes < 0)
                {
                    Console.Clear();
                    Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                    FilmAanpassenAdmin();

                }
                else if (schermtypes <= this._database.Tickets.Count && schermtypes >= 0)
                {
                    var schermkeuze = this._database.Tickets[schermtypes - 1].TypeTicket;
                    Console.WriteLine("\nTyp de speelduur van de film in minuten, voorbeeld: 183");
                    int speelmin = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("\nDe gegevens van de nieuwe film:");
                    Console.WriteLine("Titel: " + filmtoevoegen + "\nGenre: " + genrefilm + "\nBeschrijving: " + beschrijvingfilm + "\nDatum: " + datumfilm + "\nTijd: " + tijdfilm + "\nSchermtype: " + schermkeuze + "\nSpeelduur: " + speelmin);


                    Console.WriteLine("\nWilt u de film met de bovenstaande gegevens toevoegen? Ja/Nee");
                    string finalfilm = Console.ReadLine();
                    if (finalfilm == "ja" || finalfilm == "Ja")
                    {

                        Console.WriteLine("\nDe film is succesvol toegevoegd");
                        var nieuwfilm = new Film(filmtoevoegen, tijdfilm, datumfilm, genrefilm, beschrijvingfilm, schermkeuze, speelmin);
                        AddFilm(nieuwfilm);
                        UpdateData();
                        int Indexz = 1;
                        Console.WriteLine("\nBeschikbare films: ");
                        foreach (Film i in this._database.Films)
                        {
                            Console.WriteLine(i.Titel);
                            Indexz++;
                        }
                        AdminMenu();

                    }
                    else if (finalfilm == "Nee" || finalfilm != "ja" || finalfilm != "Ja")
                    {
                        Console.WriteLine("Wilt u een andere film toevoegen? Ja/ Nee");
                        string anderefilm = Console.ReadLine();
                        if (anderefilm == "ja" || anderefilm == "Ja")
                        {
                            FilmToevoegenAdmin();
                        }
                        else
                        {
                            AdminMenu();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Welkom terug bij het menu");
                        AdminMenu();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Welkom terug bij het menu");
                    AdminMenu();

                }

            }
            else if (antwoord == "nee" || antwoord != "ja")
            {

                AdminMenu();
            }

        }
        public void FilmVerwijderenAdmin()
        {
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
                    FilmVerwijderenAdmin();
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
                        AdminMenu();
                    }
                    else if (removefilm == "nee" || removefilm != "Ja")
                    {
                        Console.WriteLine("\nWilt u een andere film verwijderen of terug naar startmenu? V/S ");
                        string startantwoord = Console.ReadLine();
                        if (startantwoord == "V" || startantwoord == "v")
                        {
                            Console.WriteLine("Welkom terug bij de optie: film verwijderen.");
                            FilmVerwijderenAdmin();
                        }
                        else if (startantwoord == "S" || startantwoord == "s")
                        {
                            Console.Clear();
                            Console.WriteLine("Welkom terug bij het menu.");
                            AdminMenu();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\nWelkom terug bij het menu");
                            AdminMenu();
                        }

                    }

                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nWelkom terug bij het menu");
                        AdminMenu();
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nWelkom terug bij het menu");
                AdminMenu();
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
            int indexfilm = Convert.ToInt32(Console.ReadLine()) -1;
            var filmaantal = this._database.Films.Count;


            if (indexfilm >= filmaantal || indexfilm < 0)
            {

                Console.Write("De ingevoerde index klopt niet, probeer nog een keer");
                VolledigFilmsTonen();

            }
         
            else if((indexfilm < filmaantal) && (indexfilm >= 0))
            {
                Console.Clear();
                var filmtonen = this._database.Films[indexfilm];
                Console.WriteLine("Titel: " + filmtonen.Titel + "\nGenre: " + filmtonen.Genre + "\nBeschrijving: " + filmtonen.Beschrijving + "\nDatum: " + filmtonen.Datum + "\nTijd: " + filmtonen.Tijd + "\nSchermtype: " + filmtonen.Schermtype + "\nSpeelduur: " + filmtonen.SpeelDuur);
                Console.WriteLine("\nWilt u terug naar films tonen of naar het menu? F/M");
                string keuze = Console.ReadLine();
                if(keuze == "F" || keuze == "f")
                {
                    Console.Clear();
                    Console.WriteLine("Welkom terug bij de optie: films tonen");
                    VolledigFilmsTonen();
                }
                else if(keuze == "M" || keuze == "m")
                {
                    Console.Clear();
                    Console.WriteLine("Welkom terug bij het menu");
                    StartMenu();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("We sturen u terug naar het menu");
                    StartMenu();
                }
            }
            else
            {
                StartMenu();
            }
        }
        public void TicketAanpassen() //Tickets aanpassen
        {
            Console.WriteLine("\nBeschikbare tickets\n");
            TicketsTonen();
            Console.WriteLine("\nWelke ticket wilt u aanpassen?");
            Console.Write("\nTyp het cijfer van de ticket in : ");
            int ticketkiezen = Convert.ToInt32(Console.ReadLine());
            if (ticketkiezen > this._database.Tickets.Count)
            {
                Console.Clear();
                Console.WriteLine("Error probeer het opnieuw.");
                TicketAanpassen();
            }
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
                    Console.WriteLine($"\nU wilt de ticketprijs aanpassen van ({ticketkeuze.TypeTicket})\nHuidige ticketprijs voor deze ticket = ({ticketkeuze.TicketPrijs})");
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

        public void FilmAanpassenAdmin() //Films aanpassen 
        {

            Console.WriteLine("\nWelkom bij de optie: film aanpassen.");
            FilmsTonen();
            Console.WriteLine("\nWelke film wilt u aanpassen? Typ het cijfer van de film");
            int filmaanpassen = Convert.ToInt32(Console.ReadLine());
            if (filmaanpassen > this._database.Films.Count || filmaanpassen < 0)
            {
                Console.Clear();
                Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                FilmAanpassenAdmin();
            }
            else if (filmaanpassen <= this._database.Films.Count && filmaanpassen >= 0)
            {
                var Filmaanpas = this._database.Films[filmaanpassen - 1];
                Console.Clear();
                Console.WriteLine($"Wilt u {Filmaanpas.Titel} aanpassen? Ja/Nee");
                string antwoord2 = Console.ReadLine();
                if (antwoord2 == "ja" || antwoord2 == "Ja")
                {
                    Console.Clear();
                    Console.WriteLine("Titel: " + Filmaanpas.Titel + "\nGenre: " + Filmaanpas.Genre + "\nBeschrijving: " + Filmaanpas.Beschrijving + "\nDatum: " + Filmaanpas.Datum + "\nTijd: " + Filmaanpas.Tijd + "\nSchermtype: " + Filmaanpas.Schermtype + "\nSpeelduur: " + Filmaanpas.SpeelDuur);
                    Console.WriteLine($"\nWat wilt u aanpassen:\n(1)Titel\n(2)Genre\n(3)Beschrijving\n(4)Datum\n(5)Tijd\n(6)Schermtype\n(7)Speelduur\n(8)Alles aanpassen");
                    int intantwoord = Convert.ToInt32(Console.ReadLine());
                    if (intantwoord == 1) //Titel aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(1) U heeft gekozen voor de optie titel aanpassen.");
                        Console.WriteLine($"Typ de nieuwe titel van de film:");
                        string nieuwtitels = Console.ReadLine();
                        Console.WriteLine($"\nWilt u de titel: {Filmaanpas.Titel} in {nieuwtitels} veranderen? Ja/Nee");
                        string antwoord5 = Console.ReadLine();
                        if (antwoord5 == "Ja" || antwoord5 == "ja")
                        {
                            Console.WriteLine($"\nDe titel {Filmaanpas.Titel} is veranderd in de nieuwe titel: {nieuwtitels}.");
                            Filmaanpas.Titel = nieuwtitels;
                            UpdateData();
                            FilmsTonen();
                            AdminMenu();
                        }
                        else
                        {
                            Console.WriteLine($"\nWilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de optie: film aanpassen");
                                FilmAanpassenAdmin();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("We sturen u terug naar het menu.");
                                AdminMenu();
                            }

                        }
                    }
                    else if (intantwoord == 2)//Genre aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(2) U heeft gekozen voor de optie genre aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft het genre: {Filmaanpas.Genre}");
                        Console.WriteLine("\nKies welke genre de film" + Filmaanpas.Titel + " heeft:");
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
                        Console.WriteLine($"\nTyp de index van het nieuwe genre van de Film");
                        int nieuwgenre1 = Convert.ToInt32(Console.ReadLine());
                        if (nieuwgenre1 > genre.Count || nieuwgenre1 < 0)
                        {
                            Console.Clear();
                            Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                            FilmAanpassenAdmin();
                        }
                        else if (nieuwgenre1 <= genre.Count && nieuwgenre1 >= 0)
                        {
                            Console.WriteLine($"\nWilt u {Filmaanpas.Titel} met het genre {Filmaanpas.Genre} veranderen in {genre[nieuwgenre1 - 1]}? Ja/Nee");
                            string antwoord1 = Console.ReadLine();
                            if (antwoord1 == "Ja" || antwoord1 == "ja")
                            {
                                Console.WriteLine($"\nDe film {Filmaanpas.Titel} heeft het genre {genre[nieuwgenre1 - 1]}");
                                Filmaanpas.Genre = genre[nieuwgenre1 - 1];
                                UpdateData();
                                AdminMenu();
                            }
                            else
                            {
                                Console.WriteLine($"\nWilt u een andere film of {Filmaanpas.Titel} aanpassen? Ja/Nee");
                                string antwoord3 = Console.ReadLine();
                                if (antwoord3 == "Ja" || antwoord3 == "ja")
                                {
                                    Console.Clear();
                                    Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                    FilmAanpassenAdmin();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("We sturen u terug naar het menu.");
                                    AdminMenu();
                                }

                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("We sturen u terug naar het menu");
                            AdminMenu();
                        }

                    }
                    else if (intantwoord == 3)//beschrijving aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(3) U heeft gekozen voor de optie beschrijving aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft als beschrijving:\n {Filmaanpas.Beschrijving}");
                        Console.WriteLine($"Typ de nieuwe beschrijving van de film:  {Filmaanpas.Beschrijving}");
                        string nieuwbeschrijving = Console.ReadLine();
                        Console.WriteLine($"Wilt u de oude beschrijving:\n {Filmaanpas.Beschrijving} \nveranderen naar de nieuwe beschrijving:\n {nieuwbeschrijving}\n ? Ja/Nee");
                        string antwoord3 = Console.ReadLine();
                        if (antwoord3 == "Ja" || antwoord3 == "ja")
                        {
                            Console.WriteLine($"\nDe film {Filmaanpas.Titel} heeft als nieuwe beschrijving:\nBeschrijving: {nieuwbeschrijving}");
                            Filmaanpas.Beschrijving = nieuwbeschrijving;
                            UpdateData();
                            AdminMenu();
                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord6 = Console.ReadLine();
                            if (antwoord6 == "Ja" || antwoord6 == "ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                FilmAanpassenAdmin();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij het menu.");
                                AdminMenu();
                            }

                        }
                    }
                    else if (intantwoord == 4)//datum aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(4) U wilt de datum van {Filmaanpas.Titel} aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} draait op {Filmaanpas.Datum}");
                        Console.WriteLine($"Typ de nieuwe datum van de film:  {Filmaanpas.Titel}. voorbeeld: 22-04-2020");
                        string nieuwdatum2 = Console.ReadLine();
                        Console.WriteLine($"Wilt u {Filmaanpas.Titel} op {Filmaanpas.Datum} veranderen in {Filmaanpas.Titel} om {nieuwdatum2}? Ja/Nee");
                        string antwoorden = Console.ReadLine();
                        if (antwoorden == "Ja" || antwoorden == "ja")
                        {
                            Console.WriteLine($"\nDe film {Filmaanpas.Titel} draait op {nieuwdatum2}");
                            Filmaanpas.Datum = nieuwdatum2;
                            UpdateData();
                            AdminMenu();
                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                FilmAanpassenAdmin();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("We sturen u terug naar het Startmenu.");
                                AdminMenu();
                            }

                        }
                    }

                    else if (intantwoord == 5)//tijd aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(5) U heeft gekozen voor de optie tijd aanpassen.");
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
                            AdminMenu();
                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                FilmAanpassenAdmin();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij het menu.");
                                AdminMenu();
                            }

                        }
                    }
                    else if (intantwoord == 6)//schermtype aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(6) U heeft gekozen voor de optie schermtype aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft het schermtype: {Filmaanpas.Schermtype}.");
                        Console.WriteLine("\nKies welke schermtype " + Filmaanpas.Titel + " heeft: ");
                        int Index2 = 1;
                        foreach (Ticket i in this._database.Tickets)
                        {
                            Console.WriteLine($"({Index2}) {i.TypeTicket}");
                            Index2++;
                        }
                        Console.WriteLine("\nKies de index van het nieuwe schermtype:");
                        int schermtypes = Convert.ToInt32(Console.ReadLine());
                        if (schermtypes > this._database.Tickets.Count || schermtypes < 0)
                        {
                            Console.Clear();
                            Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                            FilmAanpassenAdmin();

                        }
                        else if (schermtypes <= this._database.Tickets.Count && schermtypes >= 0)
                        {
                            var schermkeuze = this._database.Tickets[schermtypes - 1].TypeTicket;
                            Console.WriteLine($"Wilt u {Filmaanpas.Titel} met het schermtype: {Filmaanpas.Schermtype} veranderen in {schermkeuze}? Ja/Nee");
                            string scherm = Console.ReadLine();
                            if (scherm == "Ja" || scherm == "ja")
                            {
                                Console.WriteLine($"\nDe film {Filmaanpas.Titel} heeft het schermtype: {schermkeuze}");
                                Filmaanpas.Schermtype = schermkeuze;
                                UpdateData();
                                AdminMenu();
                            }
                            else
                            {
                                Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                                string antwoord3 = Console.ReadLine();
                                if (antwoord3 == "Ja" || antwoord2 == "ja")
                                {
                                    Console.Clear();
                                    Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                    FilmAanpassenAdmin();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Welkom terug bij het menu.");
                                    AdminMenu();
                                }

                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                            FilmAanpassenAdmin();

                        }
                    }
                    else if (intantwoord == 7)//speelduur aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(7) U heeft gekozen voor de optie speelduur aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft een speelduur van: {Filmaanpas.SpeelDuur} minuten.");
                        Console.WriteLine("Typ de nieuwe speelduur van " + Filmaanpas.Titel + " in minuten. Voorbeeld: 172");
                        int speelduur = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine($"Wilt u {Filmaanpas.Titel} met een speelduur van {Filmaanpas.SpeelDuur} minuten veranderen in {speelduur} minuten?\nJa/Nee");
                        string scherm = Console.ReadLine();
                        if (scherm == "Ja" || scherm == "ja")
                        {
                            Console.WriteLine($"De film {Filmaanpas.Titel} heeft een speelduur van: {speelduur} minuten");
                            Filmaanpas.SpeelDuur = speelduur;
                            UpdateData();
                            AdminMenu();
                        }
                        else
                        {
                            Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                FilmAanpassenAdmin();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij het menu.");
                                AdminMenu();
                            }

                        }

                    }
                    else if (intantwoord == 8)//alles aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(8) U heeft gekozen voor de optie alles aanpassen.");
                        Console.WriteLine("\nTyp de nieuwe Titel van de film: " + Filmaanpas.Titel);
                        string filmtoevoegen = Console.ReadLine();
                        Console.WriteLine("\nKies welke genre de film" + filmtoevoegen + " heeft:");
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
                        if (gekozen > genre.Count || gekozen < 0)
                        {
                            Console.Clear();
                            Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                            FilmAanpassenAdmin();
                        }
                        else if (gekozen <= genre.Count && gekozen >= 0)
                        {

                            string genrefilm = genre[gekozen - 1];
                            Console.WriteLine("\nTyp de nieuwe beschrijving van de film " + filmtoevoegen + " heeft:");
                            string beschrijvingfilm = Console.ReadLine();
                            Console.WriteLine("\nTyp op welke nieuwe datum " + filmtoevoegen + " draait: voorbeeld 23-03-2020");
                            string datumfilm = Console.ReadLine();
                            Console.WriteLine("\nTyp om welke tijd de film " + filmtoevoegen + " draait, voorbeeld: 12.00");
                            string tijdfilm = Console.ReadLine();
                            Console.WriteLine("\nKies welke schermtype bij de film moet, typ de index");
                            int Index2 = 1;
                            foreach (Ticket i in this._database.Tickets)
                            {
                                Console.WriteLine($"({Index2}) {i.TypeTicket}");
                                Index2++;
                            }
                            int schermtypes = Convert.ToInt32(Console.ReadLine());

                            if (schermtypes > this._database.Tickets.Count || schermtypes < 0)
                            {
                                Console.Clear();
                                Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                                FilmAanpassenAdmin();

                            }
                            else if (schermtypes <= this._database.Tickets.Count && schermtypes >= 0)
                            {

                                var schermkeuze = this._database.Tickets[schermtypes - 1].TypeTicket;
                                Console.WriteLine("\nTyp de speelduur van de film in minuten, voorbeeld: 183");
                                int speelmin = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                Console.WriteLine("\nDe gegevens van de nieuwe film:");
                                Console.WriteLine("Titel: " + filmtoevoegen + "\nGenre: " + genrefilm + "\nBeschrijving: " + beschrijvingfilm + "\nDatum: " + datumfilm + "\nTijd: " + tijdfilm + "\nSchermtype: " + schermkeuze + "\nSpeelduur: " + speelmin);
                                Console.WriteLine("\nWilt u de bovenstaande gegevens toevoegen aan " + filmtoevoegen + "? Ja/Nee");
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
                                    Console.WriteLine("\nDe nieuwe gegevens zijn toegevoegd");
                                    UpdateData();
                                    FilmsTonen();
                                    AdminMenu();

                                }
                                else
                                {
                                    Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                                    string antwoord3 = Console.ReadLine();
                                    if (antwoord3 == "Ja" || antwoord3 == "ja")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                        FilmAanpassenAdmin();
                                    }
                                    if (antwoord3 == "nee" || antwoord3 == "Nee")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Welkom terug bij het menu");
                                        AdminMenu();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Welkom terug bij het menu.");
                                        AdminMenu();
                                    }

                                }


                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                                FilmAanpassenAdmin();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wilt u een andere film aanpassen? Ja/Nee");
                            string vraag = Console.ReadLine();
                            if (vraag == "ja" || vraag == "Ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de functie: film aanpassen");
                                FilmAanpassenAdmin();

                            }
                            else
                            {
                                Console.WriteLine("Welkom terug bij het menu");
                                AdminMenu();
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Probeer het nog een keer");
                        FilmAanpassenAdmin();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Welkom terug bij het menu");
                    AdminMenu();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                FilmAanpassenAdmin();
            }
        }
        public void FilmAanpassen() //Films aanpassen 
        {

            Console.WriteLine("\nWelkom bij de optie: film aanpassen.");
            FilmsTonen();
            Console.WriteLine("\nWelke film wilt u aanpassen? Typ het cijfer van de film");
            int filmaanpassen = Convert.ToInt32(Console.ReadLine());
            if (filmaanpassen > this._database.Films.Count || filmaanpassen < 0)
            {
                Console.Clear();
                Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                FilmAanpassen();
            }
            else if (filmaanpassen <= this._database.Films.Count && filmaanpassen >= 0)
            {
                var Filmaanpas = this._database.Films[filmaanpassen - 1];
                Console.Clear();
                Console.WriteLine($"Wilt u {Filmaanpas.Titel} aanpassen? Ja/Nee");
                string antwoord2 = Console.ReadLine();
                if (antwoord2 == "ja" || antwoord2 == "Ja")
                {
                    Console.Clear();
                    Console.WriteLine("Titel: " + Filmaanpas.Titel + "\nGenre: " + Filmaanpas.Genre + "\nBeschrijving: " + Filmaanpas.Beschrijving + "\nDatum: " + Filmaanpas.Datum + "\nTijd: " + Filmaanpas.Tijd + "\nSchermtype: " + Filmaanpas.Schermtype + "\nSpeelduur: " + Filmaanpas.SpeelDuur);
                    Console.WriteLine($"\nWat wilt u aanpassen:\n(1)Titel\n(2)Genre\n(3)Beschrijving\n(4)Datum\n(5)Tijd\n(6)Schermtype\n(7)Speelduur\n(8)Alles aanpassen");
                    int intantwoord = Convert.ToInt32(Console.ReadLine());
                    if (intantwoord == 1) //Titel aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(1) U heeft gekozen voor de optie titel aanpassen.");
                        Console.WriteLine($"Typ de nieuwe titel van de film:");
                        string nieuwtitels = Console.ReadLine();
                        Console.WriteLine($"\nWilt u de titel: {Filmaanpas.Titel} in {nieuwtitels} veranderen? Ja/Nee");
                        string antwoord5 = Console.ReadLine();
                        if (antwoord5 == "Ja" || antwoord5 == "ja")
                        {
                            Console.WriteLine($"\nDe titel {Filmaanpas.Titel} is veranderd in de nieuwe titel: {nieuwtitels}.");
                            Filmaanpas.Titel = nieuwtitels;
                            UpdateData();
                            FilmsTonen();
                            StartMenu();
                        }
                        else
                        {
                            Console.WriteLine($"\nWilt u een andere film aanpassen? Ja/Nee");
                            string antwoord3 = Console.ReadLine();
                            if (antwoord3 == "Ja" || antwoord2 == "ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de optie: film aanpassen");
                                FilmAanpassen();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("We sturen u terug naar het menu.");
                                StartMenu();
                            }

                        }
                    }
                    else if (intantwoord == 2)//Genre aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(2) U heeft gekozen voor de optie genre aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft het genre: {Filmaanpas.Genre}");
                        Console.WriteLine("\nKies welke genre de film" + Filmaanpas.Titel + " heeft:");
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
                        Console.WriteLine($"\nTyp de index van het nieuwe genre van de Film");
                        int nieuwgenre1 = Convert.ToInt32(Console.ReadLine());
                        if (nieuwgenre1 > genre.Count || nieuwgenre1 < 0)
                        {
                            Console.Clear();
                            Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                            FilmAanpassen();
                        }
                        else if (nieuwgenre1 <= genre.Count && nieuwgenre1 >= 0)
                        {
                            Console.WriteLine($"\nWilt u {Filmaanpas.Titel} met het genre {Filmaanpas.Genre} veranderen in {genre[nieuwgenre1 - 1]}? Ja/Nee");
                            string antwoord1 = Console.ReadLine();
                            if (antwoord1 == "Ja" || antwoord1 == "ja")
                            {
                                Console.WriteLine($"\nDe film {Filmaanpas.Titel} heeft het genre {genre[nieuwgenre1 - 1]}");
                                Filmaanpas.Genre = genre[nieuwgenre1 - 1];
                                UpdateData();
                                StartMenu();
                            }
                            else
                            {
                                Console.WriteLine($"\nWilt u een andere film of {Filmaanpas.Titel} aanpassen? Ja/Nee");
                                string antwoord3 = Console.ReadLine();
                                if (antwoord3 == "Ja" || antwoord3 == "ja")
                                {
                                    Console.Clear();
                                    Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                    FilmAanpassen();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("We sturen u terug naar het menu.");
                                    StartMenu();
                                }

                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("We sturen u terug naar het menu");
                            StartMenu();
                        }

                    }
                    else if (intantwoord == 3)//beschrijving aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(3) U heeft gekozen voor de optie beschrijving aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft als beschrijving:\n {Filmaanpas.Beschrijving}");
                        Console.WriteLine($"Typ de nieuwe beschrijving van de film:  {Filmaanpas.Beschrijving}");
                        string nieuwbeschrijving = Console.ReadLine();
                        Console.WriteLine($"Wilt u de oude beschrijving:\n {Filmaanpas.Beschrijving} \nveranderen naar de nieuwe beschrijving:\n {nieuwbeschrijving}\n ? Ja/Nee");
                        string antwoord3 = Console.ReadLine();
                        if (antwoord3 == "Ja" || antwoord3 == "ja")
                        {
                            Console.WriteLine($"\nDe film {Filmaanpas.Titel} heeft als nieuwe beschrijving:\nBeschrijving: {nieuwbeschrijving}");
                            Filmaanpas.Beschrijving = nieuwbeschrijving;
                            UpdateData();
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
                    else if (intantwoord == 4)//datum aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(4) U wilt de datum van {Filmaanpas.Titel} aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} draait op {Filmaanpas.Datum}");
                        Console.WriteLine($"Typ de nieuwe datum van de film:  {Filmaanpas.Titel}. voorbeeld: 22-04-2020");
                        string nieuwdatum2 = Console.ReadLine();
                        Console.WriteLine($"Wilt u {Filmaanpas.Titel} op {Filmaanpas.Datum} veranderen in {Filmaanpas.Titel} om {nieuwdatum2}? Ja/Nee");
                        string antwoorden = Console.ReadLine();
                        if (antwoorden == "Ja" || antwoorden == "ja")
                        {
                            Console.WriteLine($"\nDe film {Filmaanpas.Titel} draait op {nieuwdatum2}");
                            Filmaanpas.Datum = nieuwdatum2;
                            UpdateData();
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
                                Console.Clear();
                                Console.WriteLine("We sturen u terug naar het Startmenu.");
                                StartMenu();
                            }

                        }
                    }

                    else if (intantwoord == 5)//tijd aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(5) U heeft gekozen voor de optie tijd aanpassen.");
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
                    else if (intantwoord == 6)//schermtype aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(6) U heeft gekozen voor de optie schermtype aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft het schermtype: {Filmaanpas.Schermtype}.");
                        Console.WriteLine("\nKies welke schermtype " + Filmaanpas.Titel + " heeft: ");
                        int Index2 = 1;
                        foreach (Ticket i in this._database.Tickets)
                        {
                            Console.WriteLine($"({Index2}) {i.TypeTicket}");
                            Index2++;
                        }
                        Console.WriteLine("\nKies de index van het nieuwe schermtype:");
                        int schermtypes = Convert.ToInt32(Console.ReadLine());
                        if (schermtypes > this._database.Tickets.Count || schermtypes < 0)
                        {
                            Console.Clear();
                            Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                            FilmAanpassen();

                        }
                        else if (schermtypes <= this._database.Tickets.Count && schermtypes >= 0)
                        {
                            var schermkeuze = this._database.Tickets[schermtypes - 1].TypeTicket;
                            Console.WriteLine($"Wilt u {Filmaanpas.Titel} met het schermtype: {Filmaanpas.Schermtype} veranderen in {schermkeuze}? Ja/Nee");
                            string scherm = Console.ReadLine();
                            if (scherm == "Ja" || scherm == "ja")
                            {
                                Console.WriteLine($"\nDe film {Filmaanpas.Titel} heeft het schermtype: {schermkeuze}");
                                Filmaanpas.Schermtype = schermkeuze;
                                UpdateData();
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
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                            FilmAanpassen();

                        }
                    }
                    else if (intantwoord == 7)//speelduur aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(7) U heeft gekozen voor de optie speelduur aanpassen.");
                        Console.WriteLine($"\nDe film: {Filmaanpas.Titel} heeft een speelduur van: {Filmaanpas.SpeelDuur} minuten.");
                        Console.WriteLine("Typ de nieuwe speelduur van " + Filmaanpas.Titel + " in minuten. Voorbeeld: 172");
                        int speelduur = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine($"Wilt u {Filmaanpas.Titel} met een speelduur van {Filmaanpas.SpeelDuur} minuten veranderen in {speelduur} minuten?\nJa/Nee");
                        string scherm = Console.ReadLine();
                        if (scherm == "Ja" || scherm == "ja")
                        {
                            Console.WriteLine($"De film {Filmaanpas.Titel} heeft een speelduur van: {speelduur} minuten");
                            Filmaanpas.SpeelDuur = speelduur;
                            UpdateData();
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
                    else if (intantwoord == 8)//alles aanpassen
                    {
                        Console.Clear();
                        Console.WriteLine($"(8) U heeft gekozen voor de optie alles aanpassen.");
                        Console.WriteLine("\nTyp de nieuwe Titel van de film: " + Filmaanpas.Titel);
                        string filmtoevoegen = Console.ReadLine();
                        Console.WriteLine("\nKies welke genre de film" + filmtoevoegen + " heeft:");
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
                        if (gekozen > genre.Count || gekozen < 0)
                        {
                            Console.Clear();
                            Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                            FilmAanpassen();
                        }
                        else if (gekozen <= genre.Count && gekozen >= 0)
                        {

                            string genrefilm = genre[gekozen - 1];
                            Console.WriteLine("\nTyp de nieuwe beschrijving van de film " + filmtoevoegen + " heeft:");
                            string beschrijvingfilm = Console.ReadLine();
                            Console.WriteLine("\nTyp op welke nieuwe datum " + filmtoevoegen + " draait: voorbeeld 23-03-2020");
                            string datumfilm = Console.ReadLine();
                            Console.WriteLine("\nTyp om welke tijd de film " + filmtoevoegen + " draait, voorbeeld: 12.00");
                            string tijdfilm = Console.ReadLine();
                            Console.WriteLine("\nKies welke schermtype bij de film moet, typ de index");
                            int Index2 = 1;
                            foreach (Ticket i in this._database.Tickets)
                            {
                                Console.WriteLine($"({Index2}) {i.TypeTicket}");
                                Index2++;
                            }
                            int schermtypes = Convert.ToInt32(Console.ReadLine());

                            if (schermtypes > this._database.Tickets.Count || schermtypes < 0)
                            {
                                Console.Clear();
                                Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                                FilmAanpassen();

                            }
                            else if (schermtypes <= this._database.Tickets.Count && schermtypes >= 0)
                            {

                                var schermkeuze = this._database.Tickets[schermtypes - 1].TypeTicket;
                                Console.WriteLine("\nTyp de speelduur van de film in minuten, voorbeeld: 183");
                                int speelmin = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                Console.WriteLine("\nDe gegevens van de nieuwe film:");
                                Console.WriteLine("Titel: " + filmtoevoegen + "\nGenre: " + genrefilm + "\nBeschrijving: " + beschrijvingfilm + "\nDatum: " + datumfilm + "\nTijd: " + tijdfilm + "\nSchermtype: " + schermkeuze + "\nSpeelduur: " + speelmin);
                                Console.WriteLine("\nWilt u de bovenstaande gegevens toevoegen aan " + filmtoevoegen + "? Ja/Nee");
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
                                    Console.WriteLine("\nDe nieuwe gegevens zijn toegevoegd");
                                    UpdateData();
                                    FilmsTonen();
                                    StartMenu();

                                }
                                else
                                {
                                    Console.WriteLine($"Wilt u een andere film aanpassen? Ja/Nee");
                                    string antwoord3 = Console.ReadLine();
                                    if (antwoord3 == "Ja" || antwoord3 == "ja")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Welkom terug bij de optie: film aanpassen.");
                                        FilmAanpassen();
                                    }
                                    if(antwoord3 == "nee" || antwoord3 == "Nee")
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Welkom terug bij het menu");
                                        StartMenu();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Welkom terug bij het menu.");
                                        StartMenu();
                                    }

                                }


                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                                FilmAanpassen();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wilt u een andere film aanpassen? Ja/Nee");
                            string vraag = Console.ReadLine();
                            if (vraag == "ja" || vraag == "Ja")
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug bij de functie: film aanpassen");
                                FilmAanpassen();

                            }
                            else
                            {
                                Console.WriteLine("Welkom terug bij het menu");
                                StartMenu();
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Probeer het nog een keer");
                        FilmAanpassen();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Welkom terug bij het menu");
                    StartMenu();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                FilmAanpassen();
            }
        }

            public void FilmToevoegen()
            {
                int Index = 1;
                Console.WriteLine("Alle beschikbare films:");
                Console.WriteLine("\n");
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
                    Console.Clear();
                    Console.WriteLine("\nKies welke genre de film" + filmtoevoegen + " heeft:");
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
                    Console.WriteLine("\nTyp de index van het genre: ");
                    int gekozen = Convert.ToInt32(Console.ReadLine());
                    string genrefilm = genre[gekozen - 1];
                    Console.WriteLine("\nTyp de beschrijving van de film " + filmtoevoegen + ":");
                    string beschrijvingfilm = Console.ReadLine();
                    Console.WriteLine("\nTyp op welke datum " + filmtoevoegen + " draait: voorbeeld 23-03-2020");
                    string datumfilm = Console.ReadLine();
                    Console.WriteLine("\nTyp om welke tijd de film " + filmtoevoegen + " draait, voorbeeld: 12.00");
                    string tijdfilm = Console.ReadLine();
                    Console.WriteLine("\nKies welke schermtype bij de film moet, typ de index");
                    int Index2 = 1;
                    foreach (Ticket i in this._database.Tickets)
                    {
                        Console.WriteLine($"({Index2}) {i.TypeTicket}");
                        Index2++;
                    }
                    int schermtypes = Convert.ToInt32(Console.ReadLine());
                    if (schermtypes > this._database.Tickets.Count || schermtypes < 0)
                    {
                        Console.Clear();
                        Console.WriteLine("De index die u heeft ingetypt is niet beschikbaar.\nProbeer het nog een keer.");
                        FilmAanpassen();

                    }
                    else if (schermtypes <= this._database.Tickets.Count && schermtypes >= 0) { 
                        var schermkeuze = this._database.Tickets[schermtypes - 1].TypeTicket;
                        Console.WriteLine("\nTyp de speelduur van de film in minuten, voorbeeld: 183");
                        int speelmin = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("\nDe gegevens van de nieuwe film:");
                        Console.WriteLine("Titel: " + filmtoevoegen + "\nGenre: " + genrefilm + "\nBeschrijving: " + beschrijvingfilm + "\nDatum: " + datumfilm + "\nTijd: " + tijdfilm + "\nSchermtype: " + schermkeuze + "\nSpeelduur: " + speelmin);


                        Console.WriteLine("\nWilt u de film met de bovenstaande gegevens toevoegen? Ja/Nee");
                        string finalfilm = Console.ReadLine();
                        if (finalfilm == "ja" || finalfilm == "Ja")
                        {

                            Console.WriteLine("\nDe film is succesvol toegevoegd");
                            var nieuwfilm = new Film(filmtoevoegen, tijdfilm, datumfilm, genrefilm, beschrijvingfilm, schermkeuze, speelmin);
                            AddFilm(nieuwfilm);
                            UpdateData();
                            int Indexz = 1;
                            Console.WriteLine("\nBeschikbare films: ");
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
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Welkom terug bij het menu");
                        StartMenu();
                    }
                    }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Welkom terug bij het menu");
                    StartMenu();

                }

                }
                else if (antwoord == "nee" || antwoord != "ja")
                {

                    StartMenu();
                }

            }
            public void FilmVerwijderen()
            {
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
                                Console.Clear();
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
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nWelkom terug bij het menu");
                    StartMenu();
                }

            }
        }

    }



