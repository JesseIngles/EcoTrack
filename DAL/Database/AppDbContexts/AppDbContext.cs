using EcoTrack.DAL.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.DAL.Database.AppDbContexts;

public class AppDbContext : DbContext 
{
  public DbSet<SustainableAction> SustainableActions {get;set;}
  public DbSet<User> Users {get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql("Host=pg-2bc4b2f1-testestartup.l.aivencloud.com;Port=13098;Database=defaultdb;Username=avnadmin;Password=AVNS_SxXnGsgViRNpKmAUCbJ;SSL Mode=Require;");
    }
}