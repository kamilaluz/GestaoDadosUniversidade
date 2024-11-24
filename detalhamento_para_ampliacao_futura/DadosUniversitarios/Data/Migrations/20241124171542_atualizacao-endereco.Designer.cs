﻿// <auto-generated />
using System;
using DadosUniversitarios.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DadosUniversitarios.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241124171542_atualizacao-endereco")]
    partial class atualizacaoendereco
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AlunoDisciplina", b =>
                {
                    b.Property<int>("AlunosId")
                        .HasColumnType("integer");

                    b.Property<int>("DisciplinasId")
                        .HasColumnType("integer");

                    b.HasKey("AlunosId", "DisciplinasId");

                    b.HasIndex("DisciplinasId");

                    b.ToTable("AlunoDisciplina");
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("CursoId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumeroMatricula")
                        .HasColumnType("integer");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Disciplina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int?>("CursoId")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NomeServico")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Empresas");

                    b.HasDiscriminator().HasValue("Empresa");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NomeRua")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Numero")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("CursoId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumeroMatricula")
                        .HasColumnType("integer");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Professores");
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmacaoSenha")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Fornecedor", b =>
                {
                    b.HasBaseType("DadosUniversitarios.Models.Empresa");

                    b.Property<DateOnly>("DataPagamento")
                        .HasColumnType("date");

                    b.Property<int>("NumeroContrato")
                        .HasColumnType("integer");

                    b.Property<int>("Periodicidade")
                        .HasColumnType("integer");

                    b.Property<double>("ValorServico")
                        .HasColumnType("double precision");

                    b.Property<DateOnly>("VencimentoContrato")
                        .HasColumnType("date");

                    b.HasDiscriminator().HasValue("Fornecedor");
                });

            modelBuilder.Entity("AlunoDisciplina", b =>
                {
                    b.HasOne("DadosUniversitarios.Models.Aluno", null)
                        .WithMany()
                        .HasForeignKey("AlunosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DadosUniversitarios.Models.Disciplina", null)
                        .WithMany()
                        .HasForeignKey("DisciplinasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Aluno", b =>
                {
                    b.HasOne("DadosUniversitarios.Models.Curso", null)
                        .WithMany("Alunos")
                        .HasForeignKey("CursoId");

                    b.HasOne("DadosUniversitarios.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Disciplina", b =>
                {
                    b.HasOne("DadosUniversitarios.Models.Curso", null)
                        .WithMany("Disciplinas")
                        .HasForeignKey("CursoId");

                    b.HasOne("DadosUniversitarios.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Empresa", b =>
                {
                    b.HasOne("DadosUniversitarios.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Professor", b =>
                {
                    b.HasOne("DadosUniversitarios.Models.Curso", null)
                        .WithMany("Professores")
                        .HasForeignKey("CursoId");

                    b.HasOne("DadosUniversitarios.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("DadosUniversitarios.Models.Curso", b =>
                {
                    b.Navigation("Alunos");

                    b.Navigation("Disciplinas");

                    b.Navigation("Professores");
                });
#pragma warning restore 612, 618
        }
    }
}
