using UnityEngine;

namespace SpaceInvadersLeoEcs.Pooling
{
    public class PoolObjectExt : PoolObject
    {
        public Rigidbody2D Rigidbody2D { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}