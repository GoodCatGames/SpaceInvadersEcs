using System;
using UnityEngine.Serialization;

namespace Model.Components.Body
{
    [Serializable]
    public struct Health
    {
        public int Initial;
        public int Current;
    }
}