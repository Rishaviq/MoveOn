using Microsoft.EntityFrameworkCore;
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
        public void onExerciserPageLoad(string username)
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

        public void onExerciseCompeteion(ExcerciseRecord[] excerciseRecords) {//взето от мобилното приложение при натискане на бутон за прилкючване на тренировка

            using (var Db = new HealthAppContext(configuration))
            {
                for (int i=0;i<excerciseRecords.Length;i++) {
                    targetRepsAdjustment(excerciseRecords[i].ExcerciseRecordsReps);
                    Db.ExcerciseRecords.Add(excerciseRecords[i]);
                    Db.SaveChanges(); }
            }
        }
        internal void targetRepsAdjustment(int? reps) {
            int target = 10;//в истинска имплементация бих го взел от локалния json файл от по рано

            if ((reps + 2) < target)
            {//използвам го като placeholder за това как да се прогресва през упражненията
                target = (reps.Value + target) / 2;// ако потребителя е далеч от желаните повторения ги намаляме,
                                                   // като не искаме да ги намалим прекалено много, затова използвам средната стойност между направените и целта

            }
            else
            {
                target++;//ако потребителя е успял да постигне целта повторения или я е надвишил овеличаваме желаните повторения за следващия път с 1

                progessIncrease(20);//предполагаемо се връща в приложението, като прогрес
            }
            
         //save(target)   //тук записваме желаните повторения отново в json файла, като те биха били сврзани със упражненията, на който са направени
        }


        public void onExerciseHistoryLoad(DateTime date1,DateTime date2,string user) {//дадени от приложението

            using (var Db = new HealthAppContext(configuration))
            {
                var exercises = Db.ExcerciseRecords
                    .Include(ex=>ex.ExcerciseRecordsUserNavigation)
                    .Where(ex=> ex.ExcerciseRecordsDate>=date1 && ex.ExcerciseRecordsDate <= date2 && ex.ExcerciseRecordsUserNavigation.UserName==user)
                    .ToList();//вимаме всичките направени упражнения и техните повторения за да може да се представи дадения прогрес
               
                //логика за това как всъщност да се представят данните
            }
        }

        public int progessIncrease(int progress) {
            int lvl = 0;
            int progressBar = 14;//предполагаемо взето локално от приложението на потребителя

            if (progress > 0 && progress < 100)
            {
                progressBar = progressBar + progress;
                if (progressBar > 100)
                {
                    progressBar = 0;
                    lvl++;
                }
            }
            return progressBar;
        }
    }
}
