using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [Serializable]
    public class BoardReference
    {
        public BoardVariable Variable;
        public Board Value => Variable.ObjectOnScene;
    }
}