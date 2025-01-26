using System;
using UnityEngine;

namespace core.eventsystem
{
    public class PlayerMovement : MonoBehaviour,IMoveable
    {
        public float moveSpeed = 5f;
        public float rotationSpeed = 150f;

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {
            Rotation();
            Move();
        }


        public void Rotation() {
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotation, 0);
        }

        public void Move() {
            float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            Vector3 forward = transform.forward * move;

            rb.velocity = forward;

            if (move == 0) {
                rb.velocity = Vector3.zero;
            }
        }
    }
}