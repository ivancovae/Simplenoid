using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;
using UnityEngine.SceneManagement;

namespace Simplenoid
{
    /// <summary>
    /// Менеджер уровней
    /// </summary>
    public class ManagerLevels
    {
        private LevelsVariable _levels;
        private BlocksVariable _blocks;
        private Transform _levelObject;

        public ManagerLevels(ILevelsData data, Transform levelObject)
        {
            _levels = data.GetLevels;
            _blocks = data.GetBlocks;
            _levelObject = levelObject;
            _levels.CurrentIndexLevel = 0;
        }

        public void InstantiateLevel()
        {
            if (_levels.ObjectOnScene != null)
            {
                RemoveLevel(_levels.ObjectOnScene);
            }
            var level = GameObject.Instantiate<Level>(_levels.PrefabsLevels[_levels.CurrentIndexLevel]);
            level.Transform.SetParent(_levelObject);
            _levels.ObjectOnScene = level;
            UpdateBlocks();
        }

        public void InstantiateNextLevel()
        {
            _levels.CurrentIndexLevel++;
            if (_levels.CurrentIndexLevel >= _levels.PrefabsLevels.Count)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }
            InstantiateLevel();
        }

        public void RemoveLevel(Level level)
        {
            level.InstanceObject.SetActive(false);
            _levels.ObjectOnScene = null;
        }

        private void UpdateBlocks()
        {
            _blocks.Clear();
            if (_levels.ObjectOnScene != null)
            {
                var blocks = _levels.ObjectOnScene.Blocks;
                foreach (var block in blocks)
                {
                    _blocks.Add(block);
                }
            }
        }
    }
}