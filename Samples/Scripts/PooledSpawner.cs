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

    public EasyNodePool<Projectile> ProjectilePool => _projectilePool;
    private EasyNodePool<Projectile> _projectilePool;
    private bool _isActive;

    public override void _Ready()
    {
        _projectilePool = new EasyStackPool<Projectile>(new EasyPoolSettings.Builder()
            .WithCapacity(_capacity)
            .Build());
    }

    public void Toggle(bool isActive)
    {
        _isActive = isActive;
    }

    public override void _Process(double delta)
    {
        if (!_isActive)
        {
            return;
        }

        foreach (var spawnedContainer in _spawnedContainers)
        {
            var projectile = _projectilePool.Borrow(() => CreateProjectile(spawnedContainer));

            projectile.Reset();
            projectile.Fire(Vector2.Right, () => _projectilePool.Return(projectile));
        }
    }

    private Projectile CreateProjectile(Node parent)
    {
        var projectile = _projectile.Instantiate().GetNode<Projectile>(".");
        parent.AddChild(projectile);

        return projectile;
    }
}
