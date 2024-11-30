using System;
using System.Collections.Generic;

namespace MoveOn.Models;

public partial class Condition
{
    public int IdConditions { get; set; }

    public string? ConditionsName { get; set; }

    public string? ConditionsProblemArea { get; set; }

    public string? ConditionsAvgRecovory { get; set; }

    public virtual ICollection<ExercisePerCondition> ExercisePerConditions { get; set; } = new List<ExercisePerCondition>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
