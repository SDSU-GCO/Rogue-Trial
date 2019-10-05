using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{

    public class OneHSwordAttack : MonoBehaviour
    {
        public GameObject player;
        public GameObject child;
        void Awake()
        {



            float offset = 1;
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 spawnOffset = (mouseWorldPosition - (Vector2)transform.position).normalized * offset;
            Vector3 spawnLocation = transform.position + (Vector3)spawnOffset;
            spawnLocation.z = this.transform.position.z;


            Vector2 direction = spawnLocation - transform.position;
            float rotation = Mathf.Rad2Deg * (Mathf.Atan(direction.y / direction.x));
            rotation += -90;
            if (direction.x < 0)
            {
                rotation += 180;
            }

            GameObject child = Instantiate(this.gameObject, spawnLocation, Quaternion.Euler(0, 0, rotation));
            child.transform.SetParent(transform);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                FireAttack();
            }
        }

        public void FireAttack()
        {
            float offset = 1;
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 spawnOffset = (mouseWorldPosition - (Vector2)transform.position).normalized * offset;
            Vector3 spawnLocation = transform.position + (Vector3)spawnOffset;
            spawnLocation.z = this.transform.position.z;


            Vector2 direction = spawnLocation - transform.position;
            float rotation = Mathf.Rad2Deg * (Mathf.Atan(direction.y / direction.x));
            rotation += -90;
            if (direction.x < 0)
            {
                rotation += 180;
            }

            GameObject child = Instantiate(this.gameObject, spawnLocation, Quaternion.Euler(0, 0, rotation));
            child.transform.SetParent(transform);
        }
    }
}