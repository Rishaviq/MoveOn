using System;
using System.Collections.Generic;

namespace MoveOn.Models;

public partial class ExercisePerCondition
{
    public int IdexercisePerCondition { get; set; }

    public int? Exercise { get; set; }

    public int? Condition { get; set; }

    public virtual Condition? ConditionNavigation { get; set; }

    public virtual Excercise? ExerciseNavigation { get; set; }
}
