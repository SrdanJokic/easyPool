using Godot;
using System;

namespace EasyPool.Samples;

public partial class NormalSpawner : Node
{
    [Export] private Godot.Collections.Array<Node> _spawnedContainers;
    [Export] private PackedScene _projectile;

    public void Reset()
    {
        // Purge any previous projectiles
        foreach (var spawnedContainer in _spawnedContainers)
        {
            var spawnedProjectiles = spawnedContainer.GetChildren();
            foreach (var projectile in spawnedProjectiles)
            {
                projectile.QueueFree();
            }
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        foreach (var spawnedContainer in _spawnedContainers)
        {
            var projectile = _projectile.Instantiate().GetNode<Projectile>(".");
            spawnedContainer.AddChild(projectile);

            projectile.Reset();
            projectile.Fire(Vector2.Right, () => projectile.QueueFree());
        }
    }
}
