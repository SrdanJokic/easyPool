using Godot;
using System;

namespace EasyPool;

public interface IEasyPool<T>
{
    int CountBorrowed { get; }
    int CountInPool { get; }

    /// <summary>
    /// Fetch an instance from a pool. If the pool has no remaining instances
    /// to return, a new instance will be created.
    /// </summary>
    /// <param name="creationDelegate">
    /// Delegate to be invoked if <see cref="Borrow"/>is invoked on an empty contain 
    /// - in this case, a new instance is returned using this func.
    /// </param>
    /// <returns><An instance from a pool./returns>
    T Borrow(Func<T> creationDelegate);

    /// <summary>
    /// Return an instance back into the pool. If the capacity was set and if it would
    /// be breached by the return of an instance in a pool, the instance is destroyed instead.
    /// </summary>
    /// <param name="instance">Instance to be returned into the pool</param>
    void Return(T instance);

    /// <summary>
    /// Clears the pool of all instances.
    /// </summary>
    void Clear();
}
