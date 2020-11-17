using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [Serializable]
    public class LevelsReference
    {
        public LevelsVariable Variable;
        public IReadOnlyList<Level> Value => Variable.Items;
    }
}