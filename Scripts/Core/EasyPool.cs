using EasyPool;
using Godot;
using System;

namespace EasyPool;

public abstract class EasyNodePool<T> : IEasyPool<T> where T : Node
{
    public int CountBorrowed { get; private set; }
    public abstract int CountInPool { get; }

    protected EasyPoolSettings Settings;
    protected Node Parent;

    /// <summary>
    /// Initializes the easy pool.
    /// </summary>
    /// <param name="settings">Settings of the pool.</param>
    public EasyNodePool(EasyPoolSettings settings)
    {
        if (settings == null)
        {
            throw new ArgumentNullException(nameof(settings), $"EasyPool failed to initialize; {nameof(settings)} was null");
        }

        Settings = settings;
        Parent = GetParent(settings);
    }

    private static Node GetParent(EasyPoolSettings settings)
    {
        var parent = settings.Parent;
        parent ??= new Node()
        {
            Name = $"[{typeof(T)}] Pool"
        };

        return parent;
    }

    public abstract void Clear();

    public T Borrow(Func<T> creationDelegate)
    {
        CountBorrowed++;

        return DoBorrow(creationDelegate);
    }

    protected abstract T DoBorrow(Func<T> creationDelegate);

    public abstract void Return(T instance);

    protected void AssignParent(T instance)
    {
        Parent.AddChild(instance);
    }
}
