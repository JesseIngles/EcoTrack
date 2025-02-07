using System;
using EcoTrack.DAL.Database.Enums;
namespace EcoTrack.DAL.Database.Entities;
public class SustainableAction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public SustainableCategory Category { get; set; }
    public int Points { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    public Guid UserId { get; set; } 
}
