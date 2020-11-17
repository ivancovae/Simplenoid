using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

        public int Value => UseConstant ? ConstantValue : Variable.Value;
    }
}