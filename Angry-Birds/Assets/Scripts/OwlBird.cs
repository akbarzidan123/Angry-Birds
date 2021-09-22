using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBird : Bird
{
    [SerializeField]
    public float filedImpact;
    public float power;
    public LayerMask obstacle;
    public override void OnTap()
    {
        // print("Booom");
        Explode();
        Destroy(gameObject);
    }
    
    void Explode()
    {
        Collider2D[] obj = Physics2D.OverlapCircleAll(transform.position, filedImpact,obstacle);
        foreach(Collider2D objects in obj)
        {
            Vector2 direct = objects.transform.position - transform.position;
            objects.GetComponent<Rigidbody2D>().AddForce(direct * power);
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, filedImpact);
    }
}