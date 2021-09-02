using System;
using System.Configuration;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess
{
    public partial class PublishContext : DbContext
    {
        public virtual DbSet<ArticleOld> OldArticle { get; set; }
        public virtual DbSet<InfoOld> OldInfo { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleTag> ArticleTags { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Text> Texts { get; set; }
        public PublishContext()
        {
        }

        public PublishContext(DbContextOptions<PublishContext> options)
            : base(options)
        {
        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("articles_pk")
                    .IsClustered(false);

                entity.ToTable("articles", "dbadmin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAuthor).HasColumnName("id_author");

                entity.Property(e => e.Name)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PublishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("publish_date");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.IdAuthor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("articles_authors_id_fk");
            });

            modelBuilder.Entity<ArticleTag>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("article_tags_pk")
                    .IsClustered(false);

                entity.ToTable("article_tags", "dbadmin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdArticle).HasColumnName("id_article");

                entity.Property(e => e.IdTag).HasColumnName("id_tag");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleTags)
                    .HasForeignKey(d => d.IdArticle)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("article_tags_articles_id_fk");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ArticleTags)
                    .HasForeignKey(d => d.IdTag)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("article_tags_tags_id_fk");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("authors_pk")
                    .IsClustered(false);

                entity.ToTable("authors", "dbadmin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime")
                    .HasColumnName("join_date");

                entity.Property(e => e.Name)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("tags_pk")
                    .IsClustered(false);

                entity.ToTable("tags", "dbadmin");

                entity.HasComment("desc tags for articles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Text>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("texts", "dbadmin");

                entity.HasIndex(e => e.IdArticle, "texts_id_article_uindex")
                    .IsUnique();

                entity.Property(e => e.AdultOnly)
                    .HasColumnName("adult_only")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Data)
                    .IsUnicode(false)
                    .HasColumnName("data");

                entity.Property(e => e.IdArticle).HasColumnName("id_article");

                entity.Property(e => e.ReaderRating).HasColumnName("reader_rating");

                entity.HasOne(d => d.Article)
                    .WithOne()
                    .HasForeignKey<Text>(d => d.IdArticle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("texts_articles_id_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

    }
}
