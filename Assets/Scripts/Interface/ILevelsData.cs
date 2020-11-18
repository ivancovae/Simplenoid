using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    public interface ILevelsData
    {
        LevelsVariable GetLevels { get; }
    }
}