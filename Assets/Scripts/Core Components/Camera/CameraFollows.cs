using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Actors
{
    /// <summary>
    /// Make a camera follow this actor.
    /// </summary>
    public class CameraFollows : MonoBehaviour
    {
        [SerializeField] private Camera targetCamera;       // Assigned in Start.

        [SerializeField] private bool isTethered;           // Editor configurable.
        [SerializeField] private Vector2 maxDistance;       // Editor configurable.
        [SerializeField] private float followSpeed;         // Editor configurable.

        public Camera TargetCamera => targetCamera;

        private void Update()
        {
            // Camera stays put if it's not tethered.
            if (!isTethered)
                return;

            Vector2 distance = (Vector2)transform.position - (Vector2)targetCamera.transform.position;
            if (Mathf.Abs(distance.x) < maxDistance.x || Mathf.Abs(distance.y) < maxDistance.y)
                return;

            Vector3 position = Vector3.Slerp(targetCamera.transform.position, transform.position, followSpeed * Time.deltaTime);
            position.z = -1;
            targetCamera.transform.position = position;
        }

        private void Start()
        {
            targetCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            targetCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
    }
}