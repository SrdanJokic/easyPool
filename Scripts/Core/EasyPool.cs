using EasyPool;
using Godot;
using System;

namespace EasyPool;

public abstract class EasyNodePool<T> : IEasyPool<T> where T : Node
{
    protected Node Parent;
    protected Func<T> CreationDelegate;
    protected EasyPoolSettings Settings;

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
        Parent = settings.Parent;
        Parent ??= new Node()
        {
            Name = $"[{typeof(T)}] Pool"
        };
    }

    public abstract void Clear();

    public abstract T Fetch(Func<T> creationDelegate);

    public abstract void Return(T instance);

    protected void AssignParent(T instance)
    {
        Parent.AddChild(instance);
    }
}
