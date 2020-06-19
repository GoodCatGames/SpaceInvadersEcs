using System;
using Leopotam.Ecs;
using UnityEngine;

namespace SpaceInvadersLeoEcs.UnityComponents
{
    public class EcsUnityProvider : MonoBehaviour, IEcsUnityProvider
    {
        public EcsWorld World
        {
            get
            {
                if(_world == default) throw new Exception("Entity is not assigned!");
                return _world;
            }
            set => _world = value;
        }

        private EcsWorld _world;

        
        public EcsEntity Entity
        {
            get
            {
                if(_entity == default) throw new Exception("Entity is not assigned!");
                return _entity;
            }
            set => _entity = value;
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