using EasyPool;
using Godot;
using System;

namespace EasyPool;

public abstract class EasyPool<T> : IEasyPool<T> where T : Node
{
    private Node _parent;

    protected EasyPool()
    {
        _parent = new Node()
        {
            Name = $"[{typeof(T)}] Pool"
        };
    }

    protected EasyPool(Node parent)
    {
        _parent = parent ?? throw new ArgumentNullException(nameof(parent), "EasyPool failed to initialize; given parent was null");
    }

    public abstract void Clear();

    public abstract T Fetch();

    public abstract void Return(T instance);

    protected void AssignParent(T instance)
    {
        _parent.AddChild(instance);
    }
}
