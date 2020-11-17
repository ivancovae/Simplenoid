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
        [SerializeField] private UserDataVariable _userDataVariable;

        protected override void Start()
        {
            base.Start();
            Toolbox.Instance.Setup();

            var managerBalls = new ManagerBalls(_userDataVariable.Balls, _userDataVariable.Board);
            var managerBonuses = new ManagerBonuses(managerBalls, _userDataVariable.Bonuses, _userDataVariable.Board);

            // добавление контроллера управления мячиками
            var ballController = Toolbox.Instance.Add<BallsController>();
            ballController.InitController(managerBalls, managerBonuses);

            // добавление контроллера контроля уровней
            var levelController = Toolbox.Instance.Add<LevelController>();
            levelController.InitController(managerBalls, _userDataVariable.Levels);

            // добавление контроллера управления бонусами
            var bonusController = Toolbox.Instance.Add<BonusController>();
            bonusController.InitController(managerBonuses, _userDataVariable.Bonuses);
        }
    }
}