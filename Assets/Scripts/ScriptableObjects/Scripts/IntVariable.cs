using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Объект для хранения значения типа Int
    /// </summary>
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Variable/Int Variable")]
    public class IntVariable : ScriptableObject
    {
        public int Value;
    }
}