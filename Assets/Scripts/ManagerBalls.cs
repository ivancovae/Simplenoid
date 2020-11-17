
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Simplenoid
{
    public class ManagerBalls
    {
        private BallsVariable _balls;
        private BoardReference _board;

        public ManagerBalls(BallsVariable balls, BoardReference board)
        {
            _balls = balls;
            _board = board;
        }

        private Vector3 GetDefaultPosition(Ball ball) => new Vector3(_board.Value.Position.x + _board.Value.Size.x / 2 - ball.Size.x / 2, _board.Value.Position.y + _board.Value.Size.y, 0.0f);
        
        public void InstantiateBall()
        {
            var ball = GameObject.Instantiate(_balls.PrefabBall, Vector3.zero, Quaternion.identity);
            ball.Position = GetDefaultPosition(ball);
            _board.Value.Ball = ball;
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