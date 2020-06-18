using System;
using SpaceInvadersLeoEcs.Blueprints;
using UnityEngine;

namespace SpaceInvadersLeoEcs.AppData
{
    [Serializable]
    public class PlayerConfig
    {
        //public Transform Transform;
        public GunBlueprint GunPlayer;
        public float Speed;
    }
}