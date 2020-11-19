using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Объект хранения значения типа bool
    /// </summary>
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "Variable/Bool Variable")]
    public class BoolVariable : ScriptableObject
    {
        public bool Value;
    }
}