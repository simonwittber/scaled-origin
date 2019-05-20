# scaled-origin
An derivative of floating origin + 3D skybox for implementing massive worlds in Unity.

How does it work?
-----------------

Two Cameras are used, one for near objects, one for far objects.
The far camera renders first, then the near camera renders, only clearing the zbuffer.

When a Transorm exceeds the threshold (usually your farClip plane distances) it is
moved into the far camera layer, and it's scale is reduced by 1.0 / farClipPlane.
Translations to objects in the far layer are also scaled.

When the Transform comes back into bounds, it's scale is restored and it is added to
the near camera layer.
