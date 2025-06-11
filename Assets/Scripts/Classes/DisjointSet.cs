using System.Collections.Generic;

namespace Classes
{
    public class DisjointSet<T> where T : notnull
    {
        // dictionary of sets, where the key is the element and the value is the set it belongs to
        private Dictionary<T, T> _parent = new Dictionary<T, T>();

        // constructor for disjoint set
        // takes a collection of elements and initializes the sets
        public DisjointSet(IEnumerable<T> elements)
        {
            // initialize the sets, each element is its own set
            // this is to make sure that each element is its own parent, meaning it is its own set
            foreach (var element in elements)
            {
                _parent[element] = element;
            }
        }

        // method to find the set of an element
        public T Find(T element)
        {
            //if the element is not it's own parent, we need to find the parent of the element, by recursion
            if (!_parent[element].Equals(element)) _parent[element] = Find(_parent[element]);

            // return the parent of the element, which is the set it belongs to
            return _parent[element];
        }

        // method to union two sets ('add them together')
        public void Union(T element1, T element2)
        {
            // find the sets of the elements
            var root1 = Find(element1);
            var root2 = Find(element2);

            // if they are not the same set, we join them together
            if (!root1.Equals(root2))
            {
                // set the parent of one set to the other, meaning they are now the same set
                _parent[root1] = root2;
            }
        }

        //method to check if two elements are in the same set
        public bool Connected(T element1, T element2) => Find(element1).Equals(Find(element2));
    }
}