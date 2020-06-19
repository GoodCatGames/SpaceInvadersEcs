using SpaceInvadersLeoEcs.UnityComponents;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.UnityComponent
{
    public static class UnityComponentExtension
    {
        public static IEcsUnityProvider GetProvider(this Component component)
        {
            var gameObject = component.gameObject;
            
            IEcsUnityProvider provider;
            var providerExist = gameObject.TryGetComponent(out provider);
            if (!providerExist)
            {
                provider = gameObject.AddComponent<EcsUnityProvider>();
            }

            return provider;
        }
        
        public static bool HasProvider(this Component component)
        {
            var gameObject = component.gameObject;
            var providerExist = gameObject.TryGetComponent<IEcsUnityProvider>(out var provider);
            return providerExist;
        }
    }
}