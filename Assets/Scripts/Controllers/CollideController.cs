using Simplenoid.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Controllers
{
    /// <summary>
    /// Контроллер управления столкновениями объектов
    /// </summary>
    public class CollideController : BaseController
    {
        [SerializeField] private Level _level;
        /// <summary>
        /// Границы уровня
        /// </summary>
        public Border[] Borders { get; set; }
        /// <summary>
        /// Доска
        /// </summary>
        public Board Board { get; set; }
        /// <summary>
        /// Выбранный уровень
        /// </summary>
        public Level SelectedLevel
        {
            get => _level;
            set
            {
                _level = value;
            }
        }
        /// <summary>
        /// Проверка столкновения Мячика с Блоками
        /// </summary>
        /// <param name="ball">Мячик</param>
        private void CheckBlocks(Ball ball)
        {
            foreach (var block in _level.Blocks)
            {
                if (!block.isAlive)
                {
                    continue;
                }

                if (Collide(ball, block))
                {
                    ball.BumpBlock(block);
                    break;
                }
            }
        }
        /// <summary>
        /// Проверка столкновения Мячика с границами уровня
        /// </summary>
        /// <param name="ball">Мячик</param>
        private void CheckBorders(Ball ball)
        {
            foreach (var border in Borders)
            {
                if (Collide(ball, border))
                {
                    ball.BumpBorder(border);
                    break;
                }
            }
        }
        /// <summary>
        /// Проверка столкновения Доски с границами уровня
        /// </summary>
        /// <param name="board">Доска</param>
        private void CheckBorders(Board board)
        {
            foreach (var border in Borders)
            {
                if (Collide(board, border))
                {
                    board.Stop(border);
                    break;
                }
            }
        }
        /// <summary>
        /// Проверка столкновения бонусов с границами уровня
        /// </summary>
        /// <param name="bonus">Бонус</param>
        private void CheckBorders(Bonus bonus)
        {
            foreach (var border in Borders)
            {
                if (Collide(bonus, border))
                {
                    var bonusController = Toolbox.Instance.Get<BonusController>();
                    bonusController.RemoveBonus(bonus);
                    break;
                }
            }
        }
        /// <summary>
        /// Проверка столкновения Мячика с Доской
        /// </summary>
        /// <param name="ball">Мячик</param>
        private void CheckBoard(Ball ball)
        {
            if (Collide(ball, Board))
            {
                ball.BumpBoard(Board);
            }
        }
        /// <summary>
        /// Проверка столкновения Доски с Бонусом
        /// </summary>
        /// <param name="bonus">Бонус</param>
        private void CheckBoard(Bonus bonus)
        {
            if (!bonus.IsUsed)
            {
                if (Collide(bonus, Board))
                {
                    var bonusController = Toolbox.Instance.Get<BonusController>();
                    bonusController.ActiveBonus(bonus);
                }
            }
        }

        protected override void Update()
        {
            base.Update();

            var ballsController = Toolbox.Instance.Get<BallsController>();
            var bonusesController = Toolbox.Instance.Get<BonusController>();
            var balls = ballsController.Balls;
            var bonuses = bonusesController.Bonuses;
            if ((_level != null) && (balls.Count > 0))
            {
                for (var i = 0; i < balls.Count; i++)
                {
                    var ball = balls[i];
                    CheckBorders(ball);
                    CheckBlocks(ball);
                    CheckBoard(ball);
                }

                CheckBorders(Board);

                for (var i = 0; i < bonuses.Count; i++)
                {
                    var bonus = bonuses[i];
                    CheckBorders(bonus);
                    CheckBoard(bonus);
                }
            }
        }

        /// <summary>
        /// Проверка столкновения запрашивающего объекта с другим объектом
        /// </summary>
        /// <param name="sender">Объект инициирующий проверку</param>
        /// <param name="collideObject">Объект, с которым проводится проверка</param>
        /// <returns>Результат столкновения</returns>
        public static bool Collide(ICollidable sender, ICollidable collideObject)
        {
            var newPos = sender.NextPosition;
            if (collideObject != null)
            {
                if (newPos.x + sender.Size.x > collideObject.Position.x &&
                    newPos.x < collideObject.Position.x + collideObject.Size.x &&
                    newPos.y + sender.Size.y > collideObject.Position.y &&
                    newPos.y < collideObject.Position.y + collideObject.Size.y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}