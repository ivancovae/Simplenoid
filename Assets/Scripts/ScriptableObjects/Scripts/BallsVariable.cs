using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [CreateAssetMenu(fileName = "BallsVariable", menuName = "Variable/Balls Variable")]
    public class BallsVariable : RuntimeSet<Ball>
    {
        public Ball PrefabBall;
    }
}