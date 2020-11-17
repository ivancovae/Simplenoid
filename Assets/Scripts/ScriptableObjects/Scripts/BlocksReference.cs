using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [Serializable]
    public class BlocksReference
    {
        public BlocksVariable Variable;
        public IReadOnlyList<Block> Value => Variable.Items;
    }
}