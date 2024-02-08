using Microsoft.EntityFrameworkCore;

OnlineEducationDbContext context = new();


var students = await context.Students.Include(s => s.Courses).Include(s => s.Teachers).ToListAsync();


await context.SaveChangesAsync();






Console.WriteLine("Hello, World!");





public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int StudentId { get; set; }
    public string Email { get; set; }
    public ICollection<StudentCourse> Courses { get; set; }
    public ICollection<CourseReview> CourseReviews { get; set; }
    public ICollection<Teacher> Teachers { get; set; }
}

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public string Surname { get; set; }
    public string Profession { get; set; }
    public ICollection<Student> Students { get; set; }
    public ICollection<Course> Courses { get; set; }
}


public class StudentCourse
{
    public int StudentsId { get; set; }
    public int CoursesId { get; set; }
    public Student Student { get; set; }
    public Course Course { get; set; }
}

public class Course
{
    public int Id { get; set; }
    public int CourseCategoryId { get; set; }
    public int TeacherId { get; set; }
    public string Title { get; set; }
    public string Statement { get; set; }
    public ICollection<StudentCourse> Students { get; set; }
    public CourseCategory CourseCategory { get; set; }
    public Teacher Teacher { get; set; }
    public ICollection<CourseReview> CourseReviews { get; set; }
}

public class CourseCategory
{
    public int Id { get; set; }
    public string Category { get; set; }
    public ICollection<Course> Courses { get; set; }
}

public class CourseReview
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string Comment { get; set; }
    public Student Student { get; set; }
    public Course Course { get; set; }
}





public class OnlineEducationDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseReview> CourseReviews { get; set; }
    public DbSet<CourseCategory> CourseCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasKey(s => s.Id);
        modelBuilder.Entity<Teacher>().HasKey(t => t.Id);
        modelBuilder.Entity<Course>().HasKey(c => c.Id);
        modelBuilder.Entity<CourseReview>().HasKey(c => c.Id);
        modelBuilder.Entity<CourseCategory>().HasKey(cc => cc.Id);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Teachers)
            .WithMany(t => t.Students);

        //modelBuilder.Entity<Student>()
        //    .HasMany(s => s.Courses)
        //    .WithMany(t => t.Students);

        //Default yaklaşımı burda iptal ettik ve kendimiz kurmaya çalıştık
        modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentsId, sc.CoursesId });

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithOne(sc => sc.Course)
            .HasForeignKey(sc => sc.CoursesId);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Courses)
            .WithOne(sc => sc.Student)
            .HasForeignKey(sc => sc.StudentsId);


        modelBuilder.Entity<Student>()
            .HasMany(s => s.CourseReviews)
            .WithOne(cr => cr.Student)
            .HasForeignKey(cr => cr.StudentId);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.CourseCategory)
            .WithMany(cc => cc.Courses)
            .HasForeignKey(c => c.CourseCategoryId);


        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Courses)
            .WithOne(c => c.Teacher)
            .HasForeignKey(c => c.TeacherId);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.CourseReviews)
            .WithOne(c => c.Course)
            .HasForeignKey(cr => cr.CourseId);


    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=OnlineEducationDb;Trusted_Connection=True");
    }
}