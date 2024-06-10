using Godot;
using System;
using System.Collections.Generic;

namespace EasyPool;

public sealed class EasyStackPool<T> : EasyPool<T> where T : Node
{
    private readonly Stack<T> _container;

    public EasyStackPool(EasyPoolSettings settings) : base(settings)
    {
        
    }

    public override void Clear()
    {
        throw new NotImplementedException();
    }

    public override T Fetch()
    {
        throw new NotImplementedException();
    }

    public override void Return(T instance)
    {
        throw new NotImplementedException();
    }
}
