using Microsoft.EntityFrameworkCore;
using SimplyBooksBE.Models;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class SimplyBooksBEDbContextFactory : IDesignTimeDbContextFactory<SimplyBooksBEDbContext>
{
    public SimplyBooksBEDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // so it can find appsettings.json
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<SimplyBooksBEDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(connectionString);

        return new SimplyBooksBEDbContext(builder.Options);
    }
}

public class SimplyBooksBEDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    public SimplyBooksBEDbContext(DbContextOptions<SimplyBooksBEDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(new Author[]
        {
            new Author{ Id = 1, Email = "dinnerdoggy@gmail.com", FirstName = "Casey", LastName = "Cunningham", Image = "https://avatars.githubusercontent.com/u/31261276?v=4", Favorite = true, Uid = "oQWpgCUQhWfVTf3fVK6G5XdG7Z73" },
            new Author{ Id = 2, Email = "email@email.com", FirstName = "John", LastName = "Doe", Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/John_Doe%2C_born_John_Nommensen_Duchac.jpg/1200px-John_Doe%2C_born_John_Nommensen_Duchac.jpg", Favorite = false, Uid = "oQWpgCUQhWfVTf3fVK6G5XdG7Z73" }
        });

        modelBuilder.Entity<Book>().HasData(new Book[]
        {
            new Book { Id = 1, AuthorId = 1, Title = "Casey's Best Book", Image = "https://media.canva.com/v2/files/uri:ifs%3A%2F%2FM%2F9e5ef14c-b183-4307-a6e1-c0fdf559d5b6?csig=AAAAAAAAAAAAAAAAAAAAABYvq5u88tkGEvkQadcue_6AdY_e7KxdVct1etJC4glY&exp=1743750464&signer=media-rpc&token=AAIAAU0AJDllNWVmMTRjLWIxODMtNDMwNy1hNmUxLWMwZmRmNTU5ZDViNgAAAAABlf-gggDo8EPWDxB_7R8vCdmFIhCgucvH9pcq6GuRg0Kh6Eohvg", Price = 49.99m, Sale = false, Description = "The Best Book Ever", Uid = "oQWpgCUQhWfVTf3fVK6G5XdG7Z73" },
            new Book { Id = 2, AuthorId = 2, Title = "John's Best Book", Image = "https://media.canva.com/v2/files/uri:ifs%3A%2F%2FM%2F9e5ef14c-b183-4307-a6e1-c0fdf559d5b6?csig=AAAAAAAAAAAAAAAAAAAAABYvq5u88tkGEvkQadcue_6AdY_e7KxdVct1etJC4glY&exp=1743750464&signer=media-rpc&token=AAIAAU0AJDllNWVmMTRjLWIxODMtNDMwNy1hNmUxLWMwZmRmNTU5ZDViNgAAAAABlf-gggDo8EPWDxB_7R8vCdmFIhCgucvH9pcq6GuRg0Kh6Eohvg", Price = 99.99m, Sale = false, Description = "It's just an okay book", Uid = "oQWpgCUQhWfVTf3fVK6G5XdG7Z73" }
        });
    }
}