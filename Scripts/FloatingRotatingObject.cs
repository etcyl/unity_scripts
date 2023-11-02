using UnityEngine;

namespace ABCToolkit
{

    public class FloatingRotatingObject : MonoBehaviour
    {
        public float rotationSpeed = 50.0f; // Speed of rotation
        public float floatSpeed = 0.5f; // Speed of floating movement
        public float floatHeight = 1.0f; // Height of floating movement
        public int objType; //0 for health pot, 1 for mana pot

        private Vector3 startPosition;
        private float originalY;

        private void Start()
        {
            startPosition = transform.position;
            originalY = startPosition.y;
        }

        private void Update()
        {
            // Rotate the GameObject
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // Float the GameObject up and down
            Vector3 tempPos = startPosition;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatSpeed) * floatHeight;
            transform.position = tempPos;
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Check if the collision is with the player
            if (collision.gameObject.CompareTag("Player"))
            {
                // Set ABC stats
                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                if (playerObject != null)
                {

                    if (objType == 0) // Health Potion
                    {
                        ABC_StateManager statemgr = playerObject.GetComponent<ABC_StateManager>();
                        if (statemgr != null)
                        {
                            statemgr.currentHealth += 10f + PlayerPrefs.GetFloat("Level")*2.5f;
                        }
                    }
                    else if (objType == 1) // Mana potion
                    {
                        ABC_Controller controllermgr = playerObject.GetComponent<ABC_Controller>();

                        if (controllermgr != null)
                        {
                            controllermgr.currentMana += 10f + PlayerPrefs.GetFloat("Level")*2.5f;
                        }

                    }
                    else if (objType == 2) // Exp totem
                    {
                        float currExp = PlayerPrefs.GetFloat("Experience");
                        float totemExp = 5f + PlayerPrefs.GetFloat("Level") * 5f;
                        PlayerPrefs.SetFloat("Experience", currExp + totemExp);
                    }
                    else if (objType == 3) // Coins
                    {
                        float currCoin = PlayerPrefs.GetFloat("coins");
                        float pickupCoin = 5f + PlayerPrefs.GetFloat("Level") * 5f;
                        PlayerPrefs.SetFloat("coins", currCoin + pickupCoin);
                    }



                }

                // Destroy the GameObject
                Destroy(gameObject);
            }
        }
    }
}