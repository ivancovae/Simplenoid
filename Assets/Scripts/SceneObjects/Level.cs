using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Компонент уровня
    /// </summary>
    public class Level : BaseObjectScene
    {
        [SerializeField] private List<LevelLine> _levelLines = new List<LevelLine>();
        /// <summary>
        /// Блоки на уровне(Блоки по всем линиям)
        /// </summary>
        public IReadOnlyList<Block> Blocks 
        {
            get
            {
                var list = new List<Block>();

                for(var i = 0; i < _levelLines.Count; i++)
                {
                    var levelLine = _levelLines[i];
                    list.AddRange(levelLine.Blocks);
                }
                return list;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            foreach (Transform child in Transform)
            {
                var levelLine = child.GetComponentInChildren<LevelLine>();
                if (levelLine != null)
                {
                    _levelLines.Add(levelLine);
                }
            }
        }
    }
}