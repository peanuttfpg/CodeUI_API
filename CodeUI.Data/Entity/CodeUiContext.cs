using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CodeUI.Data.Entity;

public partial class CodeUiContext : DbContext
{
    public CodeUiContext()
    {
    }

    public CodeUiContext(DbContextOptions<CodeUiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountChat> AccountChats { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChatBox> ChatBoxes { get; set; }

    public virtual DbSet<Collaboration> Collaborations { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Element> Elements { get; set; }

    public virtual DbSet<Follow> Follows { get; set; }

    public virtual DbSet<LikeTable> LikeTables { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Snippet> Snippets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=54.169.231.3;Database=CodeUI;User ID=sa;Password=QuanNM_0516;MultipleActiveResultSets=true;Integrated Security=true;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("date");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Account_Role");
        });

        modelBuilder.Entity<AccountChat>(entity =>
        {
            entity.ToTable("Account_Chat");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountChats)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Account_Chat_Account");

            entity.HasOne(d => d.ChatBox).WithMany(p => p.AccountChats)
                .HasForeignKey(d => d.ChatBoxId)
                .HasConstraintName("FK_Account_Chat_ChatBox");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<ChatBox>(entity =>
        {
            entity.ToTable("ChatBox");

            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.LastActivityDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Collaboration>(entity =>
        {
            entity.ToTable("Collaboration");

            entity.HasOne(d => d.Account).WithMany(p => p.Collaborations)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Collaboration_Account");

            entity.HasOne(d => d.Element).WithMany(p => p.Collaborations)
                .HasForeignKey(d => d.ElementId)
                .HasConstraintName("FK_Collaboration_Element");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.Content).HasMaxLength(150);
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Account).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Comment_Account");

            entity.HasOne(d => d.Element).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ElementId)
                .HasConstraintName("FK_Comment_Element");
        });

        modelBuilder.Entity<Element>(entity =>
        {
            entity.ToTable("Element");

            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Target).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(15);
            entity.Property(e => e.UpdateDate).HasColumnType("date");

            entity.HasOne(d => d.Category).WithMany(p => p.Elements)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Element_Category");
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.ToTable("Follow");

            entity.HasOne(d => d.Follower).WithMany(p => p.FollowFollowers)
                .HasForeignKey(d => d.FollowerId)
                .HasConstraintName("FK_Follow_Account");

            entity.HasOne(d => d.Following).WithMany(p => p.FollowFollowings)
                .HasForeignKey(d => d.FollowingId)
                .HasConstraintName("FK_Follow_Account1");
        });

        modelBuilder.Entity<LikeTable>(entity =>
        {
            entity.ToTable("LikeTable");

            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Account).WithMany(p => p.LikeTables)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_LikeTable_Account");

            entity.HasOne(d => d.Element).WithMany(p => p.LikeTables)
                .HasForeignKey(d => d.ElementId)
                .HasConstraintName("FK_LikeTable_Element");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Message");

            entity.Property(e => e.Content).HasMaxLength(200);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Account).WithMany()
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Message_Account");

            entity.HasOne(d => d.ChatBox).WithMany()
                .HasForeignKey(d => d.ChatBoxId)
                .HasConstraintName("FK_Message_ChatBox");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notification");

            entity.Property(e => e.Data).HasColumnType("text");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Type).HasMaxLength(30);

            entity.HasOne(d => d.Account).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Notification_Account");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.ToTable("Profile");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.Gender).HasMaxLength(15);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Wallet).HasColumnType("money");

        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.ToTable("Rating");

            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Account).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Rating_Account");

            entity.HasOne(d => d.Element).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ElementId)
                .HasConstraintName("FK_Rating_Element");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.ToTable("Report");

            entity.Property(e => e.Content).HasMaxLength(150);
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany(p => p.Reports)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Report_Account");

            entity.HasOne(d => d.Element).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ElementId)
                .HasConstraintName("FK_Report_Element");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable("Request");

            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Request_Category");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.RequestCreateByNavigations)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK_Request_Account");

            entity.HasOne(d => d.ReceiveByNavigation).WithMany(p => p.RequestReceiveByNavigations)
                .HasForeignKey(d => d.ReceiveBy)
                .HasConstraintName("FK_Request_Account1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Snippet>(entity =>
        {
            entity.ToTable("Snippet");

            entity.Property(e => e.Csscode).HasColumnName("CSSCode");
            entity.Property(e => e.Htmlcode).HasColumnName("HTMLCode");
            entity.Property(e => e.Jscode).HasColumnName("JSCode");

            entity.HasOne(d => d.Element).WithMany(p => p.Snippets)
                .HasForeignKey(d => d.ElementId)
                .HasConstraintName("FK_Snippet_Element");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
