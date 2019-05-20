using System;
using UnityEngine;

public class FloatingTransform : FloatingComponent
{
    void Update()
    {
        if (isOOB)
            OutOfBounds();
        else
            InBounds();
    }

    void OutOfBounds()
    {
        isOOB = (Math.Abs(x) > MAXRANGE || Math.Abs(y) > MAXRANGE || Math.Abs(z) > MAXRANGE);
        if (!isOOB)
        {
            gameObject.layer = normalLayer;
            translationScale = 1f;
            transform.localScale = Vector3.one * (float)originalScale;
        }
        transform.position = new Vector3((float)x * translationScale, (float)y * translationScale, (float)z * translationScale);
    }

    void InBounds()
    {
        isOOB = (Math.Abs(x) > MAXRANGE || Math.Abs(y) > MAXRANGE || Math.Abs(z) > MAXRANGE);
        if (isOOB)
        {
            isOOB = true;
            gameObject.layer = skyboxLayer;
            translationScale = 1.0f / MAXRANGE;
            transform.localScale = Vector3.one * (float)originalScale * translationScale;
        }
        transform.position = new Vector3((float)x * translationScale, (float)y * translationScale, (float)z * translationScale);
    }


}
