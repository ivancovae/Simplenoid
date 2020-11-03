using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Simplenoid.Controllers
{
    /// <summary>
    /// Контроллер управления шарами
    /// </summary>
    public class BallsController : BaseController
    {
        [SerializeField] private List<Ball> _balls = new List<Ball>();
        /// <summary>
        /// Активные шары
        /// </summary>
        public IReadOnlyList<Ball> Balls => _balls;

        private Ball _prefabBall;
        /// <summary>
        /// Режим ускорения шара
        /// </summary>
        public bool IsFaster { get; set; } = false;
        /// <summary>
        /// Режим замедления шара
        /// </summary>
        public bool IsSlowly { get; set; } = false;
        /// <summary>
        /// Делитель скорости
        /// </summary>
        public float Divider { get; private set; }

        protected override void Update()
        {
            base.Update();

            foreach (var ball in Balls)
            {
                ball.Move(Time.deltaTime);
            }
        }
        /// <summary>
        /// Инициализация контроллера
        /// </summary>
        /// <param name="prefabBall">Префаб шарика</param>
        /// <param name="divider">Делитель скорости</param>
        public void InitController(Ball prefabBall, float divider = 2.0f)
        {
            _prefabBall = prefabBall;
            Divider = divider;
        }
        /// <summary>
        /// Позиция мячика по середине доски
        /// </summary>
        /// <param name="board">Доска</param>
        /// <param name="ball">Мячик</param>
        /// <returns></returns>
        public Vector3 GetDefaultPosition(Board board, Ball ball)
        {
            return new Vector3(board.Position.x + board.Size.x / 2 - ball.Size.x / 2, board.Position.y + board.Size.y, 0.0f);
        }
        /// <summary>
        /// Добавление шарика
        /// </summary>
        /// <param name="board">Доска</param>
        public void InstantiateBall(Board board)
        {
            var ball = Instantiate(_prefabBall, Vector3.zero, Quaternion.identity);
            ball.Position = GetDefaultPosition(board, ball);
            board.Ball = ball;
            _balls.Add(ball);
        }
        /// <summary>
        /// Удаление шарика
        /// </summary>
        /// <param name="ball">Мячик</param>
        public void RemoveBall(Ball ball)
        {
            ball.InstanceObject.SetActive(false);
            _balls.Remove(ball);
            if (_balls.Count == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        /// <summary>
        /// Удаление всех мячиков
        /// </summary>
        public void ClearBalls()
        {
            foreach (var ball in Balls)
            {
                ball.InstanceObject.SetActive(false);
                Destroy(ball);
            }
            _balls.Clear();
        }
    }
}