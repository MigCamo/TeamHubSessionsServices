using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeamHubSessionsServices.Entities;

public partial class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public virtual DbSet<document> document { get; set; }

    public virtual DbSet<project> project { get; set; }

    public virtual DbSet<projectdocument> projectdocument { get; set; }

    public virtual DbSet<projectstudent> projectstudent { get; set; }

    public virtual DbSet<projecttask> projecttask { get; set; }

    public virtual DbSet<student> student { get; set; }

    public virtual DbSet<studentsession> studentsession { get; set; }

    public virtual DbSet<task> task { get; set; }

    public virtual DbSet<taskstudent> taskstudent { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost ; database=teamhub; user=MigCamo; password=Mig123456789");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<document>(entity =>
        {
            entity.HasKey(e => e.IdDocument).HasName("PRIMARY");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Ruta).HasMaxLength(250);
        });

        modelBuilder.Entity<project>(entity =>
        {
            entity.HasKey(e => e.IdProject).HasName("PRIMARY");

            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<projectdocument>(entity =>
        {
            entity.HasKey(e => e.IdProjectDocument).HasName("PRIMARY");

            entity.HasIndex(e => e.IdDocument, "IdDocument");

            entity.HasIndex(e => e.IdProject, "IdProject");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.projectdocument)
                .HasForeignKey(d => d.IdDocument)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projectdocument_ibfk_2");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.projectdocument)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projectdocument_ibfk_1");
        });

        modelBuilder.Entity<projectstudent>(entity =>
        {
            entity.HasKey(e => e.IdProjectStudent).HasName("PRIMARY");

            entity.HasIndex(e => e.IdProject, "IdProject");

            entity.HasIndex(e => e.IdStudent, "IdStudent");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.projectstudent)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projectstudent_ibfk_1");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.projectstudent)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projectstudent_ibfk_2");
        });

        modelBuilder.Entity<projecttask>(entity =>
        {
            entity.HasKey(e => e.IdProjectTask).HasName("PRIMARY");

            entity.HasIndex(e => e.IdProject, "IdProject");

            entity.HasIndex(e => e.IdTask, "IdTask");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.projecttask)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projecttask_ibfk_1");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.projecttask)
                .HasForeignKey(d => d.IdTask)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("projecttask_ibfk_2");
        });

        modelBuilder.Entity<student>(entity =>
        {
            entity.HasKey(e => e.IdStudent).HasName("PRIMARY");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(15);
            entity.Property(e => e.MiddleName).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(15);
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.ProDocumentImage).HasMaxLength(250);
            entity.Property(e => e.SurName).HasMaxLength(15);
        });

        modelBuilder.Entity<studentsession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.IdStudent, "Usuario_idx");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IPAdress).HasMaxLength(30);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(155);

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.studentsession)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Usuario");
        });

        modelBuilder.Entity<task>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("PRIMARY");

            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<taskstudent>(entity =>
        {
            entity.HasKey(e => e.IdTaskStudent).HasName("PRIMARY");

            entity.HasIndex(e => e.IdStudent, "IdStudent");

            entity.HasIndex(e => e.IdTask, "IdTask");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.taskstudent)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("taskstudent_ibfk_2");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.taskstudent)
                .HasForeignKey(d => d.IdTask)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("taskstudent_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
