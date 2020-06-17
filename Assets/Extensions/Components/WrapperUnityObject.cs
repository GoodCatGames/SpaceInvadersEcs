using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.Components
{
    public struct WrapperUnityObject<T>
        where T : Object
    {
        public T Value;
    }
}