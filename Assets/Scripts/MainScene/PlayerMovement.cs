using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 startingPosition;

    public Transform BoundaryHolder;

    Boundary playerBoundary;

    public Collider2D PlayerCollider { get; private set; }

    public PlayerController Controller;

    public int? LockedFingerID { get; set; }

    // Use this for initialization
    void Start()
    {

#if UNITY_EDITOR
        Debug.Log("Unity Editor");
#endif
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;
        PlayerCollider = GetComponent<Collider2D>();

        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);

    }

#if UNITY_EDITOR || UNITY_WEBGL
    bool wasJustClicked = true;
    bool canMove;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 preMousePos = Input.mousePosition;
            preMousePos.z = 10;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(preMousePos);

            if (wasJustClicked)
            {
                wasJustClicked = false;

                if (PlayerCollider.OverlapPoint(mousePos))
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }

            if (canMove)
            {
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left,
                                                                  playerBoundary.Right),
                                                      Mathf.Clamp(mousePos.y, playerBoundary.Down,
                                                                  playerBoundary.Up));
                rb.MovePosition(clampedMousePos);
            }
        }
        else
        {
            wasJustClicked = true;
        }
    }
#endif

    private void OnEnable()
    {
        Controller.Players.Add(this);
    }
    private void OnDisable()
    {
        Controller.Players.Remove(this);
    }

    public void MoveToPosition(Vector2 position)
    {
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(position.x, playerBoundary.Left,
                                                  playerBoundary.Right),
                                      Mathf.Clamp(position.y, playerBoundary.Down,
                                                  playerBoundary.Up));
        rb.MovePosition(clampedMousePos);
    }

    public void ResetPosition()
    {
        rb.position = startingPosition;
    }
}