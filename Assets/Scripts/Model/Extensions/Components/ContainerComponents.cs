using System.Collections.Generic;

namespace Model.Extensions.Components
{
    public struct ContainerComponents<T> 
        where T : struct
    {
        public List<T> List => _list ??= new List<T>();
        private List<T> _list;
    }
}