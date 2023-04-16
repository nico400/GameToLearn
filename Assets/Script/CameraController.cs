using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerPosition;

    public Transform positionAttack;
    public Transform positionNormal;

    public float sencibilityX;
    public float sencibilityY;
    public float distace;
    public float distaceAttack;
    public float distaceNormal;
    public float MaxDistace;

    public float mouseX;
    public float mouseY;

    static RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PlayerController.toChangeCam += changeCam;
        PlayerController.toChangeCamNormal += changeCamNormal;
    }

    // Update is called once per frame
    void Update()
    {
        //inputs
        mouseX += Input.GetAxisRaw("Mouse X");
        mouseY += Input.GetAxisRaw("Mouse Y");

        mouseY = Mathf.Clamp(mouseY,-85,85);
        //rotacionar a camera
        transform.rotation = Quaternion.Euler(mouseY,mouseX,0);
        
        Vector3 theDirectionOfCam = transform.rotation * Vector3.back;//pegando as direçoes do objeto (nesse caso to pegando as coistas da camera)    
        Vector3 posCam = PlayerPosition.position + (theDirectionOfCam * distace);//distancia do player para camera e olhando para o player
        transform.localPosition = posCam;//mover cam
      
        if (Physics.Linecast(PlayerPosition.position, transform.localPosition, out hit))//imagino uma linha entre meu personagem ate a camera
        {
            //colision
            transform.position = hit.point + transform.forward * 0.1f;
        }
    }
    void changeCam()
    {
        PlayerPosition = positionAttack;
        distace = Mathf.Lerp(distace, distaceAttack, 3 * Time.deltaTime);
    }
    void changeCamNormal()
    {
        PlayerPosition = positionNormal;
        distace = Mathf.Lerp(distace, distaceNormal, 3 * Time.deltaTime);
    }

}
