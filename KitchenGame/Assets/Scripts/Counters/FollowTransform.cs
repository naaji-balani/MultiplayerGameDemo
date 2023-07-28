using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    private Transform _targetTransoform;

    public void SetTragetTransform(Transform targetTransform)
    {
        this._targetTransoform = targetTransform;
    }

    private void LateUpdate()
    {
        if (_targetTransoform == null) return;

        transform.position = _targetTransoform.position;
        transform.rotation = _targetTransoform.rotation;
    }
}
