using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections;
using System.IO;


namespace registratie88888888

{
    class Classq


    {


        public static void Menu()
        {
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




        }



        public static void Login()

        {
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

            Console.Clear();
            Console.WriteLine("Inloggen bij Nioscoop");
            Console.WriteLine("Voer uw gebruikersnaam in");
            input = Console.ReadLine();
            input = input.ToLower();
            if (input == "default")

            {
                Console.Clear();
                Console.WriteLine("error, probeer het opnieuw");
                Console.ReadKey();
                {
                    Classq.Welkom();
                }
            }
            foreach (string name in username)
            {
                if (name == input)
                {
                    int listNo = username.IndexOf(input);
                    Console.WriteLine("Voer uw wachtwoord in");
                    input = Console.ReadLine();
                    string passCheck = Convert.ToString(password[listNo]);
                    if (input == passCheck)
                    {
                        Console.Clear();
                        ID = listNo;
                        string lastLogin = Convert.ToString(time[ID]);
                        Console.WriteLine(@"Welkom " + name);
                        Console.WriteLine("Uw bent voor het laatst ingelogt om " + lastLogin);
                        Console.WriteLine("druk op enter om verder te gaan");
                        time[ID] = (Convert.ToString(DateTime.Now));
                        using (TextWriter writer = File.CreateText(fileTime))
                        {
                            foreach (string date in time)
                            {
                                writer.WriteLine(date);
                            }
                        }
                        Console.ReadKey();
                        login = true;
                        goto menu1;



                    menu1:

                        Console.Clear();
                        {


                            string user = Convert.ToString(username[ID]);
                            Console.WriteLine("\n Hoofd Menu \n Welkom terug " + user);

                            Console.WriteLine("\n [1] Uitloggen\n [2] Wachtwoord veranderen\n [3] Afsluiten");

                            input = Console.ReadLine();
                            input.ToLower();
                            switch (input)
                            {
                                case "1":
                                case "uitloggen":
                                case "log uit":
                                    Console.WriteLine("Wilt u echt uitloggen?");
                                    input = Console.ReadLine();
                                    if (input == "ja")
                                    {
                                        login = false;
                                        ID = 0;
                                        Console.WriteLine("u bent uitgelogt");
                                        Console.ReadKey();
                                    }
                                    break;




                                case "2":
                                case "Verander het wachtwoord":
                                    Console.WriteLine("Voer uw nieuwe wachtwoord in");
                                    input = Console.ReadLine();
                                    password[ID] = input;
                                    using (TextWriter writer = File.CreateText(filePasswordprem))
                                    {
                                        foreach (string pass in password)
                                        {
                                            writer.WriteLine(pass);


                                        }
                                    }
                                    Console.Clear();
                                    Console.WriteLine("Wachtwoord veranderd");
                                    Console.ReadKey();
                                    goto menu1;

                                case "3":
                                case "Afsluiten":
                                    Console.Clear();
                                    Console.WriteLine("Af aan het sluiten..");
                                    Console.ReadKey();
                                    Environment.Exit(0);
                                    break;

                                default:
                                    Console.WriteLine("?");
                                    Console.ReadKey();
                                    break;
                            }
                            Console.Clear();
                            {
                                Classq.Welkom();
                            }





                        }

                    }

                }
            }

        }






        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////





        public static void Registreer()

