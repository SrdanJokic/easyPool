using EasyPool;
using Godot;
using System;

namespace EasyPool;

public abstract class EasyPool<T> : IEasyPool<T> where T : Node
{
    private Node _parent;

    public EasyPool(EasyPoolSettings settings)
    {
        if (settings == null)
        {
            throw new ArgumentNullException(nameof(settings), $"EasyPool failed to initialize; {nameof(settings)} was null");
        }

        _parent = settings.Parent;
        _parent ??= new Node()
        {
            Name = $"[{typeof(T)}] Pool"
        };
    }

    public abstract void Clear();

    public abstract T Fetch();

    public abstract void Return(T instance);

    protected void AssignParent(T instance)
    {
        _parent.AddChild(instance);
    }
}
