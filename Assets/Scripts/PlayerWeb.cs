using UnityEngine;

public class PlayerWeb : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private PlayerMovement pM;
    private Vector3 position;
    private bool active = false;

    void Update()
    {
        if (active)
            updateWeb();
    }

    public void ActivateWeb(Vector3 point)
    {
        if (active != true)
            pM.AddSwingPoint(1);
        active = true;
        lr.enabled = active;
        position = point;
        lr.SetPosition(1, point);
    }

    public void DeactivateWeb()
    {
        if (active != false)
            pM.SubtractSwingPoint();
        active = false;
        lr.enabled = active;
        transform.eulerAngles = new Vector3(90, 0, 0);
    }

    private void updateWeb()
    {
        transform.LookAt(position);
        lr.SetPosition(0, origin.position);
    }
}
