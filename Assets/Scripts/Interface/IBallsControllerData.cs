using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    public interface IBallsControllerData
    {
        BallsVariable GetBalls { get; }
        BoardVariable GetBoard { get; }

        BlocksVariable GetBlocks { get; }
        BordersVariable GetBorders { get; }

        BoolVariable GetIsFaster { get; }
        BoolVariable GetIsSlowly { get; }
        FloatVariable GetDivider { get; }
    }
}