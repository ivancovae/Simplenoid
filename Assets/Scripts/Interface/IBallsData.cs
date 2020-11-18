using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    public interface IBallsData
    {
        BallsVariable GetBalls { get; }
        BoardVariable GetBoard { get; }
    }
}