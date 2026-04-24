using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    public Camera mainCamera;
    public float maxDistance = 100f;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            DetectHit(Input.mousePosition);
        }
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            DetectHit(Input.GetTouch(0).position);
        }
#endif
    }

    void DetectHit(Vector2 screenPos)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, maxDistance);

        if (hit.collider != null)
        {
            Target target = hit.collider.GetComponent<Target>();
            if (target != null)
            {
                target.Hit();
            }
        }
    }
}
