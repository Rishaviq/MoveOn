using Microsoft.Extensions.Configuration;
using MoveOn.Models;
using System.Net;

namespace MoveOn
{ 
    internal class Program
    {
        public static IConfiguration configuration;
        static void Main(string[] args)
        {configurationfunc();


            Console.WriteLine("Hello, World!");
            User user = new User();
            
            Console.WriteLine(onLogIn(null, null));
            
        }

      static  public void onRegister(User user) {//предполагаемо от мобилното приложение ще вземем данните за потребителя 

            try
            {
                using (var Db = new HealthAppContext(configuration))
                {   //PasswordHasher(user.userPassword); //предполагаемо               
                    Db.Users.Add(user);
                    Db.SaveChanges();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }


        static public string onLogIn(string username, string password) {//отново предполагаемо идват от мобилното приложение

            using (var Db = new HealthAppContext(configuration))
            {

                var user = Db.Users

                        .Where(b => b.UserName.Equals(username))
                .Single();

                if (/*passwordVerifier(password, user.UserPassword) && user != null*/ true) //проверяваме дали паролата е вярна и дали всъщност има такъв потребител
                {
                    //някаква логика за аутентикация дали било с токен или при опит за достъп на определени данни

                    return "success";//
                }
                else { return "wrong username/password"; }


            }

        }



      static public void configurationfunc() {
            configuration = new ConfigurationBuilder()
.SetBasePath(AppContext.BaseDirectory) // Set the base path to the current directory
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // Load settings from appsettings.json
.AddUserSecrets<Program>()
.Build();
        }








    }
}
