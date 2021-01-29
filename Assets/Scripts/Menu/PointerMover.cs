using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMover : MonoBehaviour
{
    [SerializeField]
    private GameObject RequestBubble;

    [SerializeField]
    private GameObject Pointer;

    [SerializeField]
    private Camera CameraUI;

    [SerializeField]
    private float BorderSize;

    private RectTransform pointerRectTransform;
    private Vector3 targetPosition;

    private void Awake()
    {
        pointerRectTransform = Pointer.GetComponent<RectTransform>();
        targetPosition = RequestBubble.transform.position;
    }

    private void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;

        fromPosition.z = 0.0f;
        Vector3 direction = (toPosition - fromPosition).normalized;
        float angle = GetAngleFromVectorFloat(direction);
        pointerRectTransform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        if(IsPositionRight(targetPositionScreenPoint) ||
            IsPositionLeft(targetPositionScreenPoint) ||
            IsPositionDown(targetPositionScreenPoint) ||
            IsPositionUp(targetPositionScreenPoint))
        {
            EnableArrow();

            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (IsPositionRight(cappedTargetScreenPosition)) cappedTargetScreenPosition.x = BorderSize;
            if (IsPositionLeft(cappedTargetScreenPosition)) cappedTargetScreenPosition.x = Screen.width - BorderSize;
            if (IsPositionDown(cappedTargetScreenPosition)) cappedTargetScreenPosition.y = BorderSize;
            if (IsPositionUp(cappedTargetScreenPosition)) cappedTargetScreenPosition.y = Screen.height - BorderSize;

            Vector3 pointerWorldPosition = CameraUI.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0.0f);
        }
        else
        {
            EnableBubble();
        }
    }

    private void EnableArrow()
    {
        Pointer.SetActive(true);
        RequestBubble.SetActive(false);
    }

    private void EnableBubble()
    {
        RequestBubble.SetActive(true);
        Pointer.SetActive(false);
    }

    private bool IsPositionRight(Vector3 position)
    {
        return position.x <= BorderSize;
    }

    private bool IsPositionLeft(Vector3 position)
    {
        return position.x >= Screen.width - BorderSize;
    }

    private bool IsPositionDown(Vector3 position)
    {
        return position.y <= BorderSize;
    }

    private bool IsPositionUp(Vector3 position)
    {
        return position.y >= Screen.height - BorderSize;
    }

    private float GetAngleFromVectorFloat(Vector3 vector)
    {
        vector = vector.normalized;
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        return (angle < 0)? (angle + 360) : angle;
    }
}
