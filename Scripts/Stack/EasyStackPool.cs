using Godot;
using System;
using System.Collections.Generic;

namespace EasyPool.Stack;

public sealed class EasyStackPool<T> : EasyNodePool<T> where T : Node
{
    public override int CountInPool => _container.Count;

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
        while (_container.Count != 0)
        {
            _container.Pop().QueueFree();
        }
    }

    public override T Borrow(Func<T> creationDelegate)
    {
        if (_container.Count == 0)
        {
            return creationDelegate?.Invoke();
        }

        var top = _container.Pop();
        Parent.RemoveChild(top);

        return top;
    }

    public override void Return(T instance)
    {
        instance.SetProcess(false);

        instance.Owner?.RemoveChild(instance);
        Parent.AddChild(instance);
    }

    protected override T DoBorrow(Func<T> creationDelegate)
    {
        throw new NotImplementedException();
    }
}

// TODO: Fail returning if capacity would be breached
// TODO: Fail fetching a new one if capacity would be breached
// TODO: Preload based on the amount to preload