using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    public class CameraFollows : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        public Camera Camera { get { return camera; } }

        // ==================================================
        // ==================== SETTINGS ====================
        // ==================================================
        [SerializeField] private bool isTethered;
        [SerializeField] private Vector2 maxDistance;
        [SerializeField] private float followSpeed;

        private void Update()
        {
            // Camera stays put if it's not tethered.
            if (!isTethered)
                return;

            Vector2 distance = (Vector2)transform.position - (Vector2)camera.transform.position;
            if (Mathf.Abs(distance.x) <= maxDistance.x || Mathf.Abs(distance.y) <= maxDistance.y)
                return;

            Vector3 position = Vector3.Slerp(camera.transform.position, transform.position, followSpeed * Time.deltaTime);
            position.z = -1;
            camera.transform.position = position;
        }

        private void Start()
        {
            camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
    }
}