/*
* EasyPoolSettings.cs
*
* This script is licensed under the MIT License.
* See the LICENSE file in the root of the repository for more details.
*
* Copyright (c) 2024 Srdan Jokic
*/

using Godot;
using System;

namespace EasyPool;

public class EasyPoolSettings
{
    public int? Capacity { get; private set; }
    public Node Parent { get; private set; }

    private EasyPoolSettings(Builder builder)
    {
        Capacity = builder.Capacity;
        Parent = builder.Parent;
    }

    public class Builder
    {
        public int? Capacity { get; private set; }
        public Node Parent { get; private set; }


        public Builder WithCapacity(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException($"Pool settings were not created; capacity of [{capacity}] was invalid");
            }

            Capacity = capacity;
            return this;
        }

        public Builder WithParentOfInactives(Node parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent), $"Pool settings were not created; {nameof(parent)} was null");
            return this;
        }

        public EasyPoolSettings Build()
        {
            return new EasyPoolSettings(this);
        }
    }
}
