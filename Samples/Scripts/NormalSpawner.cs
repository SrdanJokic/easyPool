using Godot;
using System;

namespace EasyPool.Samples;

public partial class NormalSpawner : Node
{
    [Export] private Node _spawnedContainer;
    [Export] private PackedScene _projectile;

    public void Reset()
    {
        // Purge any previous projectiles
        var spawnedProjectiles = _spawnedContainer.GetChildren();
        foreach (var projectile in spawnedProjectiles)
        {
            projectile.QueueFree();
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        var projectile = _projectile.Instantiate().GetNode<Projectile>(".");
        _spawnedContainer.AddChild(projectile);

        projectile.Reset();
        projectile.Fire(Vector2.Right, () => projectile.QueueFree());
    }
}
