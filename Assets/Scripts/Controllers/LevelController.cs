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
        private LevelsVariable _levels;

        [SerializeField] private int _selectedIndexLevel;
        [SerializeField] private Level _selectedLevel;

        private ManagerBalls _managerBalls;
        /// <summary>
        /// Инициализация контроллера
        /// </summary>
        /// <param name="board">Доска</param>
        /// <param name="levels">Уровни</param>
        public void InitController(ManagerBalls managerBalls, LevelsVariable levels)
        {
            _managerBalls = managerBalls;
            _levels = levels;
            _selectedIndexLevel = 0;
            _selectedLevel = _levels.Items[_selectedIndexLevel];
        }
        private void ChangeLevel()
        {
            _selectedLevel.gameObject.SetActive(false);
            _selectedIndexLevel++;
            if (_selectedIndexLevel == _levels.Items.Count)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }
            _selectedLevel = _levels.Items[_selectedIndexLevel];
            _selectedLevel.gameObject.SetActive(true);
        }
        /// <summary>
        /// Проверка завершенности уровня
        /// </summary>
        public void CheckLevel()
        {
            if (_selectedLevel.Blocks.Where(b => b.isAlive).Count() == 0)
            {
                ChangeLevel();
                _managerBalls.ClearBalls();
                _managerBalls.InstantiateBall();
            }
        }
    }
}