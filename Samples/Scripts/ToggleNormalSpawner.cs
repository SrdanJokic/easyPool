/*
* ToggleNormalSpawner.cs
*
* This script is licensed under the MIT License.
* See the LICENSE file in the root of the repository for more details.
*
* Copyright (c) 2024 Srdan Jokic
*/

using Godot;

namespace EasyPool.Samples;

public partial class ToggleNormalSpawner : CheckButton
{
    [Export] private NormalSpawner _normalSpawner;

    public override void _Ready()
    {
        _normalSpawner.Toggle(ButtonPressed);
    }

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);

        _normalSpawner.Toggle(toggledOn);
    }
}
