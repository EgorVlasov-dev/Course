using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds : MonoBehaviour
    {
        private Bounds _bounds;

        private void Start()
        {
            var bottomLeftCorner = Camera.main.ViewportToWorldPoint (
                new Vector3 (0,0,Camera.main.nearClipPlane));
            
            var topRightCorner = Camera.main.ViewportToWorldPoint (
                new Vector3 (1,1,Camera.main.nearClipPlane));
            
            Vector3 center = (bottomLeftCorner + topRightCorner) / 2;
            Vector3 size = topRightCorner - bottomLeftCorner;

            _bounds = new Bounds(center, size);
        }

        public bool InBounds(Vector3 position)
        {   
            return _bounds.Contains(position);
        }
    }
}