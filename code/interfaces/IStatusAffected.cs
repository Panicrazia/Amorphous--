using Godot;
using System;
using System.Collections.Generic;

public interface IStatusAffected
{
    IDictionary<StatusEnum, int> statuses { get; set; }

    public virtual bool AddStatus(StatusEnum status, int duration)
    {
        return statuses.TryAdd(status, duration);
    }

    public virtual bool RemoveStatus(StatusEnum status)
    {
        return statuses.Remove(status);
    }

    public virtual void DecrementStatusDuration()
    {
        foreach (KeyValuePair<StatusEnum, int> i in statuses)
        {
            statuses[i.Key] = i.Value - 1;
            if(i.Value <= 0)
            {
                RemoveStatus(i.Key);
            }
        }
    }

    public void LiterallyNothing()
    {

    }
}
