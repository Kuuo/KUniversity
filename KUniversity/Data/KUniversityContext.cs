using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KUniversity.Models
{
    public partial class KUniversityContext : DbContext
    {
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Campus> Campus { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Courseassignment> Courseassignment { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Eclass> Eclass { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Instructor> Instructor { get; set; }
        public virtual DbSet<Major> Major { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        public KUniversityContext(DbContextOptions<KUniversityContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Campus>(entity =>
            {
                entity.ToTable("campus");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Address).HasColumnType("varchar(255)");

                entity.Property(e => e.Name).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("FK_Course_Department");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId).HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Course_Department");
            });

            modelBuilder.Entity<Courseassignment>(entity =>
            {
                entity.ToTable("courseassignment");

                entity.HasIndex(e => e.CourseId)
                    .HasName("FK_CourseAssignment_Course");

                entity.HasIndex(e => e.InstructorId)
                    .HasName("FK_CourseAssignment_Instructor");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ClassRoom1)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ClassRoom2).HasColumnType("varchar(255)");

                entity.Property(e => e.ClassRoom3).HasColumnType("varchar(255)");

                entity.Property(e => e.ClassTime1)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ClassTime2).HasColumnType("varchar(255)");

                entity.Property(e => e.ClassTime3).HasColumnType("varchar(255)");

                entity.Property(e => e.CourseId).HasColumnType("int(11)");

                entity.Property(e => e.EndWeek).HasColumnType("int(11)");

                entity.Property(e => e.InstructorId).HasColumnType("int(11)");

                entity.Property(e => e.Order).HasColumnType("int(11)");

                entity.Property(e => e.Semester).HasColumnType("int(11)");

                entity.Property(e => e.StartWeek).HasColumnType("int(11)");

                entity.Property(e => e.Year).HasColumnType("int(11)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Courseassignment)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_CourseAssignment_Course");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Courseassignment)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_CourseAssignment_Instructor");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.CampusId)
                    .HasName("FK_Department_Campus");

                entity.HasIndex(e => e.InstructorId)
                    .HasName("FK_Department_Instructor");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CampusId).HasColumnType("int(11)");

                entity.Property(e => e.InstructorId).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Campus)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.CampusId)
                    .HasConstraintName("FK_Department_Campus");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Department_Instructor");
            });

            modelBuilder.Entity<Eclass>(entity =>
            {
                entity.ToTable("eclass");

                entity.HasIndex(e => e.InstructorId)
                    .HasName("FK_EClass_Instructor");

                entity.HasIndex(e => e.MajorId)
                    .HasName("FK_EClass_Major");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.InstructorId).HasColumnType("int(11)");

                entity.Property(e => e.MajorId).HasColumnType("int(11)");

                entity.Property(e => e.Order).HasColumnType("int(11)");

                entity.Property(e => e.StartYear).HasColumnType("int(11)");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Eclass)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_EClass_Instructor");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Eclass)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_EClass_Major");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("enrollment");

                entity.HasIndex(e => e.CourseAssignmentId)
                    .HasName("FK_Enrollment_CourseAssignment");

                entity.HasIndex(e => e.StudentId)
                    .HasName("FK_Enrollment_Student");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CourseAssignmentId).HasColumnType("int(11)");

                entity.Property(e => e.Grade).HasColumnType("decimal(10,0)");

                entity.Property(e => e.StudentId).HasColumnType("int(11)");

                entity.HasOne(d => d.CourseAssignment)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.CourseAssignmentId)
                    .HasConstraintName("FK_Enrollment_CourseAssignment");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Enrollment_Student");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("instructor");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("FK_Instructor_Department");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Academic).HasColumnType("varchar(255)");

                entity.Property(e => e.DepartmentId).HasColumnType("int(11)");

                entity.Property(e => e.Email).HasColumnType("varchar(255)");

                entity.Property(e => e.Gender)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Nation).HasColumnType("varchar(50)");

                entity.Property(e => e.OfficeLocation).HasColumnType("varchar(255)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.PhoneNumber).HasColumnType("varchar(20)");

                entity.HasOne(d => d.DepartmentNavigation)
                    .WithMany(p => p.InstructorNavigation)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Instructor_Department");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("major");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("FK_Major_Department");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId).HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Major)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Major_Department");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.HasIndex(e => e.EclassId)
                    .HasName("FK_Student_ECLass");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.EclassId)
                    .HasColumnName("EClassId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email).HasColumnType("varchar(255)");

                entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");

                entity.Property(e => e.Gender)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Nation).HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.PhoneNumber).HasColumnType("varchar(20)");

                entity.HasOne(d => d.Eclass)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.EclassId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Student_ECLass");
            });
        }
    }
}