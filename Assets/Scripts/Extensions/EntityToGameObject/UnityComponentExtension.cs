using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.EntityToGameObject
{
    public static class UnityComponentExtension
    {
        public static EcsUnityProvider GetProvider(this Component component)
        {
            var gameObject = component.gameObject;

            var providerExist = gameObject.TryGetComponent(out EcsUnityProvider provider);
            if (!providerExist)
            {
                provider = gameObject.AddComponent<EcsUnityProvider>();
            }

            return provider;
        }
        
        public static bool HasProvider(this Component component)
        {
            var gameObject = component.gameObject;
            var providerExist = gameObject.TryGetComponent<EcsUnityProvider>(out _);
            return providerExist;
        }
    }
}