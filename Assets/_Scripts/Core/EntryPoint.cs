using System.Collections;
using System.Collections.Generic;
using Core.States;
using Game.Table;
using UnityEngine;
using Zenject;

namespace Core
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] private readonly StatesInstaller _statesInstaller;

        void Start()
        {
            Application.targetFrameRate = 60;
            _statesInstaller.InitStates();
            _statesInstaller.StartApplication();
        }
    }
}

