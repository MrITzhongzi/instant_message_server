using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication2.Models
{
    public partial class instant_messageContext : DbContext
    {
        public instant_messageContext()
        {
        }

        public instant_messageContext(DbContextOptions<instant_messageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MessageList> MessageList { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;uid=root;pwd=;database=instant_message");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageList>(entity =>
            {
                entity.ToTable("message_list");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.MsgContent)
                    .IsRequired()
                    .HasColumnName("msg_content")
                    .HasColumnType("longtext");

                entity.Property(e => e.MsgTime)
                    .HasColumnName("msg_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("text");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Tel)
                    .HasColumnName("tel")
                    .HasColumnType("int(255)");
            });
        }
    }
}
