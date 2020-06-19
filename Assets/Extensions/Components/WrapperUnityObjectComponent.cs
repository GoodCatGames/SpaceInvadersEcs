using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.Components
{
    public struct WrapperUnityObjectComponent<T>
        where T : Object
    {
        public T Value;
    }
}