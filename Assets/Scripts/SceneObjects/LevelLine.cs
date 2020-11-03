using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Компонент Линий блоков на уровне
    /// </summary>
    public class LevelLine : BaseObjectScene
    {
        [SerializeField] private List<Block> _blocks = new List<Block>();
        /// <summary>
        /// Блоки в линии
        /// </summary>
        public IReadOnlyList<Block> Blocks => _blocks;

        protected override void Awake()
        {
            base.Awake();

            foreach (Transform child in Transform)
            {
                var block = child.GetComponentInChildren<Block>();
                if (block != null)
                {
                    _blocks.Add(block);
                }
            }
        }
    }
}