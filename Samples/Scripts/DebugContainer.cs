using Godot;
using System;

namespace EasyPool.Samples;

public partial class DebugContainer : Node
{
    [Export] private Label _borrowed;
    [Export] private Label _available;
    [Export] private Label _framerate;
    [Export] private PooledSpawner _pooledSpawner;

    public override void _Process(double delta)
    {
        base._Process(delta);

        _framerate.Text = $"FPS: {Engine.GetFramesPerSecond()}";
        _borrowed.Text = $"Borrowed: {_pooledSpawner.ProjectilePool.CountBorrowed}";
        _available.Text = $"Available: {_pooledSpawner.ProjectilePool.CountInPool}";
    }
}
