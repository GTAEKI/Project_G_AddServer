using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshipMovement : MonoBehaviour
{
    private Gunship gunship;
    private GameInput gameInput;
    [SerializeField]
    private float moveSpeed = 10f;

    //BKT
    [SerializeField]
    private Vector3 minBounds;
    [SerializeField]
    private Vector3 maxBounds;

    void Start()
    {
        Init();
    }

    void Init()
    {
        gunship = GetComponent<Gunship>();        
        gameInput = gunship.GameInput;
        
    }
    void Update()
    {
        Handle_Movement();
    }

    void Handle_Movement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormailized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;

        // 맵 범위를 벗어나는지 후에 체크
        //bool canMove = default;

        // BKT
        Vector3 newPosition = transform.position + moveDir * moveDistance;

        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
        newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

        transform.position = newPosition;
    }

    void CheckMapEdge()
    {

    }
}
