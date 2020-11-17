using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Variable/Float Variable")]
    public class FloatVariable : ScriptableObject
    {
        public float Value;
    }
}