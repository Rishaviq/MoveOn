using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MoveOn.Models;

public partial class HealthAppContext : DbContext
{
    private readonly IConfiguration _configuration;
    public HealthAppContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public HealthAppContext()
    {
    }

    public HealthAppContext(DbContextOptions<HealthAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<Excercise> Excercises { get; set; }

    public virtual DbSet<ExcerciseRecord> ExcerciseRecords { get; set; }

    public virtual DbSet<ExercisePerCondition> ExercisePerConditions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql(_configuration["SQL:connection"], Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Condition>(entity =>
        {
            entity.HasKey(e => e.IdConditions).HasName("PRIMARY");

            entity.ToTable("conditions");

            entity.Property(e => e.IdConditions)
                .ValueGeneratedNever()
                .HasColumnName("idConditions");
            entity.Property(e => e.ConditionsAvgRecovory).HasMaxLength(45);
            entity.Property(e => e.ConditionsName).HasMaxLength(45);
            entity.Property(e => e.ConditionsProblemArea).HasMaxLength(45);
        });

        modelBuilder.Entity<Excercise>(entity =>
        {
            entity.HasKey(e => e.Idexcercises).HasName("PRIMARY");

            entity.ToTable("excercises");

            entity.Property(e => e.Idexcercises)
                .ValueGeneratedNever()
                .HasColumnName("idexcercises");
            entity.Property(e => e.ExcercisesBodyPart)
                .HasMaxLength(45)
                .HasColumnName("excercises_BodyPart");
            entity.Property(e => e.ExcercisesName)
                .HasMaxLength(45)
                .HasColumnName("excercises_name");
        });

        modelBuilder.Entity<ExcerciseRecord>(entity =>
        {
            entity.HasKey(e => e.IdexcerciseRecords).HasName("PRIMARY");

            entity.ToTable("excercise_records");

            entity.HasIndex(e => e.ExcerciseRecordsExercise, "exercise id_idx");

            entity.HasIndex(e => e.ExcerciseRecordsUser, "user ids_idx");

            entity.Property(e => e.IdexcerciseRecords).HasColumnName("idexcercise_records");
            entity.Property(e => e.ExcerciseRecordsDate)
                .HasColumnType("datetime")
                .HasColumnName("excercise_records_date");
            entity.Property(e => e.ExcerciseRecordsExercise).HasColumnName("excercise_records_exercise");
            entity.Property(e => e.ExcerciseRecordsReps).HasColumnName("excercise_records_reps");
            entity.Property(e => e.ExcerciseRecordsUser).HasColumnName("excercise_records_user");

            entity.HasOne(d => d.ExcerciseRecordsExerciseNavigation).WithMany(p => p.ExcerciseRecords)
                .HasForeignKey(d => d.ExcerciseRecordsExercise)
                .HasConstraintName("exercise id");

            entity.HasOne(d => d.ExcerciseRecordsUserNavigation).WithMany(p => p.ExcerciseRecords)
                .HasForeignKey(d => d.ExcerciseRecordsUser)
                .HasConstraintName("user ids");
        });

        modelBuilder.Entity<ExercisePerCondition>(entity =>
        {
            entity.HasKey(e => e.IdexercisePerCondition).HasName("PRIMARY");

            entity.ToTable("exercise_per_condition");

            entity.HasIndex(e => e.Condition, "condition_for_excercise_idx");

            entity.HasIndex(e => e.Exercise, "exercise_idx");

            entity.Property(e => e.IdexercisePerCondition)
                .ValueGeneratedNever()
                .HasColumnName("idexercise_per_condition");
            entity.Property(e => e.Condition).HasColumnName("condition");
            entity.Property(e => e.Exercise).HasColumnName("exercise");

            entity.HasOne(d => d.ConditionNavigation).WithMany(p => p.ExercisePerConditions)
                .HasForeignKey(d => d.Condition)
                .HasConstraintName("condition_for_excercise");

            entity.HasOne(d => d.ExerciseNavigation).WithMany(p => p.ExercisePerConditions)
                .HasForeignKey(d => d.Exercise)
                .HasConstraintName("exercise");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.UserConditionId, "condition_idx");

            entity.Property(e => e.Iduser)
                .ValueGeneratedNever()
                .HasColumnName("iduser");
            entity.Property(e => e.UserAge).HasColumnName("userAge");
            entity.Property(e => e.UserConditionId).HasColumnName("userConditionId");
            entity.Property(e => e.UserDiet)
                .HasMaxLength(45)
                .HasColumnName("userDiet");
            entity.Property(e => e.UserGender)
                .HasMaxLength(45)
                .HasColumnName("userGender");
            entity.Property(e => e.UserName)
                .HasMaxLength(45)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(45)
                .HasColumnName("userPassword");
            entity.Property(e => e.UserWeight)
                .HasMaxLength(45)
                .HasColumnName("userWeight");

            entity.HasOne(d => d.UserCondition).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserConditionId)
                .HasConstraintName("condition");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
