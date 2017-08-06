namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CompanyDbContext : DbContext
    {
        public CompanyDbContext()
            : base("name=CompanyDbContext")
        {
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<CompanyInfor> CompanyInfors { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Menu_Sub> Menu_Sub { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Role_Feature> Role_Feature { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInfor>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInfor>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInfor>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Field>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<Partner>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Partner>()
                .Property(e => e.ReferLink)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
