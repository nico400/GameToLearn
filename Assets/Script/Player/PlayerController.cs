using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Animator anim;
    public float speed;
    public float RUNSPEED;
    public float WALKSPEED;

    public Camera cameraDRay;

    public float timeToArrow;
    public Transform posSpawnArrow;
    public GameObject ArrowGameObj;

    public float JumpForce;

    public float LifePlayer = 100;
    public LayerMask RelativeForMyLife;

    public float radius;
    public Vector3 distancetoGrounde;

    public bool isAttacking = false;

    public LayerMask groundLayer;
    public bool inGrounded;
    public float speedRotation;
    float hori;
    float vert;
    public Vector3 directionMove;
    public Transform cam;
    Quaternion toRotation;
    Quaternion camRoot;

    //evento to my cam
    public delegate void TochangeCam();
    public static event TochangeCam toChangeCam;

    public delegate void TochangeCamNormal();
    public static event TochangeCam toChangeCamNormal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        TakeDMGinPlayer.takedamege += whatMyLife;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //entradas
        hori = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
        bool InputJump = Input.GetButtonDown("Jump");
        Vector3 move = Vector3.zero;
        //pegar a direçao 
        directionMove = new Vector3(hori, 0, vert).normalized;
        //direçao de onde esta olhando e onde vai andar
        toRotation = Quaternion.LookRotation(directionMove, Vector3.up);
        camRoot = Quaternion.Euler(0, cam.transform.root.eulerAngles.y, 0);

        //attack
        if (Input.GetMouseButton(0) && inGrounded)
        {
            Debug.Log("PrepararATTACK");
            isAttacking = true;
            //fazer ele preparar o ataque e olhar para onde a camera aponta
            anim.SetBool("isAttack", isAttacking);
            DirectionOFCam();
            toChangeCam();
            timeToArrow -= 1 * Time.deltaTime;
            speed = WALKSPEED;

            Ray ray = cameraDRay.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height/2,0));
            RaycastHit hit;
            Vector3 dir;
            if (Physics.Raycast(ray, out hit))
            {             
                dir = (hit.point - posSpawnArrow.position).normalized;
                posSpawnArrow.transform.rotation = Quaternion.LookRotation(dir);
            }
            else
            {
                dir = ray.direction;
                posSpawnArrow.transform.rotation = Quaternion.LookRotation(dir);
            }

            if (timeToArrow <= 0)
            {
                Debug.Log("attack");
                Instantiate(ArrowGameObj, posSpawnArrow.position, posSpawnArrow.rotation);
                timeToArrow = 2;
            }
        }
        else
        {
            timeToArrow = 2;
            isAttacking = false;
            //fazer ele solta o ataque
            anim.SetBool("isAttack", isAttacking);
            toChangeCamNormal();
        }
         //functions
         if (!isAttacking)
         {
            directionToFrom();//olhar em direçao com a camera
         }
         animations();

        //identificar o chao
        inGrounded = Physics.OverlapSphere(transform.position, radius, groundLayer).Length > 0;
        //fazendo pular
        if (InputJump && inGrounded)
        {
            rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jumping");
        }
        else if (inGrounded)
            anim.ResetTrigger("Jumping");

        if (!isAttacking)
        {
            //eu ando
            move = transform.forward * speed;
            move.y = rb.velocity.y;
        }else
        {
            move = (directionMove.x * cam.right + directionMove.z * cam.forward) * speed;
            move.y = rb.velocity.y;
        }
        //speedRUNS
        if (directionMove != Vector3.zero)
        {
            speed += 2f * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed += 3f * Time.deltaTime;
                if (speed >= RUNSPEED)
                {
                    speed = RUNSPEED;
                }
            }else
            {              
                if (speed >= WALKSPEED)
                {
                    speed -= 3 * Time.deltaTime;
                    if (speed <= WALKSPEED)
                    {
                        speed = WALKSPEED;
                    }
                }
            }
        }
        else
        {
            speed -= 4f * Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, 0, 7);
        //ANDAR
        rb.velocity = move;

    }
    void directionToFrom()
    {
        if (directionMove != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, camRoot * toRotation, speedRotation * Time.deltaTime);
        }else
        {
            transform.rotation = transform.rotation;
        }
    }
    void DirectionOFCam()
    {
        transform.rotation = Quaternion.Euler(0,cam.eulerAngles.y,0);
    }
    void animations()
    {
        anim.SetFloat("movement", speed);
        anim.SetBool("inGround", inGrounded);
    }
    float whatMyLife(float dmg)
    {
        return LifePlayer -= dmg;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }

}
