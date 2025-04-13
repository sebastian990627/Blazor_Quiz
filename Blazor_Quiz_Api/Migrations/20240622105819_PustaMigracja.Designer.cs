﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorQuizApi.Api.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240622105819_PustaMigracja")]
    partial class PustaMigracja
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("Blazor_Quiz_Class.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Image")
                        .HasColumnType("BLOB");

                    b.Property<string>("QuestionA")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("QuestionB")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("QuestionC")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("QuestionD")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Blazor_Quiz_Class.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SelectedAnswer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserQuizResultId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserQuizResultId");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("Blazor_Quiz_Class.UserQuizResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CorrectAnswers")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TakenOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalQuestions")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserQuizResults");
                });

            modelBuilder.Entity("Blazor_Quiz_Class.UserAnswer", b =>
                {
                    b.HasOne("Blazor_Quiz_Class.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blazor_Quiz_Class.UserQuizResult", null)
                        .WithMany("Answers")
                        .HasForeignKey("UserQuizResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Blazor_Quiz_Class.UserQuizResult", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
