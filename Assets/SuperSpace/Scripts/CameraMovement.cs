using UnityEngine;

namespace SuperSpace
{
    public class CameraMovement : MonoBehaviour
    {
        public float distance = 1000;

        new Rigidbody rigidbody;


        void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            rigidbody.position += (new Vector3(0, 0, Input.GetAxis("Vertical") * distance));
        }
    }
}