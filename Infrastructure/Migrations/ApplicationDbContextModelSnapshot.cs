﻿// <auto-generated />
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Levels.Level", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR2")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR2")
                        .HasColumnName("NAME");

                    b.HasKey("Id")
                        .HasName("PK_LEVELS");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_LEVELS_NAME");

                    b.ToTable("LEVELS", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Easy"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Medium"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("Domain.Permissions.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NAME");

                    b.HasKey("Id")
                        .HasName("PK_PERMISSIONS");

                    b.ToTable("PERMISSIONS", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ReadMember"
                        },
                        new
                        {
                            Id = 2,
                            Name = "UpdateMember"
                        });
                });

            modelBuilder.Entity("Domain.PlayerRoles.PlayerRole", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PLAYER_ID");

                    b.Property<int>("RoleId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ROLE_ID");

                    b.HasKey("PlayerId", "RoleId")
                        .HasName("PK_PLAYER_ROLES");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("IX_PLAYER_ROLES_ROLE_ID");

                    b.ToTable("PLAYER_ROLES", (string)null);
                });

            modelBuilder.Entity("Domain.PlayerScores.PlayerScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LevelId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("LEVEL_ID");

                    b.Property<int>("PlayerId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PLAYER_ID");

                    b.Property<decimal>("Score")
                        .HasColumnType("NUMBER")
                        .HasColumnName("SCORE");

                    b.HasKey("Id")
                        .HasName("PK_PLAYER_SCORES");

                    b.HasIndex("LevelId")
                        .HasDatabaseName("IX_PLAYER_SCORES_LEVEL_ID");

                    b.HasIndex("PlayerId", "LevelId")
                        .IsUnique()
                        .HasDatabaseName("IX_PLAYER_SCORES_PLAYER_ID_LEVEL_ID");

                    b.ToTable("PLAYER_SCORES", (string)null);
                });

            modelBuilder.Entity("Domain.Players.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR2")
                        .HasColumnName("IDENTITY_ID");

                    b.HasKey("Id")
                        .HasName("PK_PLAYERS");

                    b.HasIndex("IdentityId")
                        .IsUnique()
                        .HasDatabaseName("IX_PLAYERS_IDENTITY_ID");

                    b.ToTable("PLAYERS", (string)null);
                });

            modelBuilder.Entity("Domain.RolePermissions.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ROLE_ID");

                    b.Property<int>("PermissionId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PERMISSION_ID");

                    b.HasKey("RoleId", "PermissionId")
                        .HasName("PK_ROLE_PERMISSIONS");

                    b.HasIndex("PermissionId")
                        .HasDatabaseName("IX_ROLE_PERMISSIONS_PERMISSION_ID");

                    b.ToTable("ROLE_PERMISSIONS", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 2
                        });
                });

            modelBuilder.Entity("Domain.Roles.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NAME");

                    b.HasKey("Id")
                        .HasName("PK_ROLES");

                    b.ToTable("ROLES", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Registered"
                        });
                });

            modelBuilder.Entity("Domain.PlayerRoles.PlayerRole", b =>
                {
                    b.HasOne("Domain.Players.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PLAYER_ROLES_PLAYERS_PLAYER_ID");

                    b.HasOne("Domain.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PLAYER_ROLES_ROLES_ROLE_ID");
                });

            modelBuilder.Entity("Domain.PlayerScores.PlayerScore", b =>
                {
                    b.HasOne("Domain.Levels.Level", null)
                        .WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PLAYER_SCORES_LEVELS_LEVEL_ID");

                    b.HasOne("Domain.Players.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PLAYER_SCORES_PLAYERS_PLAYER_ID");
                });

            modelBuilder.Entity("Domain.Players.Player", b =>
                {
                    b.OwnsOne("Domain.Players.Email", "Email", b1 =>
                        {
                            b1.Property<int>("PlayerId")
                                .HasColumnType("NUMBER(10)")
                                .HasColumnName("ID");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("VARCHAR2")
                                .HasColumnName("EMAIL");

                            b1.HasKey("PlayerId");

                            b1.HasIndex("Value")
                                .IsUnique()
                                .HasDatabaseName("IX_PLAYERS_EMAIL");

                            b1.ToTable("PLAYERS");

                            b1.WithOwner()
                                .HasForeignKey("PlayerId")
                                .HasConstraintName("FK_PLAYERS_PLAYERS_ID");
                        });

                    b.OwnsOne("Domain.Players.Name", "Nickname", b1 =>
                        {
                            b1.Property<int>("PlayerId")
                                .HasColumnType("NUMBER(10)")
                                .HasColumnName("ID");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("VARCHAR2")
                                .HasColumnName("NICKNAME");

                            b1.HasKey("PlayerId");

                            b1.ToTable("PLAYERS");

                            b1.WithOwner()
                                .HasForeignKey("PlayerId")
                                .HasConstraintName("FK_PLAYERS_PLAYERS_ID");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Nickname")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.RolePermissions.RolePermission", b =>
                {
                    b.HasOne("Domain.Permissions.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ROLE_PERMISSIONS_PERMISSIONS_PERMISSION_ID");

                    b.HasOne("Domain.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ROLE_PERMISSIONS_ROLES_ROLE_ID");
                });
#pragma warning restore 612, 618
        }
    }
}
