using System;
using System.Collections;
using System.Collections.Generic;
using Core.Pool;
using Game;
using UnityEngine;
using Zenject;

namespace Game.Table
{
    public class PoolControllerBehaviour : MonoBehaviour
    {
        [field: SerializeField] public float BallRadius { get; private set; } = 0.5f;
        [field: SerializeField] public int BallCount { get; private set; } = 10;
        [field: SerializeField] public Transform StartPoint { get; private set; }
        [field: SerializeField] public Transform MainBallStartPoint { get; private set; }
        [field: SerializeField] public Transform CenterOfTablePoint { get; private set; }
        [field: SerializeField] public BallControllerBehaviour BallPrefab { get; private set; }
        [field: SerializeField] public BallControllerBehaviour MainBall { get; private set; }
        [field: SerializeField] public StickControllerBehaviour Stick { get; private set; }
        [field: SerializeField] public PocketDetector PocketDetector { get; private set; }
        [field: SerializeField] public TrajectoryDrawer TrajectoryDrawer { get; private set; }
    }
}
