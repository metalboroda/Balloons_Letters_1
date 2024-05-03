using Assets.__Game.Resources.Scripts.SOs;
using System;
using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Spawners
{
  [Serializable]
  public class BalloonSpawnInfo
  {
    public BalloonContainerSo BalloonContainerSo;

    [Space]
    public string BalloonValue;
    public int Amount;
  }
}