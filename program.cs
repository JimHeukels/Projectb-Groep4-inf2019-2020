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

			//var datumTest = DateTime.Now.AddHours(2);
			//var TestDatum = DateTime.Now.AddHours(6);

			//Console.WriteLine(TestDatum.ToString("yyyy MM dd"));

			//foreach (var _vertoning in bioscopen[0].Schermen[0].Vertoningen)
			//{
			//var value = _vertoning.Value;
			//Console.WriteLine(value.AanvangsTijd);
			//}

			//if(vertoning.AanvangsTijd < DateTime.Now.AddDays(14))
			//{
			//    Console.WriteLine("vertoning is premium A.F.");
			//}

			repo.beschikbareFilmsNaarVertoning();



			/*
			var repo2 = new FilmRepository();
			//
			string input;
			int ID = 0;
			bool login = false;

			var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
			var fileUsername = Path.Combine(projectFolder, @"username.json");
			string[] usernameArray = File.ReadAllLines(fileUsername);
			ArrayList username = new ArrayList(usernameArray);
			var filePassword = Path.Combine(projectFolder, @"password.json");
			string[] passwordArray = File.ReadAllLines(filePassword);
			ArrayList password = new ArrayList(passwordArray);
			var fileUsernameprem = Path.Combine(projectFolder, @"usernamepremium.json");
			string[] usernamepremiumArray = File.ReadAllLines(fileUsernameprem);
			ArrayList usernamepremium = new ArrayList(usernamepremiumArray);
			var filePasswordprem = Path.Combine(projectFolder, @"passwordpremium.json");
			string[] passwordpremiumArray = File.ReadAllLines(filePasswordprem);
			ArrayList passwordpremium = new ArrayList(passwordpremiumArray);
			var fileTime = Path.Combine(projectFolder, @"time.json");
			string[] timeArray = File.ReadAllLines(fileTime);
			ArrayList time = new ArrayList(timeArray);
			var jsonusername = JsonSerializer.Serialize(username);
			var jsonspassword = JsonSerializer.Serialize(password);
			var jsonspremiumpassword = JsonSerializer.Serialize(passwordpremium);
			var jsonusernamepremium = JsonSerializer.Serialize(usernamepremium);
			var tijdregistratie = JsonSerializer.Serialize(time);

			Console.WriteLine("Welkom bij Nioscoop");
			Console.WriteLine("Kies uw optie:\n(1) U wilt een ticket kopen\n(2) U wilt zich aanmelden/registreren");
			int antwoordOptie = Convert.ToInt32(Console.ReadLine());
			if (antwoordOptie == 1)
			{
				Console.WriteLine("U kunt een ticket kopen");
			}
			else if (antwoordOptie == 2)
			{
			start:
				if (login == true)
				{
					Console.Clear();
					Classq.Menu();
				}
				Classq.Welkom();
				input = Console.ReadLine();
				switch (input)

				{

					case "1":
					case "inloggen als Medewerker":

						{
							Console.Clear();
							Classq.Login();
						}
						goto start;




					case "2":
					case "Registreren":


						{
							Console.Clear();
							Classq.Registreer();
						}

						goto start;



					case "5":
					case "Afsluiten":
						{

							Classq.Afsluiten();
						}
						break;

					default:
						Console.WriteLine("?");
						Console.ReadKey();
						break;

					case "4":
					case "premiumlogin":
						{
							Console.Clear();
							Classq.Preminloggen();
						}


						goto start;




					case "3"://///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
						{
							Console.Clear();
							Classq.Premregistreer();
						}
						goto start;

					case "8":
					case "Afsluitenn":
						{
							Console.Clear();
							Classq.Afsluiten();
						}

					default2:

						Console.WriteLine("?");
						Console.ReadKey();
						break;
				}

			}

		}



	}
}


