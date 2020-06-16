using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CodeFirstCore
{
    /// <summary>
    /// 1. ստեղծել մոդելները, հիշել որ 1-շատի հարաբերության համար օգտագործվում է virtual ICollection<Nmae> name --- virtual Nmae name
    /// 2. ավելացնել EntityFrameworkCore փաթեթը և ավելացնել using Microsoft.EntityFrameworkCore;
    /// 3․ ավելացնել BloggingContext կլասը DbSet էր ով և override void OnConfiguring մեթոդը կրկին ավելացնելով Microsoft.EntityFrameworkCore.SqlServer փաթեթը
    /// 5․ db.Blogs.Add(name); db.SaveChanges(); - ավելացնել
    /// 6․ query ով կարդալ
    /// 7․ մոդելում և բազայում փոփոխոթյունների համար Package Manager Console ում գրել Add-Migration Initial թարմացնել Update-Database հրմանով
    /// </summary>
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb; Initial Catalog=CodeFirstCoreDB");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Create and save a new Blog
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };
                db.Blogs.Add(blog);
                db.SaveChanges();

                // Display all Blogs from the database
                var query = from b in db.Blogs
                            orderby b.Name
                            select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
