using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeFirst
{
    /// <summary>
    /// 1. ստեղծել մոդելները, հիշել որ 1-շատի հարաբերության համար օգտագործվում է virtual ICollection<Nmae> name --- virtual Nmae name
    /// 2. ավելացնել EntityFramework փաթեթը և ավելացնել using System.Data.Entity;
    /// 3․ ավելացնել BloggingContext կլասը DbSet էր ով
    /// 4․ db.Blogs.Add(name); db.SaveChanges(); - ավելացնել
    /// 5․ query ով կարդալ
    /// 6․ մոդելում և բազայում փոփոխոթյունների համար Package Manager Console ում գրել Enable-Migrations  ստեղծելով Code First Migrations
    /// 7․ փոփոխությունն անելուց հետո Package Manager Console ում գրել Add-Migration AddUrl այնուհետև թարմացնել Update-Database հրմանով
    /// 
    /// </summary>


    public class User
    {
        [Key]
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

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
        public DbSet<User> Users { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                ////// Create and save a new Blog
                ////Console.Write("Enter a name for a new Blog: ");
                ////var name = Console.ReadLine();

                ////var blog = new Blog { Name = name };
                ////db.Blogs.Add(blog);
                ////db.SaveChanges();

                // Display all Blogs from the database
                var query = from b in db.Blogs
                            orderby b.Name
                            select b;
                var q2 = db.Blogs
                           .OrderBy(s => s.Name);

                Console.WriteLine("All blogs in the database:");
                foreach (var item in q2)
                {
                    Console.WriteLine(item.Name);
                }

                foreach (var item in q2)
                {
                    XElement element = new XElement("Blogs",
                        new XAttribute("Name", item.Name),
                        new XAttribute("Posts", item.Posts));
                    Console.WriteLine(element);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
