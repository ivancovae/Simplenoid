
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Simplenoid.Interface;

namespace Simplenoid
{
    /// <summary>
    /// Менеджер шаров
    /// </summary>
    public class ManagerBalls
    {
        private BallsVariable _balls;
        private BoardVariable _board;

        public ManagerBalls(IBallsData data)
        {
            _balls = data.GetBalls;
            _board = data.GetBoard;
        }

        private Vector3 GetDefaultPosition(Ball ball) => new Vector3(_board.ObjectOnScene.Position.x + _board.ObjectOnScene.Size.x / 2 - ball.Size.x / 2, _board.ObjectOnScene.Position.y + _board.ObjectOnScene.Size.y, 0.0f);
        
        public void InstantiateBall()
        {
            if (_board.ObjectOnScene.Ball != null)
            {
                Jump(_board.ObjectOnScene.Ball);
                _board.ObjectOnScene.Ball = null;
            }
            var ball = GameObject.Instantiate(_balls.PrefabBall, Vector3.zero, Quaternion.identity);
            ball.Position = GetDefaultPosition(ball);
            _board.ObjectOnScene.Ball = ball;
            _balls.Add(ball);
        }

        public void RemoveBall(Ball ball)
        {
            ball.InstanceObject.SetActive(false);
            _balls.Remove(ball);
            if (_balls.Items.Count == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void ClearBalls()
        {
            foreach (var ball in _balls.Items)
            {
                ball.InstanceObject.SetActive(false);
                GameObject.Destroy(ball);
            }
            _balls.Clear();
        }

        public void Jump(Ball ball)
        {
            ball.Delta = new Vector3(-ball.Velocity.x, ball.Velocity.y, 0.0f);
        }
    }
}