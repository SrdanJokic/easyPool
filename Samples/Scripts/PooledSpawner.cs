using EasyPool.Stack;
using Godot;
using System;
using System.Threading.Tasks;

namespace EasyPool.Samples;

public sealed partial class PooledSpawner : Node
{
    [Export] private int _capacity = 1000;
    [Export] private Node _pooledContainer;
    [Export] private Node _spawnedContainer;
    [Export] private PackedScene _projectile;

    public delegate void ProcessDelegate(int borrowedCount, int availableCount);
    public event ProcessDelegate OnProcessed;

    private EasyNodePool<Projectile> _projectilePool;

    public override void _Ready()
    {
        _projectilePool = new EasyStackPool<Projectile>(new EasyPoolSettings.Builder()
            .WithCapacity(_capacity)
            .WithParentOfInactives(_pooledContainer)
            .Build());
    }

    public override void _Process(double delta)
    {
        var projectile = BorrowProjectile();
        projectile.Fire(Vector2.One, () => ReturnProjectile(projectile));
    }

    private Projectile BorrowProjectile()
    {
        var projectile = _projectilePool.Borrow(() => _projectile.Instantiate().GetNode<Projectile>("."));
        OnProcessed?.Invoke(_projectilePool.CountBorrowed, _projectilePool.CountInPool);

        return projectile;
    }

    private void ReturnProjectile(Projectile projectile)
    {
        _projectilePool.Return(projectile);
        OnProcessed?.Invoke(_projectilePool.CountBorrowed, _projectilePool.CountInPool);
    }
}
