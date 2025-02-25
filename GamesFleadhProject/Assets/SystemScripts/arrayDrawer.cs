using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrayDrawer : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    private void OnDrawGizmos()
    {
        //Inner Moon Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 6.0f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 9.0f);

        //Middle Moon Range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 12.0f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 15.0f);

        //Outer Moon Range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 18.0f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21.0f);

        //Home Planet Range
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 24.0f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 24.0f);

    }
}
