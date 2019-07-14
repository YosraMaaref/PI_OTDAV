namespace PI_OTDAV_Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using PI_OTDAV_Domain.Entities;

    public partial class Context : DbContext
    {
        public Context() : base("name=Context")
        {
        }
      //  public virtual DbSet<repartition> repartition { get; set; }
        public virtual DbSet<artworkcategory> artworkcategory { get; set; }
        public virtual DbSet<bankcard> bankcard { get; set; }
        public virtual DbSet<cheque> cheque { get; set; }
        public virtual DbSet<espace> espace { get; set; }
        public virtual DbSet<formuleperception> formuleperception { get; set; }
        public virtual DbSet<location> location { get; set; }
        public virtual DbSet<membershipcategory> membershipcategory { get; set; }
        public virtual DbSet<notification> notification { get; set; }
        public virtual DbSet<oeuvredeclaration> oeuvredeclaration { get; set; }
        public virtual DbSet<oeuvredeposant> oeuvredeposant { get; set; }
        public virtual DbSet<paiment> paiment { get; set; }
        public virtual DbSet<perceptioncategory> perceptioncategory { get; set; }
        public virtual DbSet<perciption> perciption { get; set; }
        public virtual DbSet<programme> programme { get; set; }
        public virtual DbSet<reclamation> reclamation { get; set; }
        public virtual DbSet<repartitioncategory> repartitioncategory { get; set; }
        public virtual DbSet<reponsereclamation> reponsereclamation { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<virement> virement { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<artworkcategory>()
                .Property(e => e.details)
                .IsUnicode(false);

            modelBuilder.Entity<artworkcategory>()
                .Property(e => e.nom)
                .IsUnicode(false);

            modelBuilder.Entity<artworkcategory>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<bankcard>()
                .HasMany(e => e.paiment)
                .WithOptional(e => e.bankcard)
                .HasForeignKey(e => e.bankcard_IdBank);

            modelBuilder.Entity<cheque>()
                .Property(e => e.Price)
                .IsUnicode(false);

            modelBuilder.Entity<cheque>()
                .Property(e => e.bank)
                .IsUnicode(false);

            modelBuilder.Entity<cheque>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<cheque>()
                .HasMany(e => e.paiment)
                .WithOptional(e => e.cheque)
                .HasForeignKey(e => e.cheque_IdCheque);

            modelBuilder.Entity<espace>()
                .Property(e => e.NomEspace)
                .IsUnicode(false);

            modelBuilder.Entity<espace>()
                .Property(e => e.adresse)
                .IsUnicode(false);

            modelBuilder.Entity<espace>()
                .Property(e => e.details)
                .IsUnicode(false);

            modelBuilder.Entity<espace>()
                .HasMany(e => e.perceptioncategory)
                .WithOptional(e => e.espace)
                .HasForeignKey(e => e.espace_idEspace);

            modelBuilder.Entity<formuleperception>()
                .HasMany(e => e.espace)
                .WithOptional(e => e.formuleperception)
                .HasForeignKey(e => e.formulePerception_idFormuleP);

            modelBuilder.Entity<location>()
                .Property(e => e.Armoire)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.Casier)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.Departement)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.Remarque)
                .IsUnicode(false);

            modelBuilder.Entity<location>()
                .Property(e => e.Salle)
                .IsUnicode(false);

            modelBuilder.Entity<membershipcategory>()
                .Property(e => e.details)
                .IsUnicode(false);

            modelBuilder.Entity<membershipcategory>()
                .Property(e => e.nom)
                .IsUnicode(false);

            modelBuilder.Entity<membershipcategory>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<membershipcategory>()
                .HasMany(e => e.user)
                .WithOptional(e => e.membershipcategory)
                .HasForeignKey(e => e.meberMemberShipCategory_id);

            modelBuilder.Entity<notification>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<notification>()
                .Property(e => e.etat)
                .IsUnicode(false);

            modelBuilder.Entity<notification>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<oeuvredeclaration>()
                .Property(e => e.Categorie)
                .IsUnicode(false);

            modelBuilder.Entity<oeuvredeclaration>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<oeuvredeclaration>()
                .Property(e => e.Support)
                .IsUnicode(false);

            modelBuilder.Entity<oeuvredeclaration>()
                .Property(e => e.Titre)
                .IsUnicode(false);

            modelBuilder.Entity<oeuvredeclaration>()
                .Property(e => e.etat)
                .IsUnicode(false);

            modelBuilder.Entity<oeuvredeclaration>()
                .Property(e => e.path)
                .IsUnicode(false);

            modelBuilder.Entity<oeuvredeclaration>()
                .HasMany(e => e.location)
                .WithOptional(e => e.oeuvredeclaration)
                .HasForeignKey(e => e.OeuvreD_id);

            modelBuilder.Entity<oeuvredeclaration>()
                .HasMany(e => e.perciption)
                .WithOptional(e => e.oeuvredeclaration)
                .HasForeignKey(e => e.OeuvreD_id);

            modelBuilder.Entity<oeuvredeclaration>()
                .HasMany(e => e.paiment)
                .WithOptional(e => e.oeuvredeclaration)
                .HasForeignKey(e => e.oeuvreDec_id);

            modelBuilder.Entity<oeuvredeposant>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<oeuvredeposant>()
                .Property(e => e.etat_depot)
                .IsUnicode(false);

            modelBuilder.Entity<oeuvredeposant>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<oeuvredeposant>()
                .Property(e => e.titre)
                .IsUnicode(false);

            modelBuilder.Entity<paiment>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<perceptioncategory>()
                .Property(e => e.NomCategory)
                .IsUnicode(false);

            modelBuilder.Entity<perceptioncategory>()
                .Property(e => e.PSC)
                .IsUnicode(false);

            modelBuilder.Entity<perceptioncategory>()
                .Property(e => e.SCT)
                .IsUnicode(false);

            modelBuilder.Entity<perceptioncategory>()
                .Property(e => e.detailsCategory)
                .IsUnicode(false);

            modelBuilder.Entity<programme>()
                .Property(e => e.titre)
                .IsUnicode(false);

            modelBuilder.Entity<programme>()
                .Property(e => e.titre_exploi)
                .IsUnicode(false);

            modelBuilder.Entity<reclamation>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<reclamation>()
                .Property(e => e.etat)
                .IsUnicode(false);

            modelBuilder.Entity<reclamation>()
                .Property(e => e.fichier_a_joindre)
                .IsUnicode(false);

            modelBuilder.Entity<reclamation>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<reponsereclamation>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.CIN)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.CommercialRegisterNumber)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.Gouverment)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.mail)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.notifications)
                .WithOptional(e => e.destination)
                .HasForeignKey(e => e.idDestination);

            modelBuilder.Entity<user>()
                .HasMany(e => e.notifications)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.idUser);

            modelBuilder.Entity<user>()
                .HasMany(e => e.oeuvredeclarations)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_ID);

            modelBuilder.Entity<virement>()
                .Property(e => e.bank)
                .IsUnicode(false);

            modelBuilder.Entity<virement>()
                .Property(e => e.codeVirement)
                .IsUnicode(false);

            modelBuilder.Entity<virement>()
                .HasMany(e => e.paiment)
                .WithOptional(e => e.virement)
                .HasForeignKey(e => e.virement_IdVirement);

        }
    }
}
