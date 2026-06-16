using Microsoft.EntityFrameworkCore;
using QuizApi.Models;

namespace QuizApi.Data
{
    public class QuizAppDbContext : DbContext
    {
        private QuizAppDbContext _dbContext;
        public QuizAppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    // Configure a one-to-many relationship with cascade delete
        modelBuilder.Entity<Quiz>()            
            .HasMany(p=>p.Options)              
            .WithOne()                   
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Quiz>()            
            .HasOne(p=>p.QnCorrectOption)              
            .WithOne()     
            .HasForeignKey<CorrectOption>(p => p.QnId)            
            .OnDelete(DeleteBehavior.Cascade); 
        modelBuilder.Entity<Quiz>()            
            .HasOne(p=>p.QnCategory)              
            .WithOne()        
            .HasForeignKey<Category>(c => c.QnId)    
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Quiz>()
            .HasOne(p => p.AnswerKeyExplanation)
            .WithOne()
            .HasForeignKey<AnswerKeyExplanation>(e => e.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
         }
         
        public DbSet<Quiz> Quiz { get; set;  }
        public DbSet<AnswerKeyExplanation> AnswerKeyExplanations { get; set; }
        public DbSet<Test> TestData { get; set; }
    }
}

