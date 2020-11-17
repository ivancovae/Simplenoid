using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    public abstract class ListVariable<T> : ScriptableObject where T : BaseObjectScene
    {
        public List<T> Value = new List<T>();
    }
}