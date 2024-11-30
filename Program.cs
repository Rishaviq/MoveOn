using Microsoft.Extensions.Configuration;
using MoveOn.ActionClasses;
using MoveOn.Models;
using System.Net;

namespace MoveOn
{
    internal class Program
    {
        public static IConfiguration configuration;
        static void Main(string[] args)
        {configurationfunc();
        ExcerciseActions excerciseActions= new ExcerciseActions(configuration);
        UserActions userActions= new UserActions(configuration);


            Console.WriteLine("Hello, World!");
            User user = new User();
            
            Console.WriteLine(userActions.onLogIn(null, null));
            
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
