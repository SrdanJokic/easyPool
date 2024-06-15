/*
* TogglePooledSpawner.cs
*
* This script is licensed under the MIT License.
* See the LICENSE file in the root of the repository for more details.
*
* Copyright (c) 2024 Srdan Jokic
*/

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
