using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EasyLeasesDB.Models
{
    public partial class EasyLeasesDBContext : DbContext
    {
        public EasyLeasesDBContext()
        {
        }

        public EasyLeasesDBContext(DbContextOptions<EasyLeasesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Homeowners> Homeowners { get; set; }
        public virtual DbSet<Leases> Leases { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<Realtors> Realtors { get; set; }
        public virtual DbSet<Renters> Renters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=PF-102KOR;Initial Catalog=EasyLeasesDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Homeowners>(entity =>
            {
                entity.Property(e => e.HomeownersId).HasColumnName("homeowners_id");

                entity.Property(e => e.FkRealtorsId).HasColumnName("fk_realtors_id");

                entity.Property(e => e.HomeownerAddress)
                    .HasColumnName("homeowner_address")
                    .HasMaxLength(50);

                entity.Property(e => e.HomeownerCity)
                    .HasColumnName("homeowner_city")
                    .HasMaxLength(30);

                entity.Property(e => e.HomeownerEmail)
                    .HasColumnName("homeowner_email")
                    .HasMaxLength(50);

                entity.Property(e => e.HomeownerName)
                    .HasColumnName("homeowner_name")
                    .HasMaxLength(40);

                entity.Property(e => e.HomeownerPassword)
                    .HasColumnName("homeowner_password")
                    .HasMaxLength(20);

                entity.Property(e => e.HomeownerPhoneNumber)
                    .HasColumnName("homeowner_phone_number")
                    .HasMaxLength(15);

                entity.Property(e => e.HomeownerState)
                    .HasColumnName("homeowner_state")
                    .HasMaxLength(2);

                entity.Property(e => e.HomeownerUserName)
                    .HasColumnName("homeowner_user_name")
                    .HasMaxLength(20);

                entity.Property(e => e.HomeownerZip)
                    .HasColumnName("homeowner_zip")
                    .HasMaxLength(5);

                entity.Property(e => e.IsRepresented).HasColumnName("is_represented");

                entity.HasOne(d => d.FkRealtors)
                    .WithMany(p => p.Homeowners)
                    .HasForeignKey(d => d.FkRealtorsId)
                    .HasConstraintName("FK_Homeowners_Realtors");
            });

            modelBuilder.Entity<Leases>(entity =>
            {
                entity.Property(e => e.LeasesId).HasColumnName("leases_id");

                entity.Property(e => e.FkHomeownersId).HasColumnName("fk_homeowners_id");

                entity.Property(e => e.FkPropertiesId).HasColumnName("fk_properties_id");

                entity.Property(e => e.FkRealtorsId).HasColumnName("fk_realtors_id");

                entity.Property(e => e.FkRentersId).HasColumnName("fk_renters_id");

                entity.Property(e => e.LeaseAmount)
                    .HasColumnName("lease_amount")
                    .HasColumnType("smallmoney");

                entity.Property(e => e.LeaseEndDate)
                    .HasColumnName("lease_end_date")
                    .HasColumnType("date");

                entity.Property(e => e.LeaseNumber)
                    .HasColumnName("lease_number")
                    .HasMaxLength(15);

                entity.Property(e => e.LeaseStartDate)
                    .HasColumnName("lease_start_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.FkHomeowners)
                    .WithMany(p => p.Leases)
                    .HasForeignKey(d => d.FkHomeownersId)
                    .HasConstraintName("FK_Leases_Homeowners");

                entity.HasOne(d => d.FkProperties)
                    .WithMany(p => p.Leases)
                    .HasForeignKey(d => d.FkPropertiesId)
                    .HasConstraintName("FK_Leases_Properties");

                entity.HasOne(d => d.FkRealtors)
                    .WithMany(p => p.Leases)
                    .HasForeignKey(d => d.FkRealtorsId)
                    .HasConstraintName("FK_Leases_Realtors");

                entity.HasOne(d => d.FkRenters)
                    .WithMany(p => p.Leases)
                    .HasForeignKey(d => d.FkRentersId)
                    .HasConstraintName("FK_Leases_Renters");
            });

            modelBuilder.Entity<Properties>(entity =>
            {
                entity.Property(e => e.PropertiesId)
                    .HasColumnName("properties_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AvailableDate)
                    .HasColumnName("available_date")
                    .HasColumnType("date");

                entity.Property(e => e.FkHomeownersId).HasColumnName("fk_homeowners_id");

                entity.Property(e => e.IsLeased).HasColumnName("is_leased");

                entity.Property(e => e.PropertyAddress)
                    .HasColumnName("property_address")
                    .HasMaxLength(50);

                entity.Property(e => e.PropertyBaths).HasColumnName("property_baths");

                entity.Property(e => e.PropertyBeds).HasColumnName("property_beds");

                entity.Property(e => e.PropertyCity)
                    .HasColumnName("property_city")
                    .HasMaxLength(30);

                entity.Property(e => e.PropertyFloors).HasColumnName("property_floors");

                entity.Property(e => e.PropertyLivingSqFt).HasColumnName("property_living_sq_ft");

                entity.Property(e => e.PropertyRentAmount)
                    .HasColumnName("property_rent_amount")
                    .HasColumnType("smallmoney");

                entity.Property(e => e.PropertyState)
                    .HasColumnName("property_state")
                    .HasMaxLength(2);

                entity.Property(e => e.PropertyZip)
                    .HasColumnName("property_zip")
                    .HasMaxLength(5);

                entity.HasOne(d => d.FkHomeowners)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.FkHomeownersId)
                    .HasConstraintName("FK_Properties_Homeowners");
            });

            modelBuilder.Entity<Realtors>(entity =>
            {
                entity.Property(e => e.RealtorsId).HasColumnName("realtors_id");

                entity.Property(e => e.CommissionRate)
                    .HasColumnName("commission_rate")
                    .HasColumnType("smallmoney");

                entity.Property(e => e.RealtorCompany)
                    .HasColumnName("realtor_company")
                    .HasMaxLength(30);

                entity.Property(e => e.RealtorName)
                    .HasColumnName("realtor_name")
                    .HasMaxLength(40);

                entity.Property(e => e.RealtorPassword)
                    .HasColumnName("realtor_password")
                    .HasMaxLength(20);

                entity.Property(e => e.RealtorPhoneNumber)
                    .HasColumnName("realtor_phone_number")
                    .HasMaxLength(15);

                entity.Property(e => e.RealtorReview)
                    .HasColumnName("realtor_review")
                    .HasMaxLength(200);

                entity.Property(e => e.RealtorUserName)
                    .HasColumnName("realtor_user_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Renters>(entity =>
            {
                entity.Property(e => e.RentersId).HasColumnName("renters_id");

                entity.Property(e => e.FkPropertiesId).HasColumnName("fk_properties_id");

                entity.Property(e => e.FkRealtorsId).HasColumnName("fk_realtors_id");

                entity.Property(e => e.RenterEmail)
                    .HasColumnName("renter_email")
                    .HasMaxLength(50);

                entity.Property(e => e.RenterGender)
                    .HasColumnName("renter_gender")
                    .HasMaxLength(10);

                entity.Property(e => e.RenterMonthlySalary)
                    .HasColumnName("renter_monthly_salary")
                    .HasColumnType("smallmoney");

                entity.Property(e => e.RenterName)
                    .HasColumnName("renter_name")
                    .HasMaxLength(40);

                entity.Property(e => e.RenterPassword)
                    .HasColumnName("renter_password")
                    .HasMaxLength(20);

                entity.Property(e => e.RenterPhoneNumber)
                    .HasColumnName("renter_phone_number")
                    .HasMaxLength(15);

                entity.Property(e => e.RenterSsn)
                    .HasColumnName("renter_ssn")
                    .HasMaxLength(15);

                entity.Property(e => e.RenterUserName)
                    .HasColumnName("renter_user_name")
                    .HasMaxLength(20);

                entity.HasOne(d => d.FkProperties)
                    .WithMany(p => p.Renters)
                    .HasForeignKey(d => d.FkPropertiesId)
                    .HasConstraintName("FK_Renters_Properties");

                entity.HasOne(d => d.FkRealtors)
                    .WithMany(p => p.Renters)
                    .HasForeignKey(d => d.FkRealtorsId)
                    .HasConstraintName("FK_Renters_Realtors");
            });
        }
    }
}
