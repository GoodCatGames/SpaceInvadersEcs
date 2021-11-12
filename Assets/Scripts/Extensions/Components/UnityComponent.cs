using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.Components
{
    public struct UnityComponent<T>
        where T : Object
    {
        public T Value;
    }
}