using System.Text.Json.Serialization;
using EcoTrack.DAL.Database.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.DTO;

public class DTO_SustainableAction
{
  [JsonIgnore]
  public Guid Id { get; set; } = Guid.NewGuid();
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public SustainableCategory Category { get; set; }
  public int Points { get; set; }
  [JsonIgnore]
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
  [JsonIgnore]
  public required Guid UserId { get; set; } 
}