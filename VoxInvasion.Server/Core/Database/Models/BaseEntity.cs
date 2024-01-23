using System.ComponentModel.DataAnnotations;

namespace VoxInvasion.Server.Core.Database.Models;

public class BaseEntity
{
    [Key] public Guid Id { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}