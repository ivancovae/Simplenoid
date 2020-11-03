using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Helpers
{
    /// <summary>
    /// Типы Бонусов
    /// </summary>
    public enum TypesBonuses : byte
    {
        None = 0, // не определен
        LongBoard = 1, // удлинение доски
        SlowerBalls = 2, // замедление шарика
        FasterBalls = 3, // ускорение шарика
        DoubleBalls = 4 // добавление мячика
    }
}