using Godot;
using System;

namespace EasyPool.Samples;

public sealed partial class Projectile : RigidBody2D
{
    [Export] private VisibleOnScreenNotifier2D _visibilityNotifier;
    [Export] private float velocity;

    private Vector2 _direction;
    private Action _onOutsideOfViewport;

    public void Fire(Vector2 direction, Action onOutsideOfViewport)
    {
        _direction = direction;
        _onOutsideOfViewport = onOutsideOfViewport;
        
        _visibilityNotifier.ScreenExited += LeaveScreen;
    }

    private void LeaveScreen()
    {
        _visibilityNotifier.ScreenExited -= LeaveScreen;
        _onOutsideOfViewport.Invoke();
    }

    public override void _PhysicsProcess(double delta)
    {
        LinearVelocity = _direction * velocity * (float)delta;
    }
}
