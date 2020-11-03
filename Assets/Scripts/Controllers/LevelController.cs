using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Simplenoid.Controllers
{
    /// <summary>
    /// Контроллер управления уровнями
    /// </summary>
    public class LevelController : BaseController
    {
        private Level[] _levels;

        [SerializeField] private int _selectedIndexLevel;
        [SerializeField] private Level _selectedLevel;
        [SerializeField] private Board _board;
        /// <summary>
        /// Инициализация контроллера
        /// </summary>
        /// <param name="board">Доска</param>
        /// <param name="levels">Уровни</param>
        public void InitController(Board board, Level[] levels)
        {
            _board = board;
            _levels = levels;
            _selectedIndexLevel = 0;
            _selectedLevel = _levels[_selectedIndexLevel];
            var colliderController = Toolbox.Instance.Add<CollideController>();
            colliderController.SelectedLevel = _selectedLevel;
        }
        private void ChangeLevel()
        {
            _selectedLevel.gameObject.SetActive(false);
            _selectedIndexLevel++;
            if (_selectedIndexLevel == _levels.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }
            _selectedLevel = _levels[_selectedIndexLevel];
            _selectedLevel.gameObject.SetActive(true);
            Toolbox.Instance.Add<CollideController>().SelectedLevel = _selectedLevel;
        }
        /// <summary>
        /// Проверка завершенности уровня
        /// </summary>
        public void CheckLevel()
        {
            if (_selectedLevel.Blocks.Where(b => b.isAlive).Count() == 0)
            {
                ChangeLevel();
                var ballsController = Toolbox.Instance.Get<BallsController>();
                ballsController.ClearBalls();
                ballsController.InstantiateBall(_board);
            }
        }
    }
}