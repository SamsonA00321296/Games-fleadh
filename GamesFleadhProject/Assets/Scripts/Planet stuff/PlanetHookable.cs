using Player_Scripts;
using UnityEngine;

namespace Planet_stuff
{
    

    public class PlanetHookable : MonoBehaviour
    {

        public HookShot hookshot;


        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other);

            if (other.CompareTag("Hook"))
            {
                Debug.Log($"Planet detected hook: {other.gameObject.name}");
                AttachHook(other.gameObject);

                // Gets the script in the hooks parent (The player) to flag the ship as attached
                hookshot = other.gameObject.GetComponentInParent<HookShot>();        //GetComponent<HookShot>();

                if (hookshot != null)
                {
                    Debug.Log("Plannet Snagged!");
                    HookShot.isAttached = true;
                }
            }
        }
        

        void AttachHook(GameObject hook)
        {
            Rigidbody2D hookRb = hook.GetComponent<Rigidbody2D>();
            if (hookRb == null) return;

            hookRb.linearVelocity = Vector2.zero;
            hookRb.bodyType = RigidbodyType2D.Dynamic;

            HingeJoint2D joint = hook.AddComponent<HingeJoint2D>();
            joint.connectedBody = GetComponent<Rigidbody2D>(); // Attach to planet
            
        }
    }
}