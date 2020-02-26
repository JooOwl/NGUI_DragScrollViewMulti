using UnityEngine;

/// <summary>
/// Horizontal스크롤도 돼야 하고. Vertical스크롤도 돼야 할 때에 쓴다. 
/// </summary>
public class DragScrollViewMulti : MonoBehaviour
{
    private UIScrollView horizontalScrollView;
    private UIScrollView verticalScrollView;

    private UIScrollView currentScrollView;

    private bool pressed = false;

    private void OnEnable()
    {
        Transform currentTrasnform = transform;
        while (horizontalScrollView == null || verticalScrollView == null)
        {
            UIScrollView scrollView = currentTrasnform.GetComponent<UIScrollView>();
            if (scrollView != null)
            {
                if (scrollView.movement == UIScrollView.Movement.Vertical)
                {
                    verticalScrollView = scrollView;
                }
                else if (scrollView.movement == UIScrollView.Movement.Horizontal)
                {
                    horizontalScrollView = scrollView;
                }
            }

            currentTrasnform = currentTrasnform.parent;
            if (currentTrasnform == null)
            {
                break;
            }
        }
    }

    private void OnDrag(Vector2 delta)
    {
        if (horizontalScrollView && verticalScrollView && NGUITools.GetActive(this))
        {
            if (currentScrollView == null)
            {
                float absX = Mathf.Abs(delta.x);
                float absY = Mathf.Abs(delta.y);

                if (absX > absY)
                {
                    currentScrollView = horizontalScrollView;
                }
                else if (absX < absY)
                {
                    currentScrollView = verticalScrollView;
                }
            }

            if (currentScrollView != null)
            {
                currentScrollView.Drag();
            }
        }
    }

    private void OnDragEnd()
    {
        currentScrollView = null;
    }

    private void OnPress(bool pressed)
    {
        this.pressed = pressed;

        if (horizontalScrollView && verticalScrollView && enabled && NGUITools.GetActive(gameObject))
        {
            horizontalScrollView.Press(pressed);
            verticalScrollView.Press(pressed);
        }
    }

    private void OnDisable()
    {
        if (pressed && currentScrollView != null && currentScrollView.GetComponentInChildren<UIWrapContent>() == null)
        {
            currentScrollView.Press(false);
            currentScrollView = null;
        }
    }
}
