using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public Rigidbody rg;
    public float Speed;
    public GameObject ArrowThis;

    public LayerMask EnemyLayer;
    public delegate float TakeDamege(float dmg);
    public static event TakeDamege takedamegeinEnemy;
    // Start is called before the first frame update
    void Start()
    {
        rg.AddForce(transform.forward * Speed, ForceMode.Impulse);
    }
    // Update is called once per frame
    void Update()
    {

        float TimeDestroy = 4;
        TimeDestroy -= 1 * Time.deltaTime;
        Destroy(ArrowThis, TimeDestroy);

        
        Collider[] coll = Physics.OverlapSphere(transform.position + new Vector3(-1,0,0),0.3f);
        for (int i = 0; i < coll.Length; i++)
        {
            Destroy(ArrowThis);
        }

        Collider[] collEnemy = Physics.OverlapSphere(transform.position + new Vector3(-1, 0, 0), 0.3f, EnemyLayer);
        for (int i = 0; i < coll.Length; i++)
        {
            takedamegeinEnemy(10);
            Destroy(ArrowThis);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + new Vector3(-1, 0, 0), 0.3f);
    }
}
