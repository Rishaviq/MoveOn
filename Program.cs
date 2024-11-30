using Microsoft.Extensions.Configuration;
using MoveOn.Models;

namespace MoveOn
{ 
    internal class Program
    {
        public static IConfiguration configuration;
        static void Main(string[] args)
        {configurationfunc();


            Console.WriteLine("Hello, World!");
            User user = new User();
            onRegister(user);
            
        }

      static  public void onRegister(User user) {//предполагаемо от мобилното приложение ще вземем данните за потребителя 

            try
            {
                using (var Db = new HealthAppContext(configuration))
                {   //PasswordHasher(user.userPassword); //предполагемо               
                    Db.Users.Add(user);
                    Db.SaveChanges();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
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
