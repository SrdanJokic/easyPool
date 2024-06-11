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
        else
        {
            _container = new Stack<T>();
        }
    }

    protected override void DoClear()
    {
        while (_container.Count != 0)
        {
            _container.Pop().QueueFree();
        }
    }

    protected override T DoBorrow(Func<T> creationDelegate)
    {
        if (_container.Count == 0)
        {
            return creationDelegate?.Invoke();
        }

        var top = _container.Pop();
        Parent.RemoveChild(top);

        return top;
    }

    protected override void DoReturn(T instance)
    {
        instance.SetProcess(false);

        instance.Owner?.RemoveChild(instance);
        Parent.AddChild(instance);
    }
}
