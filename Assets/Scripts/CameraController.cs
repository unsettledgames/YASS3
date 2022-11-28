using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject PlayerAnchor;
    public GameObject Player;

    [SerializeField]
    private float Easing;

    void FixedUpdate()
    {
        float xRot = Mathf.LerpAngle(transform.localEulerAngles.x, Player.transform.localEulerAngles.x, Easing * Time.deltaTime);
        float yRot = Mathf.LerpAngle(transform.localEulerAngles.y, Player.transform.localEulerAngles.y, Easing * Time.deltaTime);
        float zRot = Mathf.LerpAngle(transform.localEulerAngles.z, Player.transform.localEulerAngles.z, Easing * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, PlayerAnchor.transform.position, Easing * Time.deltaTime);
        transform.LookAt(Player.transform.position);

        transform.localEulerAngles = new Vector3(xRot, yRot, zRot);
    }
}