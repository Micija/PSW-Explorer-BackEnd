using System.ComponentModel.DataAnnotations.Schema;

namespace PSW24.BuildingBlocks.Core.Domain;

public abstract class Entity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; protected set; }

    public override bool Equals(object? obj)
    {
        return obj is Entity entity && Id.Equals(entity.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}