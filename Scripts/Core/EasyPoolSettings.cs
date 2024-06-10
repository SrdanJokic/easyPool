using Godot;
using System;

namespace EasyPool;

public class EasyPoolSettings
{
    public int? Capacity { get; private set; }
    public int? AmountToPreload { get; private set; }
    public Node Parent { get; private set; }

    private EasyPoolSettings(Builder builder)
    {
        Capacity = builder.Capacity;
        AmountToPreload = builder.AmountToPreload;
        Parent = builder.Parent;
    }

    public class Builder
    {
        public int? Capacity { get; private set; }
        public Node Parent { get; private set; }
        public int? AmountToPreload { get; private set; }

        public Builder WithCapacity(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException($"Pool settings were not created; capacity of [{capacity}] was invalid");
            }

            if (AmountToPreload.HasValue && capacity < AmountToPreload)
            {
                throw new ArgumentException($"Pool settings were not created; capacity of [{capacity}] was less than preloaded count of [{AmountToPreload}]");
            }

            Capacity = capacity;
            return this;
        }

        public Builder WithThisManyToPreload(int amountToPreload)
        {
            if (amountToPreload < 0)
            {
                throw new ArgumentException($"Pool settings were not created; {nameof(amountToPreload)} must be positive or 0");
            }

            if (Capacity.HasValue && amountToPreload > Capacity)
            {
                throw new ArgumentException($"Pool settings were not created; {nameof(amountToPreload)} must be positive less or equal to total capacity of [{Capacity}]");
            }

            AmountToPreload = amountToPreload;
            return this;
        }

        public Builder WithParentOfInactives(Node parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), $"Pool settings were not created; {nameof(parent)} was null");
            }

            Parent = parent;
            return this;
        }

        public EasyPoolSettings Build()
        {
            return new EasyPoolSettings(this);
        }
    }
}
