using System.Collections;
using UnityEngine;

namespace IsakUtils
{
    public static class TransformExtensions
    {
        public static IEnumerable ChildrenEnumerable(this Transform parent)
        {
            return new TransformChildEnumerable(parent);
        }

        public class TransformChildEnumerable : IEnumerable
        {
            Transform parent;
            public TransformChildEnumerable(Transform transform)
            {
                parent = transform;
            }
            public IEnumerator GetEnumerator()
            {
                return new TransformChildEnumerator(parent);
            }
        }

        public class TransformChildEnumerator : IEnumerator
        {
            private Transform transform;
            private int counter;
            public TransformChildEnumerator(Transform transform)
            {
                this.transform = transform;
                counter = -1;
            }

            public object Current => transform.GetChild(counter);

            public bool MoveNext()
            {
                counter++;
                if (counter < transform.childCount)
                {
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                counter = 0;
            }
        }
    }
}