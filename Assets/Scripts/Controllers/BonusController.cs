using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Helpers;
using System.Linq;

namespace Simplenoid.Controllers
{
    /// <summary>
    /// Контроллер управления бонусами
    /// </summary>
    public class BonusController : BaseController
    {
        [SerializeField] private List<Bonus> _bonuses = new List<Bonus>();
        /// <summary>
        /// Активные бонусы
        /// </summary>
        public IReadOnlyList<Bonus> Bonuses => _bonuses;

        private List<Bonus> _prefabBonus = new List<Bonus>();
        private Board _board;
        /// <summary>
        /// Время работы бонуса
        /// </summary>
        public float ActiveTime { get; private set; } = 3.0f;

        protected override void Update()
        {
            base.Update();

            foreach (var bonus in Bonuses)
            {
                bonus.Move(Time.deltaTime);
            }
        }
        /// <summary>
        /// Инициализация контроллера
        /// </summary>
        /// <param name="prefabsBonus">Префаб бонуса</param>
        /// <param name="board">Доска</param>
        public void InitController(Bonus[] prefabsBonus, Board board)
        {
            _prefabBonus.AddRange(prefabsBonus);
            _board = board;
        }
        /// <summary>
        /// Позиция по середине блока
        /// </summary>
        /// <param name="block">Блок</param>
        /// <param name="bonus">Бонус</param>
        /// <returns></returns>
        public Vector3 GetDefaultPosition(Block block, Bonus bonus)
        {
            return new Vector3(block.Position.x + (block.Size.x / 2) - (bonus.Size.x / 2), block.Position.y + (block.Size.y / 2) - (bonus.Size.y / 2), 0.0f);
        }
        /// <summary>
        /// Создание бонуса
        /// </summary>
        /// <param name="block">Блок</param>
        public void InstantiateBonus(Block block)
        {
            if (_prefabBonus.Any(b => b.Type == block.TypeBonus))
            {
                var prefabBonus = _prefabBonus.Where(b => b.Type == block.TypeBonus).First();
                var bonus = Instantiate(prefabBonus, Vector3.zero, Quaternion.identity);
                bonus.Position = GetDefaultPosition(block, bonus);
                _bonuses.Add(bonus);
            }
        }
        /// <summary>
        /// Удаление бонуса
        /// </summary>
        /// <param name="bonus"></param>
        public void RemoveBonus(Bonus bonus)
        {
            bonus.InstanceObject.SetActive(false);
            _bonuses.Remove(bonus);
        }
        /// <summary>
        /// Активация бонуса
        /// </summary>
        /// <param name="bonus">Бонус</param>
        public void ActiveBonus(Bonus bonus)
        {
            if (!bonus.IsUsed)
            {
                bonus.UseBonus();
                switch (bonus.Type)
                {
                    case TypesBonuses.DoubleBalls:
                        {
                            ActiveDoubleBall();
                        }
                        break;
                    case TypesBonuses.FasterBalls:
                        {
                            ActiveFast();
                        }
                        break;
                    case TypesBonuses.LongBoard:
                        {
                            ActiveLong();
                        }
                        break;
                    case TypesBonuses.SlowerBalls:
                        {
                            ActiveSlow();
                        }
                        break;
                    default:
                        {
                            Debug.LogError("Unknown type bonus");
                        }
                        break;
                }
                bonus.InstanceObject.SetActive(false);
            }
        }

        #region Bonuses Action
        private void DeactiveSlow()
        {
            var ballsController = Toolbox.Instance.Get<BallsController>();
            ballsController.IsSlowly = false;
        }

        private void ActiveSlow()
        {
            var ballsController = Toolbox.Instance.Get<BallsController>();
            ballsController.IsSlowly = true;
            Invoke("DeactiveSlow", ActiveTime);
        }

        private void DeactiveFast()
        {
            var ballsController = Toolbox.Instance.Get<BallsController>();
            ballsController.IsFaster = false;
        }

        private void ActiveFast()
        {
            var ballsController = Toolbox.Instance.Get<BallsController>();
            ballsController.IsFaster = true;
            Invoke("DeactiveFast", ActiveTime);
        }

        private void DeactiveLong()
        {
            _board.IsLongBoard = false;
        }

        private void ActiveLong()
        {
            _board.IsLongBoard = true;
            Invoke("DeactiveLong", ActiveTime);
        }

        private void ActiveDoubleBall()
        {
            var ballsController = Toolbox.Instance.Get<BallsController>();
            ballsController.InstantiateBall(_board);
        }
        #endregion
    }
}