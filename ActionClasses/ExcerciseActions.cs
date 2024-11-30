using Microsoft.Extensions.Configuration;
using MoveOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveOn.ActionClasses
{
    internal class ExcerciseActions(IConfiguration configuration)
    {
        public void onExcerciserPageLoad(string username)
        {
            using (var Db = new HealthAppContext(configuration))
            {
                var exercises = Db.Excercises
                .Where(ex => Db.ExercisePerConditions
                    .Any(epc => epc.Exercise == ex.Idexcercises &&
                         Db.Conditions
                           .Any(c => c.IdConditions == epc.Condition &&
                                Db.Users
                                           .Any(u => u.UserName == username &&
                                                     u.UserConditionId == c.IdConditions))))
                .ToList();//би трябвало да връща всичките упражнения който потребителя трябва да прави
                          //би трябвало да работи като преглежда кое заболяване има потребителя и изважда упражненията свързани с това заболяване



                //тук предполагаемо има логика за дисплейване на упражненията
                //заедно с тях трябва да има и цел повторения за упражнение, която цел бих запазвал локално в json файл.

            }

        }

    }
}
