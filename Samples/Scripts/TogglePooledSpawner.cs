using Godot;
using System;

namespace EasyPool.Samples;

public partial class TogglePooledSpawner : CheckButton
{
    [Export] private PooledSpawner _pooledSpawner;

    public override void _Ready()
    {
        _pooledSpawner.Toggle(ButtonPressed);
    }

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);

        _pooledSpawner.Toggle(toggledOn);
    }
}
