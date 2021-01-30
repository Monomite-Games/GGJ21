using UnityEngine;

using Palomas.Requests;

namespace Palomas.Menu
{
    public class PointerMover : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;
        private RequestsList RequestsList => RequestsList.Instance;

        [SerializeField]
        private GameObject RequestBubble;

        [SerializeField]
        private string RequestId;

        [SerializeField]
        private GameObject PointerPrefab;

        [SerializeField]
        private Transform PointersHolder;

        [SerializeField]
        private Camera UICamera;

        [SerializeField]
        private float BorderSize;

        private Vector3 TargetPosition;
        private RectTransform PointerRectTransform;
        private GameObject Pointer;
        private bool FirstTime = true;
        private Request Request;

        private void Awake()
        {
            TargetPosition = new Vector3(RequestBubble.transform.position.x, RequestBubble.transform.position.y, 0.0f);
        }

        private void Start()
        {
            Request = RequestsList.GetById(RequestId);
            Pointer = Instantiate(PointerPrefab, PointersHolder);
            PointerRectTransform = Pointer.GetComponent<RectTransform>();

            GameEvents.RequestChanged += (sender, args) => FirstTime = true;
        }

        private void Update()
        {
            if (Request.IsInUse())
            {
                Vector3 toPosition = TargetPosition;
                Vector3 fromPosition = Camera.main.transform.position;

                fromPosition.z = 0.0f;
                Vector3 direction = (toPosition - fromPosition).normalized;
                float angle = GetAngleFromVectorFloat(direction);
                PointerRectTransform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);

                Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(TargetPosition);
                if (IsPositionRight(targetPositionScreenPoint) ||
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

                    Vector3 pointerWorldPosition = UICamera.ScreenToWorldPoint(cappedTargetScreenPosition);
                    PointerRectTransform.position = pointerWorldPosition;
                    PointerRectTransform.localPosition = new Vector3(PointerRectTransform.localPosition.x, PointerRectTransform.localPosition.y, 0.0f);
                }
                else
                {
                    EnableBubble();
                }
            }
            else
            {
                DisableAll();
            }
        }

        private void EnableArrow()
        {
            Pointer.SetActive(true);
            RequestBubble.SetActive(false);
        }

        private void EnableBubble()
        {
            if(FirstTime)
            {
                FirstTime = false;
                GameEvents.OnRequestObtained(RequestId);
            }
            Pointer.SetActive(false);
            RequestBubble.SetActive(true);
        }

        private void DisableAll()
        {
            Pointer.SetActive(false);
            RequestBubble.SetActive(false);
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

            return angle < 0 ? angle + 360 : angle;
        }
    }
}