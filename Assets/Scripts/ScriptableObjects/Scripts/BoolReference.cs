using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [Serializable]
    public class BoolReference
    {
        public bool UseConstant = true;
        public bool ConstantValue;
        public BoolVariable Variable;

        public bool Value => UseConstant ? ConstantValue : Variable.Value;
    }
}