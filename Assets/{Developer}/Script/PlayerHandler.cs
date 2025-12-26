using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public bool isDragging { get; private set; }

    private Rigidbody rb;
    private GameObject selectedObject;

    private Plane dragPlane;
    private Vector3 dragOffset;
    private Vector3 targetPos;

    [Header("Tuning")]
    public float followSpeed = 15f;
    public float scrollSpeed = 2f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryPick();

        if (Input.GetMouseButtonUp(0))
            DragEnd();
    }

    void FixedUpdate()
    {
        if (isDragging)
            DragPhysics();
    }

    void TryPick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) &&
            hit.collider.CompareTag("Dragable"))
        {
            selectedObject = hit.collider.gameObject;
            rb = selectedObject.GetComponent<Rigidbody>();

            rb.isKinematic = false;
            rb.useGravity = false; // ❗ Gravity off during drag
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            // Plane aligned with camera view
            dragPlane = new Plane(Camera.main.transform.forward * -1f,
                                  selectedObject.transform.position);

            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                dragOffset = selectedObject.transform.position - hitPoint;
            }

            targetPos = rb.position;
            isDragging = true;
        }
    }

    void DragPhysics()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // X & Y movement from mouse
        if (dragPlane.Raycast(ray, out float enter))
        {
            Vector3 hit = ray.GetPoint(enter);
            targetPos.x = hit.x + dragOffset.x;
            targetPos.y = hit.y + dragOffset.y;
        }

        // Z movement via scroll (camera forward/back)
        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            targetPos += Camera.main.transform.forward * scroll * scrollSpeed;
        }

        // Smooth physics movement with collision respect
        Vector3 direction = targetPos - rb.position;
        Vector3 move = direction.normalized * followSpeed * Time.fixedDeltaTime;

        if (move.magnitude > direction.magnitude)
            move = direction;

        rb.MovePosition(rb.position + move);
    }

    public void DragEnd()
    {
        if (!isDragging) return;

        isDragging = false;
        rb.useGravity = true; // ✅ Gravity back on
        rb.linearVelocity = Vector3.zero;

        rb = null;
        selectedObject = null;
    }
}