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
        AmountToPreload = builder.AmountToPreload;
        Parent = builder.Parent;
    }

    public class Builder
    {
        public Node Parent { get; private set; }
        public int? AmountToPreload { get; private set; }

        public Builder WithThisManyToPreload(int amountToPreload)
        {
            if (amountToPreload < 0)
            {
                throw new ArgumentException($"Pool settings were not created; {nameof(amountToPreload)} must be positive or 0");
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
