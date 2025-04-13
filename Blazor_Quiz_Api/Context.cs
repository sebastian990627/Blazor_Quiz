using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Blazor_Quiz_Class;


public class Context : DbContext
{
    #region ctor
    public Context(DbContextOptions<Context> options)
               : base(options)
    {
    }
    #endregion

    public DbSet<Question> Questions { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
    public DbSet<UserQuizResult> UserQuizResults { get; set; }
    public DbSet<Quiz_Main> Quiz_Main { get; set; }


}

