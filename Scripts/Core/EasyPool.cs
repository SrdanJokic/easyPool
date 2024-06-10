using EasyPool;
using Godot;
using System;

namespace EasyPool;

public abstract class EasyPool<T> : IEasyPool<T> where T : Node
{
    protected Node _parent;
    protected Func<T> _creationDelegate;

    /// <summary>
    /// Initializes the easy pool.
    /// </summary>
    /// <param name="settings">Settings of the pool.</param>
    /// <param name="creationDelegate">
    /// Delegate to be invoked if <see cref="Fetch"/>is invoked on an empty contain 
    /// - in this case, a new instance is returned using this func.
    /// </param>
    public EasyPool(EasyPoolSettings settings, Func<T> creationDelegate)
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

        _creationDelegate = creationDelegate;
    }

    public abstract void Clear();

    public abstract T Fetch();

    public abstract void Return(T instance);

    protected void AssignParent(T instance)
    {
        _parent.AddChild(instance);
    }
}
