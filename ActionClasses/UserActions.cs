using Microsoft.Extensions.Configuration;
using MoveOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveOn.ActionClasses
{
    public class UserActions(IConfiguration configuration)
    {
        public string onLogIn(string username, string password)
        {//отново предполагаемо идват от мобилното приложение
         //предполагаемо се извиква при натискане на бутон за вписване/log in

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



        public void onRegister(User user)
        {//предполагаемо от мобилното приложение ще вземем данните за потребителя 
         //предполагаемо се извиква при натискане на бутон за регистрация

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
    }
}
