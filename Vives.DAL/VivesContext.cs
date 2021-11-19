using Microsoft.EntityFrameworkCore;
using System;
using Vives.DOMAIN;
using Vives.DOMAIN.Helpers;

namespace Vives.DAL
{
    public class VivesContext : DbContext
    {
        public static string LocalConnectionString { get; set; } = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VivesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string OnlineConnectionString { get; set; } = @"";

        public VivesContext()
        {
            //this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(LocalConnectionString);
            //optionsBuilder.UseSqlServer(OnlineConnectionString);
            //optionsBuilder.EnableSensitiveDataLogging(true);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        //https://docs.microsoft.com/en-us/ef/core/modeling/generated-properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region IGNORE
            modelBuilder.Ignore<GObject>();
            modelBuilder.Ignore<VivesException>();
            modelBuilder.Ignore<Exception>();
            #endregion

            #region PRIMARY KEYS
            modelBuilder.Entity<Student>()
                    .HasKey(x => x.ID);
            modelBuilder.Entity<Course>()
                    .HasKey(x => x.ID);
            #endregion

            #region UNIQUE KEYS
            modelBuilder.Entity<Student>()
                    .HasIndex(x => x.Private_Email)
                    .IsUnique();
            #endregion

            #region Student
            modelBuilder.Entity<Student>()
                    .Property(x => x.RNumber)
                    .HasComputedColumnSql("'r' + RIGHT('0000000' + CAST([ID] AS VARCHAR), 7)", stored:true);
            modelBuilder.Entity<Student>()
                    .Property(x => x.Firstname)
                    .IsRequired()
                    .HasMaxLength(50);
            modelBuilder.Entity<Student>()
                    .Property(x => x.Lastname)
                    .IsRequired()
                    .HasMaxLength(50);
            modelBuilder.Entity<Student>()
                    .Property(x => x.Date_Of_Birth)
                    .HasColumnType("datetime")
                    .IsRequired();
            modelBuilder.Entity<Student>()
                    .Property(x => x.Registration_Date)
                    .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Student>()
                    .Property(x => x.Official_Email)
                    .HasComputedColumnSql("'r' + RIGHT('00000000' + CAST([ID] AS VARCHAR), 8) + '@student.vives.be'");
            #endregion

            #region COURSE
            modelBuilder.Entity<Course>()
                    .Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(75);
            modelBuilder.Entity<Course>()
                    .Property(x => x.SP)
                    .IsRequired()
                    .HasDefaultValue(1);
            modelBuilder.Entity<Course>()
                    .Property(x => x.Course_Code)
                    .HasComputedColumnSql("'VP' + CAST([SP] AS VARCHAR) + RIGHT('0000' + CAST([ID] AS VARCHAR), 4)");
            #endregion

            #region MANY-TO-MANY
            modelBuilder.Entity<StudentCourse>()
                    .HasKey(x => new { x.StudentID, x.CourseID });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(pt => pt.Student)
                .WithMany(p => p.Courses)
                .HasForeignKey(pt => pt.StudentID);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(pt => pt.Course)
                .WithMany(t => t.Students)
                .HasForeignKey(pt => pt.CourseID);
            #endregion
        }


    }
}
