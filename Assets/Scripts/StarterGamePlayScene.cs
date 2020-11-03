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
        [SerializeField] private Level[] _levels;
        [SerializeField] private Ball _prefabBall;
        [SerializeField] private Board _board;
        [SerializeField] private Border[] _borders;
        [SerializeField] private Bonus[] _bonuses;

        protected override void Start()
        {
            base.Start();
            Toolbox.Instance.Setup();
            
            // добавление контроллера проверки столкновений
            var collideController = Toolbox.Instance.Add<CollideController>();
            collideController.Borders = _borders;
            collideController.Board = _board;

            // добавление контроллера контроля уровней
            var levelController = Toolbox.Instance.Add<LevelController>();
            levelController.InitController(_board, _levels);

            // добавление контроллера управления мячиками
            var ballController = Toolbox.Instance.Add<BallsController>();
            ballController.InitController(_prefabBall, 3.0f);
            ballController.InstantiateBall(_board);

            // добавление контроллера управления бонусами
            var bonusController = Toolbox.Instance.Add<BonusController>();
            bonusController.InitController(_bonuses, _board);
        }
    }
}