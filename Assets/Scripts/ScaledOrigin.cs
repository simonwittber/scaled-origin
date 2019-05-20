using UnityEngine;
using System.Collections.Generic;


public class ScaledOrigin : MonoBehaviour
{
    public Camera mainCamera;
    Vector3 movementThisFrame, lastPosition;
    FloatingComponent[] localSpaceObjects;

    void Start()
    {
        if (mainCamera)
        {
            localSpaceObjects = (FloatingComponent[])GameObject.FindObjectsOfType(typeof(FloatingComponent));
            FloatingComponent.MAXRANGE = mainCamera.farClipPlane * 0.9f;
        }
    }

    void Update()
    {
        if (mainCamera)
        {
            movementThisFrame = mainCamera.transform.position;
            mainCamera.transform.position = Vector3.zero;
            foreach (var i in localSpaceObjects)
                i.Translate(-movementThisFrame);
        }
    }
}
