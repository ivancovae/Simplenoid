using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;
using Simplenoid.Controllers;
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
        [SerializeField] private Vector2 _size;
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
        /// <summary>
        /// Размер Блока
        /// </summary>
        public Vector2 Size => _size;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        /// <summary>
        /// Состояние жизни Блока
        /// </summary>
        public bool isAlive => _lives > 0;

        /// <summary>
        /// Для статичных объектов следующая позиция равная текущей
        /// </summary>
        public Vector3 NextPosition => Position;

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _size = _spriteRenderer.bounds.size;
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
                if(Type == TypesBlocks.WithBonus)
                {
                    var bonusController = Toolbox.Instance.Get<BonusController>();
                    bonusController.InstantiateBonus(this);
                }
                Toolbox.Instance.Get<LevelController>().CheckLevel();
            }
        }
    }
}