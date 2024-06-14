using Godot;
using System;

namespace EasyPool.Samples;

public partial class TogglePooledSpawner : CheckButton
{
    [Export] private PooledSpawner _pooledSpawner;

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);

        _pooledSpawner.Reset();
        _pooledSpawner.SetProcess(toggledOn);
    }
}
