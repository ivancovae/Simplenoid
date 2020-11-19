using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

using Simplenoid.Interface;

namespace Simplenoid.Controllers
{
    public class LevelController : BaseController
    {
        private LevelsVariable _levels;
        private BlocksVariable _blocks;
        private ManagerBalls _managerBalls;
        private ManagerLevels _managerLevels;
        private ManagerBonuses _managerBonuses;

        protected override void Update()
        {
            base.Update();

            CheckLevel();
        }
        
        public void InitController(ManagerBalls managerBalls, ManagerLevels managerLevels, ManagerBonuses managerBonuses, ILevelsData data)
        {
            _managerBalls = managerBalls;
            _managerLevels = managerLevels;
            _managerBonuses = managerBonuses;
            _levels = data.GetLevels;
            _blocks = data.GetBlocks;

            _levels.CurrentIndexLevel = 0;

            _managerLevels.InstantiateLevel();
        }
        public void CheckLevel()
        {
            if (_levels.ObjectOnScene != null)
            {
                if (_levels.ObjectOnScene.Blocks.Where(b => b.isAlive).Count() == 0)
                {
                    _managerBalls.ClearBalls();
                    _managerBonuses.ClearBonuses();
                    _managerLevels.InstantiateNextLevel();
                    _managerBalls.InstantiateBall();
                }
            }
        }
    }
}