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
        [SerializeField] private Transform _sceneObject;
        [SerializeField] private Transform _levelObject;

        protected override void Start()
        {
            base.Start();
            Toolbox.Instance.Setup();

            var bordersGO = Instantiate(_userDataVariable.Borders.PrefabBorder);
            bordersGO.transform.SetParent(_sceneObject);

            var listBorders = bordersGO.GetComponentsInChildren<Border>();
            foreach (var border in listBorders)
            {
                _userDataVariable.Borders.Add(border);
            }

            _userDataVariable.Board.ObjectOnScene = Instantiate(_userDataVariable.Board.PrefabBoard);
            _userDataVariable.Board.ObjectOnScene.transform.SetParent(_sceneObject);

            _userDataVariable.GetBalls.Clear();

            var managerBalls = new ManagerBalls(_userDataVariable);
            managerBalls.InstantiateBall();
                       
            var managerBonuses = new ManagerBonuses(managerBalls, _userDataVariable);

            var boardController = Toolbox.Instance.Add<BoardController>();
            boardController.InitController(managerBalls, managerBonuses, _userDataVariable);

            var ballController = Toolbox.Instance.Add<BallsController>();
            ballController.InitController(managerBalls, managerBonuses, _userDataVariable);

            //var levelController = Toolbox.Instance.Add<LevelController>();
            //levelController.InitController(managerBalls, _userDataVariable.Levels);

            var bonusController = Toolbox.Instance.Add<BonusController>();
            bonusController.InitController(managerBonuses, _userDataVariable.Bonuses);            
        }
    }
}