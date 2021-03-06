using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerAndCam : MonoBehaviour
{
    public Camera cameraFPS;
    Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    float rotX = 0.0f;
    float rotY = 0.0f;
    public float speed = 10;

    private PhotonView photonview;
    public Vector3 checkPoint;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        checkPoint = transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Player";
       
        cameraFPS.transform.localPosition = new Vector3(0, 1, 0);
        cameraFPS.transform.localRotation = Quaternion.identity;
        cameraFPS.enabled = true;
        controller = GetComponent<CharacterController>();


        photonview = PhotonView.Get(this);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonview.IsMine)
        {
            cameraFPS.enabled = false;
            return;
        }
        else
        {
            cameraFPS.enabled = true;
        }


        Controll();
        CameraFPS();

    }

    public void Controll()
    {
        Vector3 directionFront = new Vector3(cameraFPS.transform.forward.x,
                                        0.0f,
                                        cameraFPS.transform.forward.z);

        Vector3 directionSide = new Vector3(cameraFPS.transform.right.x,
                                            0.0f,
                                            cameraFPS.transform.right.z);

        directionFront.Normalize();
        directionSide.Normalize();
        directionFront = directionFront * Input.GetAxis("Vertical");
        directionSide = directionSide * Input.GetAxis("Horizontal");
        Vector3 directionFinal = directionFront + directionSide;

        if (directionFinal.sqrMagnitude > 1)
        {
            directionFinal.Normalize();
        }

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(directionFinal.x, 0, directionFinal.z);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = 10.0f;
            }
        }

        moveDirection.y -= 20.0f * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

	public void LoadCheckPoint()
	{
		transform.position = checkPoint;
	}

    public void CameraFPS()
    {
        rotX += Input.GetAxis("Mouse X") * 10.0f;
        rotY += Input.GetAxis("Mouse Y") * 10.0f;
        rotX = ClampAngleFPS(rotX, -360, 360);
        rotY = ClampAngleFPS(rotY, -80, 80);
        Quaternion xQuat = Quaternion.AngleAxis(rotX, Vector3.up);
        Quaternion yQuat = Quaternion.AngleAxis(rotY, -Vector3.right);
        Quaternion rotFinal = Quaternion.identity * xQuat * yQuat;

        cameraFPS.transform.localRotation = Quaternion.Lerp(
            cameraFPS.transform.localRotation, rotFinal, Time.deltaTime*10.0f);
    }

    public float ClampAngleFPS(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
