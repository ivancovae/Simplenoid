using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;
using Simplenoid.Helpers;

namespace Simplenoid
{
    /// <summary>
    /// Компонент объекта Блок
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Block : BaseObjectScene, ISetDamage
    {
        [SerializeField] private int _lives = 1;
        /// <summary>
        /// Размер Блока
        /// </summary>
        public Vector2 Size { get; private set; }

        [SerializeField] private TypesBlocks _type = TypesBlocks.None;
        /// <summary>
        /// Тип блока
        /// </summary>
        public TypesBlocks Type => _type;
        [SerializeField] private TypesBonuses _typeBonus = TypesBonuses.None;
        /// <summary>
        /// Тип бонуса
        /// </summary>
        public TypesBonuses TypeBonus => _typeBonus;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        /// <summary>
        /// Состояние жизни Блока
        /// </summary>
        public bool isAlive => _lives > 0;
        
        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Size = _spriteRenderer.bounds.size;
        }

        /// <summary>
        /// Получение урона
        /// </summary>
        /// <param name="damage">Количество урона</param>
        public void SetDamage(int damage)
        {
            _lives -= damage;
            if (!isAlive)
            {
                InstanceObject.SetActive(false);
            }
        }
    }
}