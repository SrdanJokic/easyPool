/*
* Projectile.cs
*
* This script is licensed under the MIT License.
* See the LICENSE file in the root of the repository for more details.
*
* Copyright (c) 2024 Srdan Jokic
*/

using Godot;
using System;

namespace EasyPool.Samples;

public sealed partial class Projectile : RigidBody2D
{
    [Export] private VisibleOnScreenNotifier2D _visibilityNotifier;
    [Export] private float _velocity = 200;

    private Vector2 _direction;
    private Action _onOutsideOfViewport;
    private bool _resetRequested;

    public void Fire(Vector2 direction, Action onOutsideOfViewport)
    {
        _direction = direction;
        _onOutsideOfViewport = onOutsideOfViewport;
        
        _visibilityNotifier.ScreenExited += LeaveScreen;
    }

    public void Reset()
    {
        _resetRequested = true;
    }

    private void LeaveScreen()
    {
        _visibilityNotifier.ScreenExited -= LeaveScreen;
        _onOutsideOfViewport?.Invoke();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (_resetRequested)
        {
            _resetRequested = false;
            Position = Vector2.Zero;
        }

        MoveAndCollide(_direction * _velocity * (float)delta);
    }
}