        {
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


        username:



            input = Console.ReadLine();
            input = input.ToLower();
            if (input == "")


            {
                Console.WriteLine("Registeren");
                Console.WriteLine("Voer een gebruikersnaam in");
                goto username;
            }
            foreach (string name in username)
            {
                if (name == input)
                {
                    Console.WriteLine("Deze gebruikersnaam is al in gebruik!");
                    Console.ReadKey();

                    Classq.Login();
                }
            }
            username.Add(input);
            Console.WriteLine("Voer uw wachtwoord in: ");
        password:
            input = Console.ReadLine();
            if (input == "")
            {
                Console.WriteLine("Voer uw wachtwoord in:");
                goto password;
            }
            password.Add(input);
            using (TextWriter writer = File.CreateText(fileUsername))
            {
                foreach (string name in username)
                {
                    writer.WriteLine(name);
                }
            }
            using (TextWriter writer = File.CreateText(filePassword))
            {
                foreach (string pass in password)
                {
                    writer.WriteLine(pass);
                }
            }
            time.Add(Convert.ToString(DateTime.Now));
            using (TextWriter writer = File.CreateText(fileTime))
            {
                foreach (string date in time)
                {
                    writer.WriteLine(date);
                }
            }
            Console.WriteLine("Account aanmaken voltooid!");
            Console.ReadKey();


            //////////////////////////////////////////////////////////////////////////////////















        }
        public static void Welkom()
        {
            {
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


                Console.Clear();
                Console.WriteLine("\n Welkom bij Nioscoop\n [1] Inloggen\n [2] Registreren\n [3] Een premium acccount aanmaken\n [4] Inloggen met uw premium account\n [5] Afsluiten");

            }


            ///////////////////////////////////////////////////////////////////////////////////////////////////
        }
        public static void Preminloggen()
        {
            {
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

                Console.Clear();
                Console.WriteLine("Inloggen bij Nioscoop");
                Console.WriteLine("Voer uw gebruikersnaam in");
                input = Console.ReadLine();
                input = input.ToLower();
                if (input == "default")
                {
                    Console.Clear();
                    Console.WriteLine("error, probeer het opnieuw!");
                    Console.ReadKey();
                    {
                        Classq.Welkom();
                    }
                }
                foreach (string name in usernamepremium)
                {
                    if (name == input)
                    {
                        int listNo2 = usernamepremium.IndexOf(input);
                        Console.WriteLine("Voer uw wachtwoord in");
                        input = Console.ReadLine();
                        string passCheck = Convert.ToString(passwordpremium[listNo2]);
                        if (input == passCheck)
                        {

                            ID = listNo2;
                            string lastLogin = Convert.ToString(time[ID]);

                            Console.WriteLine(@"Welkom " + name);
                            Console.WriteLine("Uw bent voor het laatst ingelogt om " + lastLogin);
                            Console.WriteLine("druk op enter om verder te gaan");
                            time[ID] = (Convert.ToString(DateTime.Now));
                            using (TextWriter writer = File.CreateText(fileTime))
                            {
                                foreach (string date in time)
                                {
                                    writer.WriteLine(date);
                                }
                            }
                            Console.ReadKey();
                            login = true;
                            goto menu2;


                        menu2:


                            Console.Clear();
                            {

                                string userpremium = Convert.ToString(usernamepremium[ID]);
                                Console.WriteLine("\n Hoofd Menu \n Welkom terug " + userpremium);

                                Console.WriteLine("\n [1] Uitloggen\n [2] Wachtwoord veranderen\n [3] Afsluiten");
                                input = Console.ReadLine();
                                input.ToLower();
                                switch (input)
                                {
                                    case "1":
                                    case "uitloggen":
                                    case "log uit":
                                        Console.WriteLine("Wilt u echt uitloggen?");
                                        input = Console.ReadLine();
                                        if (input == "ja")
                                        {
                                            login = false;
                                            ID = 0;
                                            Console.WriteLine("u bent uitgelogt");
                                            Console.ReadKey();
                                        }
                                        break;

                                    case "2":
                                    case "Verander het wachtwoord":
                                    case "Wachtwoord veranderen":
                                        Console.WriteLine("Voer uw nieuwe wachtwoord in");
                                        input = Console.ReadLine();
                                        passwordpremium[ID] = input;
                                        using (TextWriter writer = File.CreateText(filePasswordprem))
                                        {
                                            foreach (string pass in passwordpremium)
                                            {
                                                writer.WriteLine(pass);


                                            }
                                        }
                                        Console.WriteLine("Wachtwoord veranderd");
                                        Console.ReadKey();
                                        break;

                                    case "3":
                                    case "Afsluiten":
                                        Console.Clear();
                                        Console.WriteLine("Af aan het sluiten..");
                                        Console.ReadKey();
                                        Environment.Exit(0);
                                        break;

                                    default:
                                        Console.WriteLine("?");
                                        Console.ReadKey();
                                        break;



                                }
                                Console.Clear();
                                {
                                    Classq.Welkom();
                                }





                            }

                        }

                    }
                }
            }


        }
        public static void Premregistreer()
        {
            {
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


                Console.WriteLine("Uw account instellen");
            usernamepremium:
                input = Console.ReadLine();
                input = input.ToLower();
                if (input == "")
                {
                    Console.Clear();
                    Console.WriteLine("Registreren als premium gebruiker");
                    Console.WriteLine("Voer een gebruikersnaam in");
                    goto usernamepremium;
                }
                foreach (string name in usernamepremium)
                {
                    if (name == input)
                    {
                        Console.WriteLine("Deze gebruikersnaam is al in gebruik!");
                        Console.ReadKey();
                        goto usernamepremium;
                    }
                }
                usernamepremium.Add(input);
                Console.WriteLine("Uw wachtwoord: ");
            passwordpremium:
                input = Console.ReadLine();
                if (input == "")
                {
                    Console.WriteLine("Uw wachtwoord: ");
                    goto passwordpremium;
                }
                passwordpremium.Add(input);
                using (TextWriter writer = File.CreateText(fileUsernameprem))
                {
                    foreach (string name in usernamepremium)
                    {
                        writer.WriteLine(name);
                    }
                }
                using (TextWriter writer = File.CreateText(filePasswordprem))
                {
                    foreach (string pass in passwordpremium)
                    {
                        writer.WriteLine(pass);
                    }
                }
                time.Add(Convert.ToString(DateTime.Now));
                using (TextWriter writer = File.CreateText(fileTime))
                {
                    foreach (string date in time)
                    {
                        writer.WriteLine(date);
                    }
                    Console.Clear();
                    Console.WriteLine("Account aanmaken voltooid!");
                    Console.ReadKey();


                }





            }




        }
        public static void Afsluiten()
        {
            {
                Console.Clear();
                Console.WriteLine("Af aan het sluiten..");
                Console.ReadKey();
                Environment.Exit(0);





            }


        }
    }
}

