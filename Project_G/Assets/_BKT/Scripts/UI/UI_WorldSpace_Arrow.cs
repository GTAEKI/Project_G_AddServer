using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WorldSpace_Arrow : UI_WorldSpace
{
    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private float moveHeight = 0.5f;
    private Vector3 startPosition;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        startPosition = transform.position;

        return true;
    }

    void Update()
    {
        MoveArrow();
    }

    void MoveArrow()
    {
        float newZ = startPosition.z + Mathf.Sin(Time.time * moveSpeed) * moveHeight;
        transform.position = new Vector3(startPosition.x, startPosition.y, newZ);
    }
}
