using System;
using System.Collections.Generic;

namespace MoveOn.Models;

public partial class Excercise
{
    public int Idexcercises { get; set; }

    public string? ExcercisesName { get; set; }

    public string? ExcercisesBodyPart { get; set; }

    public virtual ICollection<ExcerciseRecord> ExcerciseRecords { get; set; } = new List<ExcerciseRecord>();

    public virtual ICollection<ExercisePerCondition> ExercisePerConditions { get; set; } = new List<ExercisePerCondition>();
}
