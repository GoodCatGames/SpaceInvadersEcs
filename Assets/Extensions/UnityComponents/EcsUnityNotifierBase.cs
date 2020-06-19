using System;
using Leopotam.Ecs;
using UnityEngine;

namespace SpaceInvadersLeoEcs.UnityComponents
{
    public abstract class EcsUnityNotifierBase : MonoBehaviour
    {
        protected EcsEntity Entity => Provider.Entity;
        protected EcsWorld World => Provider.World;

        private EcsUnityProvider Provider
        {
            get
            {
                if (_provider != null) return _provider;
                if (!TryGetComponent(out _provider))
                {
                    throw new Exception();
                }

                return _provider;
            }
        }

        private EcsUnityProvider _provider;
    }
}