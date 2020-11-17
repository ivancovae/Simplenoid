using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Simplenoid.Controllers;

namespace Simplenoid
{
    /// <summary>
    /// Предустановка игровой сцены
    /// </summary>
    public class StarterGamePlayScene : Starter
    {

        [SerializeField] private BallsVariable _balls;
        [SerializeField] private BoardReference _board;
        [SerializeField] private BlocksVariable _blocks;
        [SerializeField] private BordersReference _borders;
        [SerializeField] private LevelsReference _levels;
        [SerializeField] private BonusesVariable _bonuses;

        [SerializeField] private BoolVariable _isFaster;
        [SerializeField] private BoolVariable _isSlowly;
        [SerializeField] private FloatVariable _divider;
        [SerializeField] private FloatVariable _activeTime;

        protected override void Start()
        {
            base.Start();
            Toolbox.Instance.Setup();

            var managerBalls = new ManagerBalls(_balls, _board);
            var managerBonuses = new ManagerBonuses(managerBalls, _bonuses, _board);

            // добавление контроллера управления мячиками
            var ballController = Toolbox.Instance.Add<BallsController>();
            ballController.InitController(managerBalls, managerBonuses);

            // добавление контроллера контроля уровней
            var levelController = Toolbox.Instance.Add<LevelController>();
            levelController.InitController(managerBalls, _board, _levels);

            // добавление контроллера управления бонусами
            var bonusController = Toolbox.Instance.Add<BonusController>();
            bonusController.InitController(managerBonuses, _bonuses);
        }
    }
}