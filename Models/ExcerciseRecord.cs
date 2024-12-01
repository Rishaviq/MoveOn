using System;
using System.Collections.Generic;

namespace MoveOn.Models;

public partial class ExcerciseRecord
{
    public int IdexcerciseRecords { get; set; }

    public int? ExcerciseRecordsUser { get; set; }

    public int? ExcerciseRecordsExercise { get; set; }

    public int? ExcerciseRecordsReps { get; set; }

    public DateTime? ExcerciseRecordsDate { get; set; }

    public virtual Excercise? ExcerciseRecordsExerciseNavigation { get; set; }

    public virtual User? ExcerciseRecordsUserNavigation { get; set; }
}
