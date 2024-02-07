using Microsoft.EntityFrameworkCore;

OnlineEducationDbContext context = new();

//List<Student> students = new List<Student>()
//{
//    new Student()
//    {
//        Name = "Dincer",
//        Surname = "Yigit",
//        StudentId = 1258,
//        Email = "dincer@dincer.com",
//    },
//    new Student()
//    {
//        Name = "Gamze",
//        Surname = "Yigit",
//        StudentId = 3096,
//        Email = "Gamze@Gamze.com",
//    },
//    new Student()
//    {
//        Name = "Dilara",
//        Surname = "Yigit",
//        StudentId = 4536,
//        Email = "Dilara@Dilara.com",
//    }
//};

//List<Teacher> teachers = new List<Teacher>()
//{
//    new Teacher()
//    {
//        Name = "Kemal",
//        Surname = "Lale",
//        Email = "Kemal@Kemal.com",
//        Profession = "Fen",
//    },
//    new Teacher()
//    {
//        Name = "Kazım",
//        Surname = "Kocak",
//        Email = "Kazım@Kazım.com",
//        Profession = "Turkce"
//    },
//    new Teacher()
//    {
//        Name = "Ayşe",
//        Surname = "Sönmez",
//        Email = "Ayşe@Ayşe.com",
//        Profession = "Matematik"
//    },
//};




await context.SaveChangesAsync();






Console.WriteLine("Hello, World!");





public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int StudentId { get; set; }
    public string Email { get; set; }
    public ICollection<Course> Courses { get; set; }
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


public class Course
{
    public int Id { get; set; }
    public int CourseCategoryId { get; set; }
    public int TeacherId { get; set; }
    public string Title { get; set; }
    public string Statement { get; set; }
    public ICollection<Student> Students { get; set; }
    public CourseCategory CourseCategory { get; set; }
    public Teacher Teacher { get; set; }
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
    public string Comment { get; set; }
    public Student Student { get; set; }
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

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Courses)
            .WithMany(t => t.Students);

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


    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=OnlineEducationDb;Trusted_Connection=True");
    }
}