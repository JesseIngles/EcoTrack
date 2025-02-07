using System;
using EcoTrack.DAL.Database.Enums;
namespace EcoTrack.DAL.Database.Entities;
public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } 
    public string Password { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

}
