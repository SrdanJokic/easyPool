/*
* EasyNodePool.cs
*
* This script is licensed under the MIT License.
* See the LICENSE file in the root of the repository for more details.
*
* Copyright (c) 2024 Srdan Jokic
*/

using Godot;
using System;

namespace EasyPool;

public abstract class EasyNodePool<T> : IEasyPool<T> where T : Node
{
    public int CountBorrowed { get; private set; }
    public abstract int CountInPool { get; }

    public event Action<T> OnInstanceReturned;
    public event Action<T> OnInstanceBorrowed;

    protected EasyPoolSettings Settings;

    public EasyNodePool(EasyPoolSettings settings)
    {
        if (settings == null)
        {
            throw new ArgumentNullException(nameof(settings), $"EasyPool failed to initialize; {nameof(settings)} was null");
        }

        Settings = settings;
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

        if (CountInPool == 0)
        {
            return creationDelegate?.Invoke();
        }

        // Unlink the child from the pool tree
        var instance = DoBorrow(creationDelegate);
        Settings.Parent?.RemoveChild(instance);
        OnInstanceBorrowed?.Invoke(instance);

        return instance;
    }

    protected abstract T DoBorrow(Func<T> creationDelegate);

    public void Return(T instance)
    {
        CountBorrowed = CountBorrowed > 0 ? CountBorrowed - 1 : 0;

        // If adding the node would breach capacity, destroy it instead
        if (Settings.Capacity.HasValue && CountInPool + 1 > Settings.Capacity.Value)
        {
            Free(instance);
            return;
        }
        
        instance.SetProcess(false);

        if (Settings.Parent != null)
        {
            // Re-link the child under the pool tree
            instance.GetParent()?.RemoveChild(instance);
            Settings.Parent.AddChild(instance);
        }

        DoReturn(instance);
        OnInstanceReturned?.Invoke(instance);
    }

    protected abstract void DoReturn(T instance);

    protected void Free(T instance)
    {
        instance.QueueFree();
    }
}
