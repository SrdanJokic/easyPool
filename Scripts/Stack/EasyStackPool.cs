using Godot;
using System;
using System.Collections.Generic;

namespace EasyPool.Stack;

public sealed class EasyStackPool<T> : EasyPool<T> where T : Node
{
    private readonly Stack<T> _container;

    public EasyStackPool(EasyPoolSettings settings) : base(settings)
    {
        if (settings.Capacity.HasValue)
        {
            _container = new Stack<T>(settings.Capacity.Value);
        }
        else if (settings.AmountToPreload.HasValue)
        {
            _container = new Stack<T>(settings.AmountToPreload.Value);
        }
        else
        {
            _container = new Stack<T>();
        }
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
