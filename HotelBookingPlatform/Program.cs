using Microsoft.EntityFrameworkCore;


HotelBookingDbContext context = new();

List<Visitor> visitors = new List<Visitor>()
{
    new Visitor()
    {
        Email = "dincer@dincer.com"
    },
};


context.Visitors.AddRangeAsync();



Console.WriteLine("Hello, World!");


public class Visitor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string IdentityNo { get; set; }
    public ICollection<Rezervation> Rezervations { get; set; }
}

public class Room
{
    public int Id { get; set; }
    public RoomType RoomType { get; set; }
    public int RoomNo { get; set; }
    public Rezervation Rezervation { get; set; }
}



public class Rezervation
{
    public int Id { get; set; }
    public int VisitorId { get; set; }
    public Room Room { get; set; }
    public bool IsPay { get; set; }
    public Visitor Visitor { get; set; }
}

public enum RoomType
{
    Standart,
    Suit,
    King,
    Premium
}





public class HotelBookingDbContext : DbContext
{
    public DbSet<Visitor> Visitors { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Rezervation> Rezervations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Visitor>().HasKey(v => v.Id);
        modelBuilder.Entity<Room>().HasKey(r => r.Id);
        modelBuilder.Entity<Rezervation>().HasKey(r => r.Id);

        modelBuilder.Entity<Rezervation>()
            .HasOne(r => r.Room)
            .WithOne(r => r.Rezervation)
            .HasForeignKey<Rezervation>(r => r.Id);

        modelBuilder.Entity<Visitor>()
            .HasMany(v => v.Rezervations)
            .WithOne(r => r.Visitor)
            .HasForeignKey(r => r.VisitorId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=HotelBookingDb;Trusted_Connection=True;");

        
    }
}