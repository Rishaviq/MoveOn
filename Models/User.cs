﻿using System;
using System.Collections.Generic;

namespace MoveOn.Models;

public partial class User
{
    public int Iduser { get; set; }

    public string? UserName { get; set; }

    public int? UserAge { get; set; }

    public string? UserWeight { get; set; }

    public string? UserGender { get; set; }

    public int? UserConditionId { get; set; }

    public string? UserDiet { get; set; }

    public string? UserPassword { get; set; }

    public virtual ICollection<ExcerciseRecord> ExcerciseRecords { get; set; } = new List<ExcerciseRecord>();

    public virtual Condition? UserCondition { get; set; }
}
