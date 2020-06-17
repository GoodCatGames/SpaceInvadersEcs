using System;
using JetBrains.Annotations;
using SpaceInvadersLeoEcs.Pooling;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpaceInvadersLeoEcs.Systems.Model.Data
{
    public class ViewObjectUnity : IViewObject
    {
        public Vector2 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }

        [NotNull] public Transform Transform { get; }
        [CanBeNull] private readonly Rigidbody2D _rigidbody2D;

        [CanBeNull] private readonly IPoolObject _poolObject;
        
        public ViewObjectUnity([NotNull] Transform transform, [NotNull] Rigidbody2D rigidbody2D,
            IPoolObject poolObject = null) : this(transform, poolObject)
        {
            _rigidbody2D = rigidbody2D;
        }

        public ViewObjectUnity([NotNull] Transform transform, IPoolObject poolObject = null)
        {
            Transform = transform ? transform : throw new ArgumentNullException(nameof(transform));
            _poolObject = poolObject;
        }

        public void MoveTo(Vector2 vector2)
        {
            if (_rigidbody2D != null)
            {
                _rigidbody2D.MovePosition(_rigidbody2D.position + vector2 * Time.fixedDeltaTime);
            }
            else
            {
                Transform.Translate(vector2 * Time.deltaTime);
            }
        }

        public void Destroy()
        {
            if (_poolObject != null)
            {
                _poolObject.PoolTransform.gameObject.SetActive(false);
                _poolObject.PoolRecycle();
            }
            else
            {
                Object.Destroy(Transform.gameObject);
            }
        }
    }
}