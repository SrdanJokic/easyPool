using Godot;
using System;

namespace EasyPool;

public interface IEasyPool<T>
{
    /// <summary>
    /// Fetch an instance from a pool. If the pool has no remaining instances
    /// to return, a new instance will be created.
    /// </summary>
    /// <param name="creationDelegate">
    /// Delegate to be invoked if <see cref="Fetch"/>is invoked on an empty contain 
    /// - in this case, a new instance is returned using this func.
    /// </param>
    /// <returns><An instance from a pool./returns>
    T Fetch(Func<T> creationDelegate);

    /// <summary>
    /// Return an instance back into the pool.
    /// </summary>
    /// <param name="instance">Instance to be returned into the pool</param>
    void Return(T instance);

    /// <summary>
    /// Clears the pool of all instances.
    /// </summary>
    void Clear();
}
