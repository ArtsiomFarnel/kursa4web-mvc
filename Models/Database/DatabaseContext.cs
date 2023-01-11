using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursach.Models.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kursach.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<WrittenOff> WrittenOffs { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Scientist> Scientists { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Schooler> Schoolers { get; set; }
        public DbSet<Literature> Literatures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Filter> Filters { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(user => user.Name).IsRequired().HasMaxLength(30);
                entity.Property(user => user.Password).IsRequired().HasMaxLength(30);
                entity.Property(user => user.Email).IsRequired().HasMaxLength(50);
                entity.Property(user => user.DateOfRegistration).IsRequired().HasColumnType("datetime").HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Library>(entity =>
            {
                entity.Property(library => library.Name).IsRequired().HasMaxLength(30);
                entity.Property(library => library.Address).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.Property(publication => publication.Name).IsRequired().HasMaxLength(30);
                
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(author => author.Name).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(client => client.IdentityNumber).IsRequired().HasMaxLength(30);
                entity.Property(client => client.Address).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(room => room.Name).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(worker => worker.IdentityNumber).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(role => role.Name).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Copy>(entity =>
            {
                entity.Property(copy => copy.IdentityNumber).IsRequired().HasMaxLength(30);
                entity.Property(copy => copy.Amount).IsRequired().HasDefaultValue(0);
                entity.Property(copy => copy.Days).IsRequired().HasDefaultValue(14);
                entity.Property(copy => copy.IsReadOnly).IsRequired().HasDefaultValue(false);
                entity.Property(copy => copy.IsWrittenOff).IsRequired().HasDefaultValue(false);
                entity.Property(copy => copy.ReplenishmentDate).IsRequired().HasColumnType("datetime").HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<WrittenOff>(entity =>
            {
                entity.Property(writtenoff => writtenoff.WorkerIdentityNumber).HasMaxLength(30);
                entity.Property(writtenoff => writtenoff.Date).IsRequired().HasColumnType("datetime").HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.Property(operation => operation.Name).IsRequired().HasMaxLength(30);
                entity.Property(operation => operation.Status).IsRequired().HasMaxLength(30);
                entity.Property(operation => operation.Date).IsRequired().HasColumnType("datetime").HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(book => book.Name).IsRequired().HasMaxLength(30);
                entity.Property(book => book.Year).IsRequired().HasMaxLength(4);
                entity.Property(book => book.Image).IsRequired();
            });

            modelBuilder.Entity<Debt>(entity =>
            {
                entity.Property(debt => debt.ReceivingDate).IsRequired().HasColumnType("datetime");
                entity.Property(debt => debt.EstimatedReturnDate).IsRequired().HasColumnType("datetime");
                entity.Property(debt => debt.IsReturned).IsRequired().HasDefaultValue(false);
            });

            modelBuilder.Entity<Scientist>(entity =>
            {
                entity.Property(scientist => scientist.OrganizationName).IsRequired().HasMaxLength(30);
                entity.Property(scientist => scientist.ScientificTheme).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(student => student.UniversityName).IsRequired().HasMaxLength(30);
                entity.Property(student => student.FacultyName).IsRequired().HasMaxLength(30);
                entity.Property(student => student.GroupName).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Schooler>(entity =>
            {
                entity.Property(schooler => schooler.SchoolName).IsRequired().HasMaxLength(30);
                entity.Property(schooler => schooler.ClassName).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Literature>(entity =>
            {
                entity.Property(literature => literature.Name).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(category => category.Name).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(genre => genre.Name).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Storage>(entity => { });

            modelBuilder.Entity<Filter>(entity => { });

            //disable cascade deleting
            var fkeys = modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fkey in fkeys)
                fkey.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<Role>().HasData(new Role[] 
            {
                new Role { Id = 1, Name = "admin" },
                new Role { Id = 2, Name = "worker" },
                new Role { Id = 3, Name = "client" },
                new Role { Id = 4, Name = "user" }
            });

            modelBuilder.Entity<User>().HasData(new User[] 
            {
                new User { Id = 1, Name = "admin", Password = "admin", RoleId = 1, Email = "admin@gmail.com" },
                new User { Id = 2, Name = "worker1", Password = "worker", RoleId = 2, Email = "worker1@gmail.com" },
                new User { Id = 3, Name = "client1", Password = "client", RoleId = 3, Email = "client1@gmail.com" },
                new User { Id = 4, Name = "worker2", Password = "worker", RoleId = 2, Email = "worker2@gmail.com" },
                new User { Id = 5, Name = "client2", Password = "client", RoleId = 3, Email = "client2@gmail.com" },
                new User { Id = 6, Name = "client3", Password = "client", RoleId = 3, Email = "client3@gmail.com" },
                new User { Id = 7, Name = "client4", Password = "client", RoleId = 3, Email = "client4@gmail.com" },
                
            });

            modelBuilder.Entity<Library>().HasData(new Library[] 
            {
                new Library { Id = 1, Name = "Мир Фэнтези", Address = "ул. Пушкина, д. Колотушкина", Description = "Самые популярные произведения в жанре фэнтези за последние 100 лет" },
                new Library { Id = 2, Name = "Художественная литература", Address = "ул. Вязов", Description = "Хотите пощекотать нервы? Самые популярные книги жанра ужасы только у нас" }
            });

            modelBuilder.Entity<Client>().HasData(new Client[]
            {
                new Client { Id = 1, IdentityNumber = "client1", UserId = 3, LibraryId = 1, Address = "test" },
                new Client { Id = 2, IdentityNumber = "client2", UserId = 5, LibraryId = 1, Address = "test" },
                new Client { Id = 3, IdentityNumber = "client3", UserId = 6, LibraryId = 2, Address = "test" },
                new Client { Id = 4, IdentityNumber = "client4", UserId = 7, LibraryId = 2, Address = "test" }
            });

            modelBuilder.Entity<Room>().HasData(new Room[]
            {
                new Room { Id = 1, Name = "room1", LibraryId = 1 },
                new Room { Id = 2, Name = "room2", LibraryId = 2 },
                new Room { Id = 3, Name = "room3", LibraryId = 2 }
            });

            modelBuilder.Entity<Worker>().HasData(new Worker[]
            {
                new Worker { Id = 1, IdentityNumber = "work1", UserId = 2, RoomId = 1},
                new Worker { Id = 2, IdentityNumber = "work2", UserId = 4, RoomId = 2}
            });

            modelBuilder.Entity<Author>().HasData(new Author[]
            {
                new Author { Id = 1, Name = "Дж. Р. Р. Толкин" },
                new Author { Id = 2, Name = "А. Сапковский" }
            });

            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category { Id = 1, Name = "рассказ"},
                new Category { Id = 2, Name = "роман"}
            });

            modelBuilder.Entity<Book>().HasData(new Book[]
            {
                new Book { Id = 1, Name = "Хоббит", CategoryId = 1, Year = 1937, Description = "Перед вами - самая любимая волшебная сказка для детей в самом любимом оформлении, знакомом каждому", Image = "/src/img/books/1.jpg" },
                new Book { Id = 2, Name = "Властелин колец", CategoryId = 1, Year = 1954, Description = "Трилогия Властелин Колец бесспорно возглавляет список культовых книг ХХ века.", Image = "/src/img/books/2.jpg" },
                new Book { Id = 3, Name = "Сильмариллион", CategoryId = 1, Year = 1977, Description = "Сильмариллион - один из масштабнейших миров в истории фэнтези, мифологический канон, который Джон Руэл Толкин составлял на протяжении всей жизни.", Image = "/src/img/books/3.jpg" },
                new Book { Id = 4, Name = "Ведьмак", CategoryId = 2, Year = 1986, Description = "Сага о ведьмаке польского писателя Анджея Сапковского написана в жанре темного фэнтези.", Image = "/src/img/books/4.jpg" }
            });

            modelBuilder.Entity<Publication>().HasData(new Publication[]
            {
                new Publication { Id = 1, Name = "Аверсэв", BookId = 1 },
                new Publication { Id = 2, Name = "Аверсэв", BookId = 2 },
                new Publication { Id = 3, Name = "Самое лучшее", BookId = 3 },
                new Publication { Id = 4, Name = "Самое лучшее", BookId = 4 }
            });

            modelBuilder.Entity<Literature>().HasData(new Literature[]
            {
                new Literature { Id = 1, Name = "Хоббит, туда и обратно", AuthorId = 1, BookId = 1},
                new Literature { Id = 2, Name = "Братство кольца", AuthorId = 1, BookId = 2},
                new Literature { Id = 3, Name = "Две башни", AuthorId = 1, BookId = 2},
                new Literature { Id = 4, Name = "Возвращение короля", AuthorId = 1, BookId = 2},
                new Literature { Id = 5, Name = "Сильмариллион: повести", AuthorId = 1, BookId = 3},
                new Literature { Id = 6, Name = "Все расказы из основного цикла", AuthorId = 2, BookId = 4}
            });

            modelBuilder.Entity<Storage>().HasData(new Storage[]
            {
                new Storage { Id = 1, RoomId = 1},
                new Storage { Id = 2, RoomId = 2},
                new Storage { Id = 3, RoomId = 3}
            });

            modelBuilder.Entity<Copy>().HasData(new Copy[]
            {
                new Copy { Id = 1, Amount = 5, Days = 14, IdentityNumber = "copy1", PublicationId = 1, StorageId = 1},
                new Copy { Id = 2, Amount = 1, Days = 14, IdentityNumber = "copy2", PublicationId = 2, StorageId = 1},
                new Copy { Id = 3, Amount = 4, Days = 14, IdentityNumber = "copy3", PublicationId = 3, StorageId = 1},
                new Copy { Id = 4, Amount = 10, Days = 14, IdentityNumber = "copy4", PublicationId = 4, StorageId = 1},
                new Copy { Id = 5, Amount = 11, Days = 14, IdentityNumber = "copy5", PublicationId = 4, StorageId = 2},
                new Copy { Id = 6, Amount = 10, Days = 14, IdentityNumber = "copy6", PublicationId = 4, StorageId = 3}
            });

            modelBuilder.Entity<Operation>().HasData(new Operation[]
            {
                new Operation { ClientId = 1, Name = "returned", CopyId = 1, Id = 1, Status = "yes", WorkerId = 1 },
                new Operation { ClientId = 1, Name = "returned", CopyId = 1, Id = 2, Status = "yes", WorkerId = 1 },
                new Operation { ClientId = 1, Name = "returned", CopyId = 1, Id = 3, Status = "yes", WorkerId = 1 }
            });

            modelBuilder.Entity<Genre>().HasData(new Genre[]
            {
                new Genre { Id = 1, Name = "фэнтези" },
                new Genre { Id = 2, Name = "драма"}
            });

            modelBuilder.Entity<Filter>().HasData(new Filter[]
            {
                new Filter { Id = 1, BookId = 1, GenreId = 1 },
                new Filter { Id = 2, BookId = 1, GenreId = 2 },
                new Filter { Id = 3, BookId = 2, GenreId = 1 },
                new Filter { Id = 4, BookId = 2, GenreId = 2 },
                new Filter { Id = 5, BookId = 3, GenreId = 1 },
                new Filter { Id = 6, BookId = 4, GenreId = 2 },
                new Filter { Id = 7, BookId = 4, GenreId = 1 }
            });

            modelBuilder.Entity<Student>().HasData(new Student[]
            {
                new Student { Id = 1, UserId = 3, CourseNumber = 4, FacultyName = "F", GroupName = "sfb", UniversityName = "ПГУ" }
            });

            modelBuilder.Entity<Schooler>().HasData(new Schooler[]
            {
                new Schooler { Id = 1, UserId = 5, ClassName = "11A", SchoolName = "14" }
            });

            modelBuilder.Entity<Scientist>().HasData(new Scientist[]
            {
                new Scientist { Id = 1, UserId = 7, OrganizationName = "что-то", ScientificTheme = "тема" }
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    }
}