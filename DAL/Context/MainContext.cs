using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using DAL.Entities;
using myBLOGData.Context;
namespace DAL.Context
{
    public class MainContext : DbContext
    {

        public MainContext() : base("name=MainContext") { }
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Slider> Slider { get; set; }


        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<Analytic> Analytic { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<GalleryGroup> GalleryGroup { get; set; }
        public DbSet<Institutional> Institutional { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<References> References { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<DocumentGroup> DocumentGroup { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<ProductSubGroup> ProductSubGroup { get; set; }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<HumanResource> HumanResource { get; set; }
        public DbSet<Teklif> Teklif { get; set; }
        public DbSet<MailSetting> MailSetting { get; set; }
        public DbSet<MailUsers> MailUsers { get; set; }
        public DbSet<SolutionPartner> SolutionPartner { get; set; }
        public DbSet<HumanResourcePosition> HumanResourcePosition { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceGroup> ServiceGroup { get; set; }
        public DbSet<OurServices> OurServices { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<SectorGroup> SectorGroup { get; set; }
        public DbSet<OurSectors> OurSectors { get; set; }
        public DbSet<Log4Net_Error> Log4Net_Error { get; set; }
        public DbSet<ProdCategory> ProdCategory { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<OurTeam> OurTeam { get; set; }
        public DbSet<Estate> Estate { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Town> Town { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<ProductDetail> ProductDetail { get; set; }
        public DbSet<HumanResourceCv> HumanResourceCv { get; set; }
        public DbSet<ContactHome> ContactHome { get; set; }
        public DbSet<ProductHeaders> ProductHeaders { get; set; }

        public DbSet<ProductInformation> ProductInformation { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer(new DatabaseCreatorClass());
            //Database.SetInitializer(new DropCreateDatabaseAlways<MainContext>());
                 
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainContext, Configration>());


            modelBuilder.Entity<AdminUser>().ToTable("AdminUser");
            modelBuilder.Entity<Gallery>().ToTable("Gallery");
            modelBuilder.Entity<GalleryGroup>().ToTable("GalleryGroup");
            modelBuilder.Entity<Institutional>().ToTable("Institutional");
            modelBuilder.Entity<Languages>().ToTable("Languages");
            modelBuilder.Entity<News>().ToTable("News");
            modelBuilder.Entity<References>().ToTable("References");
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<DocumentGroup>().ToTable("DocumentGroup");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<ProductSubGroup>().ToTable("ProductSubGroup");
            modelBuilder.Entity<ProductGroup>().ToTable("ProductGroup");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<HumanResource>().ToTable("HumanResource");
            modelBuilder.Entity<Teklif>().ToTable("Teklif");
            modelBuilder.Entity<MailUsers>().ToTable("MailUsers");
            modelBuilder.Entity<MailSetting>().ToTable("MailSetting");
            modelBuilder.Entity<SolutionPartner>().ToTable("SolutionPartner");
            modelBuilder.Entity<HumanResourcePosition>().ToTable("HumanResourcePosition");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<ServiceGroup>().ToTable("ServiceGroup");
            modelBuilder.Entity<OurServices>().ToTable("OurServices");
            modelBuilder.Entity<Photo>().ToTable("Photo");
            modelBuilder.Entity<Sector>().ToTable("Sector");
            modelBuilder.Entity<SectorGroup>().ToTable("SectorGroup");
            modelBuilder.Entity<OurSectors>().ToTable("OurSectors");
            modelBuilder.Entity<Log4Net_Error>().ToTable("Log4Net_Error");
            modelBuilder.Entity<ProdCategory>().ToTable("ProdCategory");
            modelBuilder.Entity<Page>().ToTable("Page");
            modelBuilder.Entity<OurTeam>().ToTable("OurTeam");
            modelBuilder.Entity<Analytic>().ToTable("Analytic");
            modelBuilder.Entity<SocialMedia>().ToTable("SocialMedia");

            
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Town>().ToTable("Town");
            modelBuilder.Entity<District>().ToTable("District");
            modelBuilder.Entity<Estate>().ToTable("Estate");
            modelBuilder.Entity<Tags>().ToTable("Tags");

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Slider>().ToTable("Slider");
            modelBuilder.Entity<ProductDetail>().ToTable("ProductDetail");

            modelBuilder.Entity<HumanResourceCv>().ToTable("HumanResourceCv");
            modelBuilder.Entity<ContactHome>().ToTable("ContactHome");
            modelBuilder.Entity<ProductHeaders>().ToTable("ProductHeaders");
            modelBuilder.Entity<ProductInformation>().ToTable("ProductInformation");
            modelBuilder.Entity<ProductColors>().ToTable("ProductColors"); 
            
            
        }
    }
}