using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eGreetings.Models;

public partial class EGreetingsContext : DbContext
{
    public EGreetingsContext()
    {
    }

    public EGreetingsContext(DbContextOptions<EGreetingsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<CardDesign> CardDesigns { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DesignElement> DesignElements { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SystemLog> SystemLogs { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.;initial catalog=E_greetings;user id=sa;password=aptech; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__admins__43AA414141690D1A");

            entity.ToTable("admins");

            entity.HasIndex(e => e.Email, "UQ__admins__AB6E616427DD7F85").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__admins__F3DBC572E1F15B9C").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<CardDesign>(entity =>
        {
            entity.HasKey(e => e.DesignId).HasName("PK__card_des__1BA5C3FB0E282DC9");

            entity.ToTable("card_designs");

            entity.Property(e => e.DesignId).HasColumnName("design_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DesignImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("design_image_url");
            entity.Property(e => e.DesignName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("design_name");
            entity.Property(e => e.TemplateId).HasColumnName("template_id");

            entity.HasOne(d => d.Template).WithMany(p => p.CardDesigns)
                .HasForeignKey(d => d.TemplateId)
                .HasConstraintName("FK__card_desi__templ__3E52440B");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__D54EE9B418387999");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category_name");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
        });

        modelBuilder.Entity<DesignElement>(entity =>
        {
            entity.HasKey(e => e.ElementId).HasName("PK__design_e__388489FBE49536FB");

            entity.ToTable("design_elements");

            entity.Property(e => e.ElementId).HasColumnName("element_id");
            entity.Property(e => e.ContentUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("content_url");
            entity.Property(e => e.DesignId).HasColumnName("design_id");
            entity.Property(e => e.ElementType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("element_type");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.PositionX).HasColumnName("position_x");
            entity.Property(e => e.PositionY).HasColumnName("position_y");
            entity.Property(e => e.TextContent)
                .HasColumnType("text")
                .HasColumnName("text_content");
            entity.Property(e => e.Width).HasColumnName("width");

            entity.HasOne(d => d.Design).WithMany(p => p.DesignElements)
                .HasForeignKey(d => d.DesignId)
                .HasConstraintName("FK__design_el__desig__412EB0B6");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__feedback__7A6B2B8C9D07CABD");

            entity.ToTable("feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.FeedbackDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("feedback_date");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__feedback__user_i__36B12243");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__notifica__E059842F5A38DADB");

            entity.ToTable("notifications");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.NotificationType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("notification_type");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('unread')")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__notificat__user___49C3F6B7");
        });

        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__payment___ED1FC9EADF910BD6");

            entity.ToTable("payment_transactions");

            entity.HasIndex(e => e.TransactionId, "UQ__payment___85C600AEC9F57BCA").IsUnique();

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('pending')")
                .HasColumnName("payment_status");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("transaction_id");

            entity.HasOne(d => d.Subscription).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("FK__payment_t__subsc__44FF419A");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__reports__779B7C58ED2A6917");

            entity.ToTable("reports");

            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.ReportDate)
                .HasColumnType("date")
                .HasColumnName("report_date");
            entity.Property(e => e.TotalRevenue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_revenue");
            entity.Property(e => e.TotalTransactions).HasColumnName("total_transactions");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__subscrip__863A7EC1AC0F73EA");

            entity.ToTable("subscriptions");

            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('active')")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__subscript__user___32E0915F");
        });

        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__system_l__9E2397E018D34B81");

            entity.ToTable("system_logs");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.LogDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("log_date");
            entity.Property(e => e.LogLevel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("log_level");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.SystemLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__system_lo__user___4F7CD00D");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__tags__4296A2B61C489102");

            entity.ToTable("tags");

            entity.HasIndex(e => e.TagName, "UQ__tags__E298655C2F9B6EAA").IsUnique();

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.TagName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tag_name");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.TemplateId).HasName("PK__template__BE44E079DEA37EF9");

            entity.ToTable("templates");

            entity.Property(e => e.TemplateId).HasColumnName("template_id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.TemplateName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("template_name");

            entity.HasMany(d => d.Tags).WithMany(p => p.Templates)
                .UsingEntity<Dictionary<string, object>>(
                    "TemplateTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__template___tag_i__5629CD9C"),
                    l => l.HasOne<Template>().WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__template___templ__5535A963"),
                    j =>
                    {
                        j.HasKey("TemplateId", "TagId").HasName("PK__template__CA6D8A52CE8EA78C");
                        j.ToTable("template_tags");
                        j.IndexerProperty<int>("TemplateId").HasColumnName("template_id");
                        j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                    });
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__transact__85C600AF8EF45962");

            entity.ToTable("transactions");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.RecipientEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("recipient_email");
            entity.Property(e => e.SendDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("send_date");
            entity.Property(e => e.TemplateId).HasColumnName("template_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Template).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TemplateId)
                .HasConstraintName("FK__transacti__templ__2F10007B");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__transacti__user___2E1BDC42");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F97C6B4D6");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E61642D18916B").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC57245782587").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("registration_date");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRoleAssignment",
                    r => r.HasOne<UserRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__user_role__role___5CD6CB2B"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__user_role__user___5BE2A6F2"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__user_rol__6EDEA1530A121DE3");
                        j.ToTable("user_role_assignments");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__user_rol__760965CCD486A95E");

            entity.ToTable("user_roles");

            entity.HasIndex(e => e.RoleName, "UQ__user_rol__783254B1C647FC54").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
