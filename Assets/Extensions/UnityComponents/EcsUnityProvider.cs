using System;
using Leopotam.Ecs;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.UnityComponents
{
    public class EcsUnityProvider : MonoBehaviour, IEcsUnityProvider
    {
        public ref EcsEntity Entity
        {
            get
            {
                if(_entity.AreEquals(default)) throw new Exception("Entity is not assigned!");
                return ref _entity;
            }
        }

        private EcsEntity _entity;

        public void SetEntity(in EcsEntity entity) => _entity = entity;
    }
}