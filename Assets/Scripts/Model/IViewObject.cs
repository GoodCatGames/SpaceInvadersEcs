using UnityEngine;

namespace Model
{
    public interface IViewObject
    {
        Vector2 Position { get; set; }

        void MoveTo(in Vector2 vector2);
        void Destroy();
    }
}