using Assets.__Game.Scripts.Game.States;
using Assets.__Game.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Game.States
{
  public class GameplayState : GameBaseState
  {
    public GameplayState(GameBootstrapper gameBootstrapper) : base(gameBootstrapper)
    {
    }

    public override void Enter()
    {
      Time.timeScale = 1f;
    }
  }
}