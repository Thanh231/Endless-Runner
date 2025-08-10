using UnityEngine;

public enum MoveBehavior
{
    None,
    Left,
    Right,
    Jump,
    Fire,
    Reload
}

public class PlayerInput : MonoBehaviour
{
    public Vector2 startTouch = Vector2.zero;
    private Vector2 deltaMove = Vector2.zero;
    private bool isTouching = false;
    public float swipeThreshold = 30f;

    public MoveBehavior direction = MoveBehavior.None;

    void Update()
    {
        // HandleTouchInput();
        // HandleKeyboardFallback();
    }

    public void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                startTouch = t.position;
                isTouching = true;
                deltaMove = Vector2.zero;
                direction = MoveBehavior.None;
            }
            else if ((t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)  && isTouching)
            {

                deltaMove = t.position - startTouch;
                ProcessSwipe();
                ResetTouch();
            }
        }
    }

    public void HandleKeyboardFallback()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = MoveBehavior.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = MoveBehavior.Right;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction = MoveBehavior.Jump;
        }

        if (Input.GetKeyDown(KeyCode.F))
            direction = MoveBehavior.Fire;
        if (Input.GetKeyDown(KeyCode.R))
            direction = MoveBehavior.Reload;
    }

    private void ProcessSwipe()
    {
        if (deltaMove.magnitude < swipeThreshold)
        {
            direction = MoveBehavior.Fire;
            return;
        }

        float x = deltaMove.x;
        float y = deltaMove.y;

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            direction = (x > 0) ? MoveBehavior.Right : MoveBehavior.Left;
        }
        else
        {
            direction = (y > 0) ? MoveBehavior.Jump : MoveBehavior.None;
        }
    }

    private void ResetTouch()
    {
        isTouching = false;
        startTouch = Vector2.zero;
        deltaMove = Vector2.zero;
    }

    public MoveBehavior GetAndClearDirection()
    {
        MoveBehavior d = direction;
        direction = MoveBehavior.None;
        return d;
    }
}
