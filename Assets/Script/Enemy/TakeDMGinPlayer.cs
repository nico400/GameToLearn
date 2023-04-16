using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDMGinPlayer : MonoBehaviour
{
    public delegate float TakeDamege(float dmg);
    public static event TakeDamege takedamege;

    public float DMG = 10;
    public LayerMask playerLayer;

    public GameObject PosAttack;
    public ParticleSystem explosionEf;
    public PlayerController playerController;

    public void Damege()
    {
        Collider[] collPlayer = Physics.OverlapBox(PosAttack.transform.position, PosAttack.transform.localScale, PosAttack.transform.rotation, playerLayer);
        explosionEf.transform.position = PosAttack.transform.position;
        explosionEf.Play();

        for (int i = 0; i < collPlayer.Length; i++)
        {    
            if (takedamege != null)
                takedamege(DMG);

            playerController.rb.AddForce(playerController.transform.up * 25, ForceMode.Impulse);
        }
    }
}
