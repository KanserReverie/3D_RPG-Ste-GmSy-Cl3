using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Errors : 17
namespace Debugging.Player
{
    [AddComponentMenu("RPG/Player/Mouse Look")]
    public class MouseLook : MonoBehaviour
    {
        // enum, can only be one thing at once.
        // own custom data type.
        // X = on the object and Y on the cammera
        public enum RotationalAxis
        {
            MouseX,
            MouseY
        }
        [Header("Rotation Variables")]

        public RotationalAxis axis = RotationalAxis.MouseX;
        // This means it will be a slider.
        [Range(0,200)]
        public float sensitivity = 100;
        public float minY = -60, maxY = 60;
        private float _rotY;


        // Header put everything under one thing.
        [Header("Something Else")]
        public int Useless_Int;

        void Start()
        {
            if(GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().freezeRotation = true;
            }
            ////////////////////////Cursor.lockState = CursorLockMode.Locked;
            ////////////////////////Cursor.visible = false;
            if(GetComponent<Camera>())
            {
                axis = RotationalAxis.MouseY;
            }
        }
        void Update()
        {
            switch (axis)
            {
                case RotationalAxis.MouseX:
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0);
                    break;
                case RotationalAxis.MouseY:
                    _rotY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
                    _rotY = Mathf.Clamp(_rotY, minY, maxY);
                    transform.localEulerAngles = new Vector3(-_rotY, 0.0f);
                    break;
                default:
                    break;
            }

            // Old code but since it is an enum I am using an enum.
            //if(axis == RotationalAxis.MouseX)
            //{
            //    transform.Rotate(0,Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime,0);
            //}
            //else
            //{
            //    _rotY += Input.GetAxis("Mouse Y")  * sensitivity * Time.deltaTime;
            //    _rotY = Mathf.Clamp(_rotY, minY,maxY);
            //    transform.localEulerAngles = new Vector3(-_rotY,0.0f);
            //}
        }
    }   
}