﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WFKeevo.Data;

#nullable disable

namespace WFKeevo.Migrations
{
    [DbContext(typeof(WFKeevoDBContext))]
    [Migration("20250110001541_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WFKeevo.Models.Lancto", b =>
                {
                    b.Property<Guid?>("LanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LanDataFinal")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LanDataInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("TarefaId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<int>("TarefaTarCodigo")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.HasKey("LanId");

                    b.HasIndex("TarefaTarCodigo");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Lancto");
                });

            modelBuilder.Entity("WFKeevo.Models.Tarefa", b =>
                {
                    b.Property<int>("TarCodigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TarCodigo"));

                    b.Property<DateTime?>("TarDataFinal")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("TarDataInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TarNome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("TarStatus")
                        .HasColumnType("integer");

                    b.HasKey("TarCodigo");

                    b.ToTable("Tarefa");
                });

            modelBuilder.Entity("WFKeevo.Models.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Funcao")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("WFKeevo.Models.Lancto", b =>
                {
                    b.HasOne("WFKeevo.Models.Tarefa", "Tarefa")
                        .WithMany()
                        .HasForeignKey("TarefaTarCodigo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WFKeevo.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tarefa");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}