using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PolioMonitoringSystem.Data.Database.Tables;

namespace PolioMonitoringSystem.Data.Database.Tables
{
    public partial class PMSDbContext : DbContext
    {
        public PMSDbContext()
        {
        }

        public PMSDbContext(DbContextOptions<PMSDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppLocations> AppLocations { get; set; }
        public virtual DbSet<AppLocationsUC> AppLocationsUC { get; set; }
        public virtual DbSet<AppLocationsUCBackUp> AppLocationsUCBackUp { get; set; }
        public virtual DbSet<ApplocationsUCCopy> ApplocationsUCCopy { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<DesignationCopy> DesignationCopy { get; set; }
        public virtual DbSet<Divisiondesignationcounter> Divisiondesignationcounter { get; set; }
        public virtual DbSet<FormsIndicator> FormsIndicator { get; set; }
        public virtual DbSet<FormsIndicatorDetails> FormsIndicatorDetails { get; set; }
        public virtual DbSet<FormsIndicatorMaster> FormsIndicatorMaster { get; set; }
        public virtual DbSet<GEOLVL_V> GEOLVL_V { get; set; }
        public virtual DbSet<HFTypes> HFTypes { get; set; }
        public virtual DbSet<HealthCenterType> HealthCenterType { get; set; }
        public virtual DbSet<HealthFacility> HealthFacility { get; set; }
        public virtual DbSet<IndicatorAnswers> IndicatorAnswers { get; set; }
        public virtual DbSet<IndicatorOptions> IndicatorOptions { get; set; }
        public virtual DbSet<IndicatorsView> IndicatorsView { get; set; }
        public virtual DbSet<LogsSM> LogsSM { get; set; }
        public virtual DbSet<MonitoringForms> MonitoringForms { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<PMSEvents> PMSEvents { get; set; }
        public virtual DbSet<Pdesignationcounter> Pdesignationcounter { get; set; }
        public virtual DbSet<Sheet2_> Sheet2_ { get; set; }
        public virtual DbSet<Sheet4_> Sheet4_ { get; set; }
        public virtual DbSet<UC_> UC_ { get; set; }
        public virtual DbSet<UcDesignations> UcDesignations { get; set; }
        public virtual DbSet<UserAssignedDistricts> UserAssignedDistricts { get; set; }
        public virtual DbSet<UserLocations> UserLocations { get; set; }
        public virtual DbSet<V_Userlist> V_Userlist { get; set; }
        public virtual DbSet<districtdesignationcounter> districtdesignationcounter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Initial Catalog=PMS;user id=majid;password=asa@11121;Data Source=172.17.0.450");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppLocations>(entity =>
            {
                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.CategoryCode).HasMaxLength(50);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<AppLocationsUC>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DistrictName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.RecordStatus).HasMaxLength(20);

                entity.Property(e => e.TehsilName).HasMaxLength(50);

                entity.Property(e => e.UCNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<AppLocationsUCBackUp>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DistrictName).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.RecordStatus).HasMaxLength(20);

                entity.Property(e => e.TehsilName).HasMaxLength(50);

                entity.Property(e => e.UCNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<ApplocationsUCCopy>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.RecordStatus).HasMaxLength(20);

                entity.Property(e => e.UCNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreaedDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.GEOLVL).HasMaxLength(50);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.OrganizationId).HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordStatus)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Designation1)
                    .HasColumnName("Designation")
                    .HasMaxLength(500);

                entity.Property(e => e.DesignationLvl).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Designation)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Designation_Organization");
            });

            modelBuilder.Entity<DesignationCopy>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Designation).HasMaxLength(500);

                entity.Property(e => e.DesignationLvl).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Divisiondesignationcounter>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Chief_Executive_Officer__Health_).HasColumnName("Chief Executive Officer (Health)");

                entity.Property(e => e.Deputy_District_Officer_Health).HasColumnName("Deputy District Officer Health");

                entity.Property(e => e.District_Coordinator_IRMNCH).HasColumnName("District Coordinator IRMNCH");

                entity.Property(e => e.District_Officer_Health__MIS___HRM_).HasColumnName("District Officer Health (MIS & HRM)");

                entity.Property(e => e.District_Officer_Health__MS_).HasColumnName("District Officer Health (MS)");

                entity.Property(e => e.District_Officer_Health__Preventive_).HasColumnName("District Officer Health (Preventive)");

                entity.Property(e => e.Division).HasMaxLength(255);

                entity.Property(e => e.Medical_Superintendent).HasColumnName("Medical Superintendent");

                entity.Property(e => e.Office_Superintendent).HasColumnName("Office Superintendent");

                entity.Property(e => e.Program_Director).HasColumnName("Program Director");

                entity.Property(e => e.Program_Director_DHDC).HasColumnName("Program Director DHDC");
            });

            modelBuilder.Entity<FormsIndicator>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IndicatorCategory).HasMaxLength(50);

                entity.Property(e => e.InputType).HasMaxLength(50);

                entity.Property(e => e.SubIndicatorDependency).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.FormsIndicator)
                    .HasForeignKey(d => d.FormId)
                    .HasConstraintName("FK_FormsIndicator_MonitoringForms");
            });

            modelBuilder.Entity<FormsIndicatorDetails>(entity =>
            {
                entity.Property(e => e.Comments).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FormsIndicator)
                    .WithMany(p => p.FormsIndicatorDetails)
                    .HasForeignKey(d => d.FormsIndicatorId)
                    .HasConstraintName("FK_FormsIndicatorDetails_FormsIndicator");
            });

            modelBuilder.Entity<FormsIndicatorMaster>(entity =>
            {
                entity.Property(e => e.AICName).HasMaxLength(50);

                entity.Property(e => e.Area)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Dayofwork).HasMaxLength(50);

                entity.Property(e => e.DesignationId).HasMaxLength(50);

                entity.Property(e => e.DistrictCode).HasMaxLength(50);

                entity.Property(e => e.DivisionCode).HasMaxLength(50);

                entity.Property(e => e.Member1).HasMaxLength(50);

                entity.Property(e => e.Member2).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameofPost)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PopulationType).HasMaxLength(50);

                entity.Property(e => e.SiteName).HasMaxLength(50);

                entity.Property(e => e.SupervisorAgency).HasMaxLength(50);

                entity.Property(e => e.SupervisorName).HasMaxLength(50);

                entity.Property(e => e.Supervisordesignation).HasMaxLength(50);

                entity.Property(e => e.TehsilCode).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.TypeofPost).HasMaxLength(50);

                entity.Property(e => e.UCCode).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Village_Mohallaha_St)
                    .HasColumnName("Village/Mohallaha/St")
                    .HasMaxLength(200);

                entity.Property(e => e.designationofmonitor).HasMaxLength(50);

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.FormsIndicatorMaster)
                    .HasForeignKey(d => d.FormId)
                    .HasConstraintName("FK_FormsIndicatorMaster_MonitoringForms");

                entity.HasOne(d => d.HealthCenter)
                    .WithMany(p => p.FormsIndicatorMaster)
                    .HasForeignKey(d => d.HealthCenterId)
                    .HasConstraintName("FK_FormsIndicatorMaster_HealthCenterType");
            });

            modelBuilder.Entity<GEOLVL_V>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GEOLVL_V");

                entity.Property(e => e.LVL)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.NAME).IsRequired();

                entity.Property(e => e.PKCODE).IsRequired();
            });

            modelBuilder.Entity<HFTypes>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HFTypes");

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.HfCatName).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<HealthCenterType>(entity =>
            {
                entity.Property(e => e.HealthCenterName).HasMaxLength(50);
            });

            modelBuilder.Entity<HealthFacility>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HealthFacility");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Created_Date).HasColumnType("datetime");

                entity.Property(e => e.DistrictName).IsRequired();

                entity.Property(e => e.DivisionName).IsRequired();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FaxNo).HasMaxLength(50);

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.HFCategoryName).IsRequired();

                entity.Property(e => e.HFMISCode).IsRequired();

                entity.Property(e => e.HFTypeName).IsRequired();

                entity.Property(e => e.Mauza).HasMaxLength(50);

                entity.Property(e => e.NA).HasMaxLength(30);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PP).HasMaxLength(30);

                entity.Property(e => e.PhoneNo).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TehsilName).IsRequired();

                entity.Property(e => e.UcName).HasMaxLength(50);

                entity.Property(e => e.UcNo).HasMaxLength(10);

                entity.Property(e => e.Users_Id).HasMaxLength(128);
            });

            modelBuilder.Entity<IndicatorAnswers>(entity =>
            {
                entity.Property(e => e.AnswerDesc)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FormIndicatorDetail)
                    .WithMany(p => p.IndicatorAnswers)
                    .HasForeignKey(d => d.FormIndicatorDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IndicatorAnswers_FormsIndicatorDetails");
            });

            modelBuilder.Entity<IndicatorOptions>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InputType).HasMaxLength(50);

                entity.Property(e => e.Label).HasMaxLength(500);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Indicator)
                    .WithMany(p => p.IndicatorOptions)
                    .HasForeignKey(d => d.IndicatorId)
                    .HasConstraintName("FK_IndicatorOptions_FormsIndicator");
            });

            modelBuilder.Entity<IndicatorsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IndicatorsView");

                entity.Property(e => e.AICName).HasMaxLength(50);

                entity.Property(e => e.AnswerDesc).HasMaxLength(500);

                entity.Property(e => e.Area)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Comments).HasMaxLength(50);

                entity.Property(e => e.Dayofwork).HasMaxLength(50);

                entity.Property(e => e.DesignationId).HasMaxLength(50);

                entity.Property(e => e.DistrictCode).HasMaxLength(50);

                entity.Property(e => e.DivisionCode).HasMaxLength(50);

                entity.Property(e => e.Expr14).HasMaxLength(50);

                entity.Property(e => e.Expr9).HasMaxLength(50);

                entity.Property(e => e.IndicatorCategory).HasMaxLength(50);

                entity.Property(e => e.InputType).HasMaxLength(50);

                entity.Property(e => e.Member1).HasMaxLength(50);

                entity.Property(e => e.Member2).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NameofPost)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PopulationType).HasMaxLength(50);

                entity.Property(e => e.SiteName).HasMaxLength(50);

                entity.Property(e => e.SubIndicatorDependency).HasMaxLength(50);

                entity.Property(e => e.SupervisorAgency).HasMaxLength(50);

                entity.Property(e => e.SupervisorName).HasMaxLength(50);

                entity.Property(e => e.TehsilCode).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.TypeofPost).HasMaxLength(50);

                entity.Property(e => e.UCCode).HasMaxLength(50);

                entity.Property(e => e.Village_Mohallaha_St)
                    .HasColumnName("Village/Mohallaha/St")
                    .HasMaxLength(200);

                entity.Property(e => e.designationofmonitor).HasMaxLength(50);
            });

            modelBuilder.Entity<LogsSM>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.Mask).HasMaxLength(50);

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.MessageId).IsRequired();

                entity.Property(e => e.Number).IsRequired();

                entity.Property(e => e.Remarks).HasMaxLength(1000);

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MonitoringForms>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.FormName).HasMaxLength(500);

                entity.Property(e => e.UpdatedBy).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDae).HasColumnType("datetime");
            });

            modelBuilder.Entity<PMSEvents>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Pdesignationcounter>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Designation).HasMaxLength(255);

                entity.Property(e => e.Sr_).HasColumnName("Sr#");
            });

            modelBuilder.Entity<Sheet2_>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Sheet2$");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.UCNumber).HasMaxLength(255);
            });

            modelBuilder.Entity<Sheet4_>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Sheet4$");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.UCNumber).HasMaxLength(255);
            });

            modelBuilder.Entity<UC_>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UC$");

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.DistrictName).HasMaxLength(255);

                entity.Property(e => e.TehsilName).HasMaxLength(255);

                entity.Property(e => e.UCNumber).HasMaxLength(255);
            });

            modelBuilder.Entity<UcDesignations>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.DistrictName).HasMaxLength(255);

                entity.Property(e => e.TehsilName).HasMaxLength(255);

                entity.Property(e => e.UCNumber).HasMaxLength(255);
            });

            modelBuilder.Entity<UserAssignedDistricts>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UserAssignedDistricts");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Geolvl).IsRequired();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<UserLocations>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Geolvl).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLocations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLocations_AspNetUsers");
            });

            modelBuilder.Entity<V_Userlist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Userlist");

                entity.Property(e => e.District_Name).HasColumnName("District Name");

                entity.Property(e => e.Division_Name).HasColumnName("Division Name");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.GEOLVL).HasMaxLength(50);

                entity.Property(e => e.Health_Facility_Name).HasColumnName("Health Facility Name");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Tehsil_Name).HasColumnName("Tehsil Name");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<districtdesignationcounter>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Chief_Executive_Officer__Health_).HasColumnName("Chief Executive Officer (Health)");

                entity.Property(e => e.Deputy_District_Officer_Health).HasColumnName("Deputy District Officer Health");

                entity.Property(e => e.District).HasMaxLength(255);

                entity.Property(e => e.District_Coordinator_IRMNCH).HasColumnName("District Coordinator IRMNCH");

                entity.Property(e => e.District_Officer_Health__MIS___HRM_).HasColumnName("District Officer Health (MIS & HRM)");

                entity.Property(e => e.District_Officer_Health__MS_).HasColumnName("District Officer Health (MS)");

                entity.Property(e => e.District_Officer_Health__Preventive_).HasColumnName("District Officer Health (Preventive)");

                entity.Property(e => e.Medical_Superintendent).HasColumnName("Medical Superintendent");

                entity.Property(e => e.Program_Director).HasColumnName("Program Director");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
