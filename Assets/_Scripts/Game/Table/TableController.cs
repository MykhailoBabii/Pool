using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Core.Pool;
using Core.States;
using Core.Utilities;
using Game.Table;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using Game.Provider;
using DG.Tweening;
using ModestTree;

namespace Game
{
    public interface ITableController
    {
        void Init();
        void SpawnBalls();
        void StartDetectBallsMoving();

    }
    public class TableController : ITableController
    {
        private readonly PoolControllerBehaviour _poolControllerBehaviour;
        private readonly IStateMachine<ApplicationStates> _applicationStateMachine;
        private readonly CoroutineManager _coroutineManager;
        private readonly BallColorSettings _ballColorSettings;
        private readonly List<BallControllerBehaviour> _ballList = new();


        private BallControllerBehaviour _mainBall;

        public TableController(
            PoolControllerBehaviour poolControllerBehaviour,
            CoroutineManager coroutineManager,
            IStateMachine<ApplicationStates> applicationStateMachine,
            BallColorSettings ballColorSettings)
        {
            _poolControllerBehaviour = poolControllerBehaviour;
            _coroutineManager = coroutineManager;
            _applicationStateMachine = applicationStateMachine;
            _ballColorSettings = ballColorSettings;
        }

        public void Init()
        {
            _poolControllerBehaviour.PocketDetector.InitOnMainBallIsDetected(OnMainBallLose);
            _mainBall = _poolControllerBehaviour.MainBall;
        }

        private void OnMainBallLose()
        {
            _applicationStateMachine.SwitchToState(ApplicationStates.Start);
        }

        public void SpawnBalls()
        {
            ResetMainBall();

            var spawnPoints = GetSpawnPointsModel();
            var ballPrefab = _poolControllerBehaviour.BallPrefab;

            if (_ballList.IsEmpty() == true)
            {
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    var ballObject = GameObject.Instantiate(ballPrefab.gameObject, spawnPoints[i], new Quaternion());
                    var ball = ballObject.GetComponent<BallControllerBehaviour>();
                    ball.SetColor(_ballColorSettings.BallColorList[i]);
                    _ballList.Add(ball);
                }
            }

            else
            {
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    var ball = _ballList[i];
                    ball.transform.SetPositionAndRotation(spawnPoints[i], new Quaternion());
                    ball.StopVelocity();
                    ball.gameObject.SetActive(true);
                }
                
            }
        }

        public void StartDetectBallsMoving()
        {
            _coroutineManager.StartCoroutine(BallDetecting());
        }

        private Vector3[] GetSpawnPointsModel()
        {
            var ballRadius = _poolControllerBehaviour.BallRadius;
            var ballCount = _poolControllerBehaviour.BallCount;
            var startPoint = _poolControllerBehaviour.StartPoint.position;
            var spawnPoints = new Vector3[ballCount];
            int rowCount = 4;
            int index = 0;

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col <= row; col++)
                {
                    float xOffset = col * ballRadius * 2 - row * ballRadius;
                    float zOffset = row * ballRadius * Mathf.Sqrt(3);
                    spawnPoints[index] = startPoint + new Vector3(xOffset, 0, zOffset);
                    index++;
                }
            }

            return spawnPoints;
        }

        private void ResetMainBall()
        {
            _mainBall.StopVelocity();
            var mainBallStartPosition = _poolControllerBehaviour.MainBallStartPoint.position;
            _mainBall.transform.position = mainBallStartPosition;
        }

        private IEnumerator BallDetecting()
        {
            yield return new WaitForSeconds(0.1f);
            while (true)
            {
                int ballIsMoving = 0;
                foreach (var ball in _ballList)
                {
                    if (ball.Rb.velocity.magnitude > 0.01f)
                    {
                        ballIsMoving++;
                    }
                }

                if (_mainBall.Rb.velocity.magnitude > 0.01f)
                {
                    ballIsMoving++;
                }

                if (ballIsMoving == 0)
                {
                    _applicationStateMachine.SwitchToState(ApplicationStates.Ready);
                    break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}

