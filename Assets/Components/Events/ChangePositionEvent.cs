
using UnityEngine;

namespace SpaceInvadersLeoEcs.Components.Events
{
    internal struct ChangePositionEvent
    {
        public Vector2 positionOld;
        public Vector2 positionNew;
    }
}