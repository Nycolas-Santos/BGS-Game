using UnityEngine;

namespace Game.Core.Scripts
{
    public class GameCamera : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed = 0.5f;

        private void Start()
        {
            if (target == null)
            {
                target = FindObjectOfType<Player>().transform;
            }
        }

        void LateUpdate()
        {
            if (target != null)
            {
                Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}