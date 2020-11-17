using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Variable/Int Variable")]
    public class IntVariable : ScriptableObject
    {
        public int Value;
    }
}