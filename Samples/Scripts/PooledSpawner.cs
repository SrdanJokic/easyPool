using EasyPool.Stack;
using Godot;
using System;
using System.Threading.Tasks;

namespace EasyPool.Samples;

public sealed partial class PooledSpawner : Node
{
    [Export] private int _capacity = 5000;
    [Export] private Node _spawnedContainer;
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
        var projectile = BorrowProjectile();

        projectile.Reset();
        projectile.Fire(Vector2.Right, () => ReturnProjectile(projectile));
    }

    private Projectile BorrowProjectile()
    {
        var projectile = _projectilePool.Borrow(CreateProjectile);
        OnProcessed?.Invoke(_projectilePool.CountBorrowed, _projectilePool.CountInPool);

        return projectile;
    }

    private Projectile CreateProjectile()
    {
        var projectile = _projectile.Instantiate().GetNode<Projectile>(".");
        _spawnedContainer.AddChild(projectile);

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
        var spawnedProjectiles = _spawnedContainer.GetChildren();
        foreach (var projectile in spawnedProjectiles)
        {
            projectile.QueueFree();
        }

        OnProcessed?.Invoke(_projectilePool.CountBorrowed, _projectilePool.CountInPool);
    }
}
