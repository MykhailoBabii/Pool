using Core;
using Core.States;
using Game;
using Game.Provider;
using Game.States;
using Game.Stick;
using Game.Table;
using UI;
using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    [SerializeField] private EntryPoint _entryPoint;
    [SerializeField] private StatesInstaller _statesInstaller;
    [SerializeField] private PoolControllerBehaviour _poolControllerBehaviour;
    [SerializeField] private ScreenView _screenView;
    [SerializeField] private ScreenTouchHandler _screenTouchHandler;
    [SerializeField] private CoroutineManager _coroutineManager;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Settings _settings;
    [SerializeField] private BallColorSettings _ballColorSettings;
    
    
    
    private IStateMachine<ApplicationStates> _applicationStateMachine = new BaseStateMachine<ApplicationStates>();

    public override void InstallBindings()
    {
        InitStates();
        InitControllers();
        InitBehaviours();
        InitScriptableObjects();
    }

    private void InitStates()
    {
        Container.Bind<StatesInstaller>().AsSingle();
        Container.Bind<IStateMachine<ApplicationStates>>().FromInstance(_applicationStateMachine).AsSingle();

        Container.Bind<StartState>().AsSingle();
        Container.Bind<ReadyState>().AsSingle();
        Container.Bind<BallsMovingState>().AsSingle();
        Container.Bind<GameOverState>().AsSingle();
    }

    private void InitControllers()
    {
        Container.Bind<ITableController>().To<TableController>().AsSingle();
        Container.Bind<IStickController>().To<StickController>().AsSingle();
    }

    private void InitBehaviours()
    {
        Container.Bind<EntryPoint>().FromInstance(_entryPoint).AsSingle();
        Container.Bind<CoroutineManager>().FromInstance(_coroutineManager).AsSingle();
        Container.Bind<ScreenView>().FromInstance(_screenView).AsSingle();
        Container.Bind<PoolControllerBehaviour>().FromInstance(_poolControllerBehaviour).AsSingle();
        Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle();
        Container.Bind<ScreenTouchHandler>().FromInstance(_screenTouchHandler).AsSingle();
    }

    private void InitScriptableObjects()
    {
        Container.Bind<Settings>().FromInstance(_settings).AsSingle();
        Container.Bind<BallColorSettings>().FromInstance(_ballColorSettings).AsSingle();
    }
}
