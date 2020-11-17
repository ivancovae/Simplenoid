using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [Serializable]
    public class BonusesReference
    {
        public BonusesVariable Variable;
        public IReadOnlyList<Bonus> Value => Variable.Items;
    }
}