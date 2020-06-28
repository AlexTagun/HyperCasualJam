using UnityEngine;

namespace LazyMap
{
    public struct RectZone {
        public RectZone(Vector2 inCenter, Vector2 inSize) {
            _unityBounds = new Bounds(inCenter, inSize);
        }

        public RectZone(Bounds inUnityBounds)
            : this(inUnityBounds.center, inUnityBounds.extents * 2) { }

        public static implicit operator RectZone(Bounds inUnityBounds) => new RectZone(inUnityBounds);

        public void merge(RectZone inOtherZone) {
            _unityBounds.Encapsulate(inOtherZone._unityBounds);
        }

        public bool isCollide(RectZone inOtherZone) {
            return _unityBounds.Intersects(inOtherZone._unityBounds);
        }

        public Vector2 center { get { return _unityBounds.center; } set { _unityBounds.center = value; } }

        public void print(string inName) {
            Debug.Log("LazyMap.RectZone[" + inName + "]{ center: " + _unityBounds.center.ToString() +
                    " | extents: " + _unityBounds.extents.ToString() + " }");
        }

        public void draw(Color inColor, float inDuration = 0f) {
            Vector2 theMin = _unityBounds.min;
            Vector2 theMax = _unityBounds.max;

            Debug.DrawLine(theMin, new Vector2(theMin.x, theMax.y), inColor, inDuration);
            Debug.DrawLine(new Vector2(theMin.x, theMax.y), theMax, inColor, inDuration);
            Debug.DrawLine(theMax, new Vector2(theMax.x, theMin.y), inColor, inDuration);
            Debug.DrawLine(new Vector2(theMax.x, theMin.y), theMin, inColor, inDuration);
        }

        private Bounds _unityBounds;
    }
}
