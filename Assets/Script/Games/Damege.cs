using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damege : MonoBehaviour
{
    public float DMG = 5;
    public delegate float TakeDamege(float dmg);
    public static event TakeDamege takedamege;
    public LayerMask PlayerLayer;

    public float collDown_damg = 0;

    // Start is called before the first frame update
    void Start()
    {
        collDown_damg = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //disparar evento para o player e diminuir a vida dele(dar dano)
        Collider[] hitPlayer = Physics.OverlapBox(transform.position, Vector3.one, transform.rotation, PlayerLayer);
        for (int i = 0;i < hitPlayer.Length; i++)
        {
            collDown_damg -= Time.deltaTime;
            if (collDown_damg <= 0)
            {
                if (takedamege != null)
                    takedamege(DMG);
                collDown_damg = 1;
            }
        }
    }
}
