using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float maxSwingDistance = 50f;
    [SerializeField] private float grapleDistance = 2f;
    [SerializeField] private float predictionRadius = 3f;
    [SerializeField] private LayerMask surfaces;
    [SerializeField] private PlayerWeb[] webs;
    [SerializeField] private SpringJoint[] joints;
    private Vector3[] points = { Vector3.zero, Vector3.zero };
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
        PlayerInputScript.SwingEvent += receiveInput;
    }

    void OnDisable()
    {
        PlayerInputScript.SwingEvent -= receiveInput;
    }

    private void receiveInput(bool performed, bool isLeft)
    {
        if (isLeft && performed)
            shootWeb(0);
        else if (isLeft)
            stopWeb(0);
        else if (performed)
            shootWeb(1);
        else
            stopWeb(1);
    }

    private void shootWeb(int i)
    {
        RaycastHit hit = getWebPoint();

        if (hit.Equals(default(RaycastHit)))
            return;

        points[i] = hit.point;
        webs[i].ActivateWeb(points[i]);

        checkGrapple();
    }

    private void stopWeb(int i)
    {
        points[i] = Vector3.zero;
        webs[i].DeactivateWeb();
    }

    private RaycastHit getWebPoint()
    {
        RaycastHit sphereHit;
        Physics.SphereCast(cam.position, predictionRadius, cam.forward, out sphereHit, maxSwingDistance, surfaces);

        RaycastHit raycastHit;
        Physics.Raycast(cam.position, cam.forward, out raycastHit, maxSwingDistance, surfaces);

        if (raycastHit.point != Vector3.zero)
            return raycastHit;

        else if (sphereHit.point != Vector3.zero)
            return sphereHit;

        return default(RaycastHit);
    }

    void checkGrapple()
    {
        if (points[0] != Vector3.zero && points[1] != Vector3.zero && Vector3.Distance(points[0], points[1]) < grapleDistance)
            Debug.Log("grapple");
        else
            Debug.Log("swing");
    }

    [Space(15), Header("Swing")]
    [SerializeField] private float test;

    [Space(15), Header("Grapple")]
    [SerializeField] private float test2;
}
