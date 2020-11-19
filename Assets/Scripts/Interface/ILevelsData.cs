using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    /// <summary>
    /// Интерфейс для контроллера уровней
    /// </summary>
    public interface ILevelsData
    {
        LevelsVariable GetLevels { get; }
        BlocksVariable GetBlocks { get; }
    }
}