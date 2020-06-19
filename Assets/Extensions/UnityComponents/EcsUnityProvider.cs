using System;
using Leopotam.Ecs;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.UnityComponents
{
    public class EcsUnityProvider : MonoBehaviour, IEcsUnityProvider
    {
        public ref EcsWorld World
        {
            get
            {
                if(_world == default) throw new Exception("Entity is not assigned!");
                return ref _world;
            }
        }

        private EcsWorld _world;

        
        public ref EcsEntity Entity
        {
            get
            {
                if(_entity == default) throw new Exception("Entity is not assigned!");
                return ref _entity;
            }
        }

        private EcsEntity _entity;

        public void SetEntity(in EcsWorld world, in EcsEntity entity)
        {
            SetWorld(world);
            _entity = entity;
        }

        public void SetWorld(in EcsWorld world)
        {
            _world = world;
        }
    }
}