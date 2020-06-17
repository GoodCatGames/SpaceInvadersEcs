using System.Collections.Generic;

namespace SpaceInvadersLeoEcs.Extensions.Components
{
    internal struct ContainerComponents<T> 
        where T : struct
    {
        public IList<T> List => _list ?? (_list = new List<T>());
        private List<T> _list;
    }
}