using UnityEngine;

namespace SpaceInvadersLeoEcs.UnityComponents
{
    public class LaserRayForGun : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, Vector2.up);
        }
    }
}