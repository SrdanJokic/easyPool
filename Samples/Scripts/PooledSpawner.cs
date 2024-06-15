using EasyPool.Stack;
using Godot;
using System;
using System.Threading.Tasks;

namespace EasyPool.Samples;

public sealed partial class PooledSpawner : Node
{
    [Export] private int _capacity = 5000;
    [Export] private Godot.Collections.Array<Node> _spawnedContainers;
    [Export] private PackedScene _projectile;

    public delegate void ProcessDelegate(int borrowedCount, int availableCount);
    public event ProcessDelegate OnProcessed;

    private EasyNodePool<Projectile> _projectilePool;

    public override void _Ready()
    {
        _projectilePool = new EasyStackPool<Projectile>(new EasyPoolSettings.Builder()
            .WithCapacity(_capacity)
            .Build());
    }

    public override void _Process(double delta)
    {
        foreach (var spawnedContainer in _spawnedContainers)
        {
            var projectile = BorrowProjectile(spawnedContainer);

            projectile.Reset();
            projectile.Fire(Vector2.Right, () => ReturnProjectile(projectile));
        }
    }

    private Projectile BorrowProjectile(Node parent)
    {
        var projectile = _projectilePool.Borrow(() => CreateProjectile(parent));
        OnProcessed?.Invoke(_projectilePool.CountBorrowed, _projectilePool.CountInPool);

        return projectile;
    }

    private Projectile CreateProjectile(Node parent)
    {
        var projectile = _projectile.Instantiate().GetNode<Projectile>(".");
        parent.AddChild(projectile);

        return projectile;
    }

    private void ReturnProjectile(Projectile projectile)
    {
        _projectilePool.Return(projectile);
        OnProcessed?.Invoke(_projectilePool.CountBorrowed, _projectilePool.CountInPool);
    }

    public void Reset()
    {
        // Clear the pool of all cached
        _projectilePool.Clear();

        // Purge any remaining projectiles
        foreach (var spawnedContainer in _spawnedContainers)
        {
            var spawnedProjectiles = spawnedContainer.GetChildren();
            foreach (var projectile in spawnedProjectiles)
            {
                projectile.QueueFree();
            }
        }

        OnProcessed?.Invoke(_projectilePool.CountBorrowed, _projectilePool.CountInPool);
    }
}
