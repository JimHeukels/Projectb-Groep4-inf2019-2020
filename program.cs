using JimFilmsTake2.Db;
using System.IO;
using JimFilmsTake2.Model;
using System;
using registratie88888888;
using System.Collections;
using System.Text.Json;


namespace JimFilmsTake2
{
	class Program
	{
		static void Main(string[] args)
		{
			var Startmenu = new Classq();
			Startmenu.test();


			// maken een bioscoop met 1 scherm
			// maak de repository aan, de koppeling tussen onze code en de database
			//var repo = new BioscoopRepository();
			//repo.BioscoopKiezen();
			/*
            Console.WriteLine("Wilt u een bioscoop toevoegen? Voor de naam en de locatie in");
            var nieuweBiosNaam = Console.ReadLine();
            var bioscoop = new Bioscoop(nieuweBiosNaam, "Azaleastraat 15");
            // wat mock data om te testen
            var vertoning = new Vertoning(2, 5, DateTime.Now.AddHours(2));
            var scherm = new Scherm(1);
            scherm.Vertoningen[vertoning.AanvangsTijd.ToString()] = vertoning;



            // bioscoop.Schermen.Add(scherm);

            // Voeg de bioscoop met al zijn data toe aan de repository
            repo.AddBioscoop(bioscoop);
            repo.UpdateData();
            // Voeg nog een scherm toe aan bioscoop
            var scherm2 = new Scherm(2);
            // repo.AddScherm(bioscoop, scherm2);
            repo.UpdateData();

            //var repo = new BioscoopRepository();


            repo.ToonBioscopen();

            Console.WriteLine("Welkom bij het Admin paneel van Nioscoop!");
            Console.WriteLine("Wat wilt u doen?");
            Console.WriteLine("(1)niks \n(2)bioscoop toevoegen \n(3)bioscoop verwijderen");
            var interfaceAntwoord = Console.ReadLine();

            if (interfaceAntwoord == "3")
            {
                repo.VerwijderBioscoop();
            }
            */


			//repo.BioscoopKiezen();



			//var datumTest = DateTime.Now.AddHours(2);
			//var TestDatum = DateTime.Now.AddHours(6);



			//Console.WriteLine(TestDatum.ToString("yyyy MM dd"));

			//var bioscopen = repo.GetBioscopen();
			//var vertoning = bioscopen[0].Schermen[0].Vertoningen["22-4-2020 15:43:01"];

			//foreach (var _vertoning in bioscopen[0].Schermen[0].Vertoningen)
			//{
			//var value = _vertoning.Value;
			//Console.WriteLine(value.AanvangsTijd);
			//}

			//if(vertoning.AanvangsTijd < DateTime.Now.AddDays(14))
			//{
			//    Console.WriteLine("vertoning is premium A.F.");
			//}

			//Console.WriteLine("[O][O][X][X][O][X][X]\n[X][X][O][X][O][O][O]\n[X][O][O][X][O][O][O]\n");
			//repo.ToonBioscopen();

			//Console.Clear();
			//Console.WriteLine("hier komt de big shizzle");
			//repo.filmNaarBeschikbaar();
			
			
		}
		
	}
}





