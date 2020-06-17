using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model.Data
{
    public interface IViewObject
    {
        Vector2 Position { get; set; }
        void MoveTo(Vector2 vector2);

        void Destroy();
    }
}