using System.ComponentModel.DataAnnotations;
using GoldenDelicious.Common.Data;

namespace GoldenDelicious.DiscordBot.Data.Models;

public class GameActivityEntity : DatabaseEntity
{
    public ulong ServerId { get; set; }

    [Required]
    public required ulong UserId { get; set; }

    [Required, MaxLength(50)]
    public required string UserName { get; set; }

    [Required, MaxLength(50)]
    public required string UserDisplayName { get; set; }

    [Required, MaxLength(250)]
    public required string GameName { get; set; }
    
    [Required]
    public DateTime TimestampStart { get; set; }
    
    public DateTime? TimestampEnd { get; set; }
    
    // explicit conversion to Model
    public static explicit operator GameActivity(GameActivityEntity entity)
    {
        return new GameActivity(
            entity.Id, 
            entity.UserName, 
            entity.UserDisplayName,
            entity.UserId, 
            entity.ServerId, 
            entity.GameName, 
            entity.TimestampStart,
            entity.TimestampEnd);
    }
    
    // explicit conversation from Model
    public static explicit operator GameActivityEntity(GameActivity model)
    {
        return new GameActivityEntity
        {
            Id = model.Id,
            ServerId = model.ServerId,
            UserId = model.UserId,
            UserDisplayName = model.UserDisplayName,
            UserName = model.UserName,
            GameName = model.GameName,
            TimestampStart = model.StartTime,
            TimestampEnd = model.EndTime,
        };
    }
}
