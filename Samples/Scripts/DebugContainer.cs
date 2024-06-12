using Godot;
using System;

namespace EasyPool.Samples;

public partial class DebugContainer : BoxContainer
{
    [Export] private Label _borrowed;
    [Export] private Label _available;
    [Export] private PooledSpawner _spawner;

    private const string _BORROWED_PREFIX = "Borrowed: ";
    private const string _AVAILABLE_PREFIX = "Available: ";

    public override void _EnterTree()
    {
        _spawner.OnProcessed += UpdateDisplay;
    }

    public override void _ExitTree()
    {
        _spawner.OnProcessed -= UpdateDisplay;
    }

    private void UpdateDisplay(int borrowedCount, int availableCount)
    {
        _borrowed.Text = $"{_BORROWED_PREFIX} {borrowedCount}";
        _available.Text = $"{_AVAILABLE_PREFIX} {availableCount}";
    }
}
