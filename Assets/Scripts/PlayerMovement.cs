using System;
using UnityEngine;

namespace core.eventsystem
{
    public class PlayerMovement : MonoBehaviour,IMoveable
    {
        public float moveSpeed = 5f;  // Скорость движения танка
        public float rotationSpeed = 150f;  // Скорость вращения танка

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            HandleMovement();
            HandleShooting();
        }

        private void HandleMovement()
        {
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotation, 0);

            float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            Vector3 forward = transform.forward * move;

            rb.velocity = forward;

            if (move == 0)
            {
                rb.velocity = Vector3.zero;
            }
        }

        private void HandleShooting() {
        }
        

        public void Rotation() {
        }

        public void BackMove()
        {
        }

        public void ForwardMove()
        {
        }
    }
}