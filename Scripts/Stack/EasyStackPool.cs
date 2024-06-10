using Godot;
using System;
using System.Collections.Generic;

namespace EasyPool.Stack;

public sealed class EasyStackPool<T> : EasyPool<T> where T : Node
{
    private readonly Stack<T> _container;

    public EasyStackPool(EasyPoolSettings settings, Func<T> creationDelegate) : base(settings, creationDelegate)
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
        while (_container.Count != 0)
        {
            _container.Pop().QueueFree();
        }
    }

    public override T Fetch()
    {
        if (_container.Count == 0)
        {
            return _creationDelegate.Invoke();
        }

        var top = _container.Pop();
        _parent.RemoveChild(top);

        return top;
    }

    public override void Return(T instance)
    {
        instance.SetProcess(false);

        instance.Owner?.RemoveChild(instance);
        _parent.AddChild(instance);
    }
}
