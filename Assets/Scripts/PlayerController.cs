using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerCamera pC;
    PlayerMovement pM;

    void Start()
    {
        pC = this.GetComponent<PlayerCamera>();
        pM = this.GetComponent<PlayerMovement>();
    }


}
