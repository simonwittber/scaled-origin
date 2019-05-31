using UnityEngine;
using System.Collections.Generic;
using System;

namespace SuperSpace
{
    public class ScaledOrigin : MonoBehaviour
    {
        public Rigidbody focus;
        public Camera mainCamera;
        public Camera skyCamera;

        public Rigidbody[] localSpaceObjects;

        public Vector3 movementThisFrame, lastPosition;
        float maxRange;

        [Serializable]
        public struct ScaledPosition
        {
            public double x, y, z;
            public float translationScale;
            public bool isSky;
            public bool isKinematic;
            public Vector3 scale;
            public Rigidbody rigidbody;
            public Transform transform;
            public GameObject gameObject;
        }

        public List<ScaledPosition> scaledPositions = new List<ScaledPosition>();


        void Start()
        {
            maxRange = mainCamera.farClipPlane * 0.9f;
            foreach (var i in localSpaceObjects)
            {
                var sp = new ScaledPosition() { rigidbody = i, transform = i.transform, gameObject = i.gameObject };
                sp.isKinematic = i.isKinematic;
                var p = i.position;
                sp.x = p.x;
                sp.y = p.y;
                sp.z = p.z;
                sp.scale = i.transform.localScale;
                sp.translationScale = 1f;
                sp.isSky = false;
                scaledPositions.Add(sp);
            }
        }

        void FixedUpdate()
        {
            movementThisFrame = focus.position;
            focus.position = Vector3.zero;
            CheckForOOB();

        }
        void CheckForOOB()
        {
            for (var i = 0; i < scaledPositions.Count; i++)
            {
                var sp = scaledPositions[i];
                sp.x -= movementThisFrame.x;
                sp.y -= movementThisFrame.y;
                sp.z -= movementThisFrame.z;
                var isOOB = (Math.Abs(sp.x) > maxRange || Math.Abs(sp.y) > maxRange || Math.Abs(sp.z) > maxRange);
                if (sp.isSky)
                {
                    if (!isOOB)
                    {
                        //return to local space, restore settings.
                        sp.isSky = false;
                        sp.translationScale = 1f;
                        sp.transform.localScale = sp.scale;
                        sp.gameObject.layer = mainCamera.gameObject.layer;
                        sp.rigidbody.isKinematic = sp.isKinematic;
                    }
                }
                else
                {
                    if (isOOB)
                    {
                        //send to sky space
                        sp.isSky = true;
                        sp.translationScale = 1f / maxRange;
                        sp.transform.localScale = sp.scale * sp.translationScale;
                        sp.gameObject.layer = skyCamera.gameObject.layer;
                        sp.rigidbody.isKinematic = true;
                    }
                }

                sp.transform.position = new Vector3((float)sp.x * sp.translationScale, (float)sp.y * sp.translationScale, (float)sp.z * sp.translationScale);
                sp.rigidbody.MovePosition(sp.transform.position);
                scaledPositions[i] = sp;
            }
        }
    }
}