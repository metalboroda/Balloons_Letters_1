using Assets.__Game.Scripts.Game.States;
using Assets.__Game.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Game.States
{
  public class GamePauseState : GameBaseState
  {
    public GamePauseState(GameBootstrapper gameBootstrapper) : base(gameBootstrapper)
    {
    }

    public override void Enter()
    {
      Time.timeScale = 0f;
    }

    public override void Exit()
    {
      Time.timeScale = 1f;
    }
  }
}