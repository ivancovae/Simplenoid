using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [Serializable]
    public class BallsReference
    {
        public BallsVariable Variable;
        public IReadOnlyList<Ball> Value => Variable.Items;
    }
}