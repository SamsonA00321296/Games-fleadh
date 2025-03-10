using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player_Scripts
{
    public class HookShot : MonoBehaviour
    {
        [Header("References")]
        public GameObject chainPrefab;  // Chain link prefab
        public GameObject hookPrefab;   // Hook projectile prefab
        public Transform player;        // The ship (origin point)
        public Rigidbody2D playerRb;    // Cached Rigidbody2D

        [Header("Hook Settings")]
        public float hookSpeed = 10f;   // Hook movement speed
        public int maxLinks = 10;       // Maximum number of chain links
        public float linkSpacing = 0.2f;// Space between links
        public float retractSpeed = 15f;// Speed of hook retraction

        private bool _isShooting;
        private bool _isRetracting;
        public static bool isAttached;

        private Rigidbody2D _lastLinkRb;
        private GameObject _hookInstance;
        private Rigidbody2D _hookRb;
        private int _currentLinks;

        private List<GameObject> _chainLinks = new List<GameObject>(); // Stores all chain links

        void Start()
        {
            if (player == null) player = transform.parent;
            if (playerRb == null) playerRb = player.GetComponent<Rigidbody2D>();
        }

        public void OnAltfire()
        {
            if (!_isShooting && !_isRetracting)
            {
                ShootHook();
            }
        }

        void ShootHook()
        {
            _isShooting = true;
            _currentLinks = 0;
            _lastLinkRb = null;
            isAttached = false;

            // Spawn hook
            Vector2 hookSpawnPos = player.position + player.transform.up * 0.5f;
            _hookInstance = Instantiate(hookPrefab, hookSpawnPos, Quaternion.identity);
            _hookRb = _hookInstance.GetComponent<Rigidbody2D>();

            _hookRb.linearVelocity = player.transform.up * hookSpeed;

            StartCoroutine(SpawnChain());
        }

        IEnumerator SpawnChain()
        {
            while (_currentLinks < maxLinks)
            {
                if (!_hookInstance) break;

                Vector2 chainSpawnPos = _hookInstance.transform.position - player.transform.up * linkSpacing;
                GameObject newLink = Instantiate(chainPrefab, chainSpawnPos, Quaternion.identity);

                _chainLinks.Add(newLink); // Store the link

                Rigidbody2D newLinkRb = newLink.GetComponent<Rigidbody2D>();
                HingeJoint2D newJoint = newLink.GetComponent<HingeJoint2D>();

                _currentLinks++;

                newJoint.connectedBody = _lastLinkRb ? _lastLinkRb : playerRb;
                _lastLinkRb = newLinkRb;

                yield return new WaitForSeconds(0.1f);
            }

            if (!isAttached)
            {
                StartRetraction();
            }
        }

        void StartRetraction()
        {
            _isRetracting = true;
            _hookRb.linearVelocity = Vector2.zero;

            if (!isAttached)
            {
                StartCoroutine(RetractHook());
            }
        }

        IEnumerator RetractHook()
        {
            while (_hookInstance && !isAttached)
            {
                Vector2 directionToPlayer = (player.position - _hookInstance.transform.position).normalized;
                _hookRb.linearVelocity = directionToPlayer * retractSpeed;

                if (_chainLinks.Count > 0)
                {
                    GameObject lastChain = _chainLinks[_chainLinks.Count - 1];
                    _chainLinks.Remove(lastChain);
                    Destroy(lastChain);
                }

                if (Vector2.Distance(player.position, _hookInstance.transform.position) < 0.5f)
                {
                    DestroyHook();
                    break;
                }

                yield return new WaitForSeconds(0.05f);
            }
        }

        void DestroyHook()
        {
            if (_hookInstance)
            {
                Destroy(_hookInstance);
                _hookInstance = null;
            }

            foreach (GameObject link in _chainLinks)
            {
                Destroy(link);
            }
            _chainLinks.Clear();

            _isShooting = false;
            _isRetracting = false;
            isAttached = false;
        }
    }
}