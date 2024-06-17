/*
* EasyStackPool.cs
*
* This script is licensed under the MIT License.
* See the LICENSE file in the root of the repository for more details.
*
* Copyright (c) 2024 Srdan Jokic
*/

using Godot;
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
            Free(_container.Pop());
        }
    }

    protected override T DoBorrow()
    {
        return _container.Pop();
    }

    protected override void DoReturn(T instance)
    {
        _container.Push(instance);
    }
}
