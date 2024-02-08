using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.Repository.Entities;

namespace OnlineEducationPlatform.Repository;

public partial class OnlineEducationDbContext : DbContext
{
    public OnlineEducationDbContext()
    {
    }

    public OnlineEducationDbContext(DbContextOptions<OnlineEducationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseCategory> CourseCategories { get; set; }

    public virtual DbSet<CourseReview> CourseReviews { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OnlineEducationDb;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => e.CourseCategoryId, "IX_Courses_CourseCategoryId");

            entity.HasIndex(e => e.TeacherId, "IX_Courses_TeacherId");

            entity.HasOne(d => d.CourseCategory).WithMany(p => p.Courses).HasForeignKey(d => d.CourseCategoryId);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses).HasForeignKey(d => d.TeacherId);
        });

        modelBuilder.Entity<CourseReview>(entity =>
        {
            entity.HasIndex(e => e.CourseId, "IX_CourseReviews_CourseId");

            entity.HasIndex(e => e.StudentId, "IX_CourseReviews_StudentId");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseReviews).HasForeignKey(d => d.CourseId);

            entity.HasOne(d => d.Student).WithMany(p => p.CourseReviews).HasForeignKey(d => d.StudentId);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasMany(d => d.Courses).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentCourse",
                    r => r.HasOne<Course>().WithMany().HasForeignKey("CoursesId"),
                    l => l.HasOne<Student>().WithMany().HasForeignKey("StudentsId"),
                    j =>
                    {
                        j.HasKey("StudentsId", "CoursesId");
                        j.ToTable("StudentCourse");
                        j.HasIndex(new[] { "CoursesId" }, "IX_StudentCourse_CoursesId");
                    });

            entity.HasMany(d => d.Teachers).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentTeacher",
                    r => r.HasOne<Teacher>().WithMany().HasForeignKey("TeachersId"),
                    l => l.HasOne<Student>().WithMany().HasForeignKey("StudentsId"),
                    j =>
                    {
                        j.HasKey("StudentsId", "TeachersId");
                        j.ToTable("StudentTeacher");
                        j.HasIndex(new[] { "TeachersId" }, "IX_StudentTeacher_TeachersId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
