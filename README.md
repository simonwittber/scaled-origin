# scaled-origin
An derivative of floating origin + 3D skybox for implementing massive worlds in Unity.

How does it work?
-----------------
Two Cameras are used, one for near objects, one for far objects.
The far camera renders first, then the near camera renders, only clearing the zbuffer.

When a Transorm exceeds the threshold (usually about 90% of the farClipPlane distance)
it is moved into the far camera layer, and it's scale is reduced by 1.0 / (0.9*farClipPlane).
Translation/movement on objects in the far layer are also scaled.

When the Transform comes back into bounds, it's scale is restored and it is added to
the near camera layer.


Caveats
-------
The camera is reset to the origin every frame, therefore static colliders should not be
used. Also, all transforms that are rendered need to have a FloatingTransform or 
FloatingRigidbody component.

Translations should be performed using the FloatingComponent methods rather than the
Transform methods.