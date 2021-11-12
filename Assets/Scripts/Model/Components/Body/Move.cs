using System;
using UnityEngine;

namespace Model.Components.Body
{
    [Serializable]
    public struct Move
    {
        public Vector2 Direct;
        public float Speed;
    }
}