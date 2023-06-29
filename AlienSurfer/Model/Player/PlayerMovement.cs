using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum State {
    Run,
    Jump,
    Sideways,
    Slide,
    Fall,
    Fly,
}

public enum Lane {
    Left,
    Middle,
    Right,
}

public class PlayerMovement : MonoBehaviour {
    [SerializeField] 
    private LayerMask groundLayer;
    public State state = State.Run;
    public float jumpVelocity;
    public float sideMovementVelocity;
    public float rollDownVelocity;

    [HideInInspector]
    public Lane lane = Lane.Middle;
    public bool ObjectDetected { get; set; } = false;

    Rigidbody rb;
    GameManager gm;
    BoxCollider coll;
    Vector3 collCenter;
    Vector3 collSize;
    const float distToGround = 0.7f;
    bool interrupted = false;

    bool IsFalling {
        get {
            return rb.velocity.y < -0.2f;
        }
    }
    bool IsRolling { get; set; } = false;

    void Start() {
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        collCenter = coll.center;
        collSize = coll.size;
    }

    void Update() {
        if (IsFalling && !IsGrounded()) {
            EventManager.PlayerFalls();
        }
        if (IsFalling && IsGrounded()) {
            EventManager.PlayerLands();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Obstacle") && ObjectDetected) {
            gm.environmentSpeed = 0;
            interrupted = true;
            EventManager.GameOver();
        }
    }

    public bool IsGrounded() {
        bool isGrounded = Physics.BoxCast(coll.bounds.center, coll.bounds.extents / 2f, Vector3.down, 
                                          Quaternion.identity, distToGround, groundLayer);
        return isGrounded;
    }

    public void Jump() {
        if (state == State.Run && IsGrounded()) {
            rb.velocity = Vector3.up * jumpVelocity;
            state = State.Jump;
        }
    }

    public void Fall() {
        state = State.Fall;
    }
    
    public void Land() {
        if (state == State.Fall && IsGrounded()) {
            state = State.Run;
        }
        if (IsRolling && IsGrounded()) {
            state = State.Slide;
        }
    }

    public void MoveRight() {
        if (state != State.Slide && state != State.Sideways && lane != Lane.Right) {
            StartCoroutine(MoveToLane(lane + 1));
        }
    }

    public void MoveLeft() {
        if (state != State.Slide && state != State.Sideways && lane != Lane.Left) {
            StartCoroutine(MoveToLane(lane - 1));
        }
    }

    IEnumerator MoveToLane(Lane targetLane) {
        float targetX = 0f;
        switch (targetLane) {
            case Lane.Left:
                targetX = -gm.laneOffset;
                break;
            case Lane.Middle:
                targetX = 0f;
                break;
            case Lane.Right:
                targetX = gm.laneOffset;
                break;
        }
        State prevState = state;
        state = State.Sideways;
        while (Mathf.Abs(transform.position.x - targetX) > 0.01f && !interrupted) {
            transform.position = Vector3.MoveTowards(transform.position, 
                                                     new Vector3(targetX, transform.position.y, transform.position.z), 
                                                     sideMovementVelocity * Time.deltaTime);
            yield return null;
        }
        if (state == State.Sideways) {
            state = prevState;
        }
        lane = targetLane;
    }

    public void Slide() {
        if (state == State.Run) {
            state = State.Slide;
        }
    }

    public void RollDown() {
        if ((state == State.Jump || state == State.Fall) && !IsRolling) {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * rollDownVelocity, ForceMode.Impulse);
            IsRolling = true;
        }
    }


    // Handler for the animation event
    public void GetDown() {
        coll.size = new Vector3(1f, 0.5f, 1f);
        coll.center = new Vector3(0f, 0.25f, 0f);
    }
    
    public void EndSliding() {
        state = State.Run;
        IsRolling = false;
        coll.size = collSize;
        coll.center = collCenter;
    }


}

