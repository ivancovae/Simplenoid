using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Helpers
{
    /// <summary>
    /// Типы блоков по видам
    /// </summary>
    public enum TypesBlocks : byte
    {
        None = 0, // неопределенный
        Simple = 1, // простой
        Hard = 2, // плотный
        WithBonus = 3 // с бонусом
    }
}