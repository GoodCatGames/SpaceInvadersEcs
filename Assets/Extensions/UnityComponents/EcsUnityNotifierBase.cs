using System;
using Leopotam.Ecs;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.UnityComponents
{
    public abstract class EcsUnityNotifierBase : MonoBehaviour
    {
        protected ref EcsEntity Entity => ref Provider.Entity;

        private IEcsUnityProvider Provider
        {
            get
            {
                if (_provider != null) return _provider;
                if (!TryGetComponent(out _provider)) throw new Exception();
                return _provider;
            }
        }

        private IEcsUnityProvider _provider;
    }
}