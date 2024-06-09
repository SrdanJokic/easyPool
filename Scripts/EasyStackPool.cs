using Godot;
using System;
using System.Collections.Generic;

namespace EasyPool;

public sealed class EasyStackPool<T> : EasyPool<T> where T : Node
{
    private readonly Stack<T> _container;

    public EasyStackPool() : base()
    {
        _container = new Stack<T>();
    }

    public EasyStackPool(int capacity) : base()
    {
        _container = new Stack<T>(capacity);
    }

    public EasyStackPool(IEnumerable<T> initials) : base()
    {
        _container = new Stack<T>(initials);
    }

    public EasyStackPool(Node parent) : base(parent)
    {
        _container = new Stack<T>();
    }

    public EasyStackPool(int capacity, Node parent) : base(parent)
    {
        _container = new Stack<T>(capacity);
    }

    public EasyStackPool(IEnumerable<T> initials, Node parent) : base(parent)
    {
        _container = new Stack<T>(initials);
    }

    public override void Clear()
    {
        throw new NotImplementedException();
    }

    public override T Fetch()
    {
        throw new NotImplementedException();
    }

    public override void Return(T instance)
    {
        throw new NotImplementedException();
    }
}
