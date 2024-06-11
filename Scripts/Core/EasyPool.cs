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

    public void Clear()
    {
        CountBorrowed = 0;
        DoClear();
    }

    protected abstract void DoClear();

    public T Borrow(Func<T> creationDelegate)
    {
        CountBorrowed++;
        return DoBorrow(creationDelegate);
    }

    protected abstract T DoBorrow(Func<T> creationDelegate);

    public void Return(T instance)
    {
        // If adding the node would breach capacity, destroy it instead
        if (Settings.Capacity.HasValue && CountInPool + 1 > Settings.Capacity.Value)
        {
            instance.QueueFree();
            return;
        }

        CountBorrowed = CountBorrowed > 0 ? CountBorrowed - 1 : 0;
        DoReturn(instance);
    }

    protected abstract void DoReturn(T instance);
}
