using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [Serializable]
    public class BordersReference
    {
        public BordersVariable Variable;
        public IReadOnlyList<Border> Value => Variable.Value;
    }
}