using Godot;
using System;

namespace EasyPool.Samples;

public partial class DebugContainer : Node
{
    [Export] private Label _borrowed;
    [Export] private Label _available;
    [Export] private Label _framerate;
    [Export] private PooledSpawner _pooledSpawner;

    private const string _BORROWED_PREFIX = "Borrowed: ";
    private const string _AVAILABLE_PREFIX = "Available: ";
    private const string _FPS_PREFIX = "FPS: ";

    public override void _EnterTree()
    {
        _pooledSpawner.OnProcessed += UpdateDisplay;
    }

    public override void _ExitTree()
    {
        _pooledSpawner.OnProcessed -= UpdateDisplay;
    }

    private void UpdateDisplay(int borrowedCount, int availableCount)
    {
        _borrowed.Text = $"{_BORROWED_PREFIX}{borrowedCount}";
        _available.Text = $"{_AVAILABLE_PREFIX}{availableCount}";
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        _framerate.Text = $"{_FPS_PREFIX}{Engine.GetFramesPerSecond()}";
    }
}
