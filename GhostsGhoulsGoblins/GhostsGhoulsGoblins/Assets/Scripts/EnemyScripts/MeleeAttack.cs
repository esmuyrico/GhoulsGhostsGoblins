using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private float swingAngle;
    private bool isSwinging = false;
    private int speed = 5;
    private int damage = 2;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
       
    }

    public void setDamage(int _damage)
    {
        damage = _damage;
    }

    public IEnumerator swing()
    {
        if (!isSwinging)
        {
            float swingDuration = 10;
            Vector3 startRot = transform.rotation.eulerAngles;
            Vector3 endRot = transform.rotation.eulerAngles + new Vector3(0, 90, 0);
            isSwinging = true;
            for (float swingTime = 0; swingTime < swingDuration; swingTime+= 0.1f)
            {
                transform.Rotate(new Vector3(0, 1, 0));
                yield return null;
            }
            //transform.rotation = quaternion.Euler(startRot);
            isSwinging = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerData>().HurtPlayer(damage);
            Debug.Log("hit player");
            Destroy(gameObject);
        }
    }
}
