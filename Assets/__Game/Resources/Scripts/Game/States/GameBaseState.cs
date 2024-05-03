using Assets.__Game.Resources.Scripts.StateMachine;
using Assets.__Game.Scripts.Infrastructure;

namespace Assets.__Game.Scripts.Game.States
{
  public abstract class GameBaseState : State
  {
    protected GameBootstrapper GameBootstrapper;
    protected FiniteStateMachine StateMachine;
    protected SceneLoader SceneLoader;

    protected GameBaseState(GameBootstrapper gameBootstrapper)
    {
      GameBootstrapper = gameBootstrapper;
      StateMachine = GameBootstrapper.StateMachine;
      SceneLoader = GameBootstrapper.SceneLoader;
    }
  }
}