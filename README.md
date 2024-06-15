# easyPool

A simple [object pool pattern](https://en.wikipedia.org/wiki/Object_pool_pattern) implementation in C# for Godot game engine.

## Installation

Godot only supports marking editor plugins as "plugins" unfortunately. For this reason, feel free to simply include the **EasyPool/Scripts** in your project folder.

## Quick Start

Check out the `Samples/PooledSpawner.cs` for example usage. Summary:

```csharp
// Usages
using EasyPool;
using EasyPool.Stack;

// Create a pool of "Projectiles". This can be any Node type instead.
var _projectilePool = new EasyStackPool<Projectile>(new EasyPoolSettings.Builder()
    .WithCapacity(4000) // Limit the pool to only cache up to 4000 units.
    .Build());          // Build the settings and init the pool.

// Borrow an instance from a pool
var projectile = _projectilePool.Borrow(() =>
{
    // The creation delegate here is invoked in case the pool is 
    // empty and can't provide one of the existing instances.
    return _projectile.Instantiate().GetNode<Projectile>(".");
});

// Return an instance to the pool
_projectilePool.Return(projectile);

// The amount of instances borrowed from the pool, but not yet returned .
var borrowed = _projectilePool.CountBorrowed();

// The amount of instances cached in the pool.
var inPool = _projectilePool.CountBorrowed();
```

The core folder of the package contains the structural and important scripts, while all others (currently only "Stack") should support various other implementations based on performance benefits.

## Performance benefits

Check out the samples scene to see the demo in action for yourself. The demo is setup to test the performance of spawning a physics-affected projectile every frame with the pool in use and without it. During our testing, we noticed significant performance differences between the two approaches.

### Important notes

Before looking at the results, consider the following:
- Without pooling, objects are destroyed when leaving the screen (as would be the case in actual games); with pooling, they are disabled instead.
- Once a resource is requested from a pool, if the pool has access to it, it returns it and the receiver implicitly enables it.

The mentioned facts assure that no garbage collection occurs with the pooling system in use (since we don't destroy any objects). This does mean that objects will be kept in memory as disabled instead - it is expected that you have informed yourself on the benefits and pitfalls of object pooling elsewhere already.

### Without Pooling

Upon enabling the spawn of projectiles **without pooling**, we notice varying framerate between 10-50 fps visible drops at the garbage collection moments.

![No Pool Demo](/Documentation/EasyPool-NoPoolingDemo.gif)

### With Pooling

Upon enabling the spawn of projectiles **with pooling**, we notice a constant framerate of 60 FPS with no drops. We can also notice the amount of borrowed and available objects within the pool at the top right corner.

![Pool Demo](/Documentation/EasyPool-PoolingDemo.gif)

# License

MIT License

Copyright (c) 2024 Srdan Jokic

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
