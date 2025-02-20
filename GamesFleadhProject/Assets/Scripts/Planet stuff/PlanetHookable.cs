using Player_Scripts;
using UnityEngine;

namespace Planet_stuff
{
    public class PlanetHookable : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Hook"))
            {
                Debug.Log($"Planet detected hook: {other.gameObject.name}");
                AttachHook(other.gameObject);
                
                HookShot hookshot = other.gameObject.GetComponent<HookShot>();
                if (hookshot)
                {
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