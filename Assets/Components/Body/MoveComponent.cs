using System;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Components.Body
{
    [Serializable]
    internal struct MoveComponent
    {
        public Vector2 Direct;
        public float Speed;
    }
}