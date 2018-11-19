using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float x1, x2, y1, y2;
}

[System.Serializable]
public class Tilt {
    public float hTilt,vTilt;
}

[System.Serializable]
public class FireDelay {
    public float[] delay;
}
[System.Serializable]
public class BulletPrefab
{
    public GameObject[] gameObjects;
}

public class plane : MonoBehaviour {

    public float speed;
    public Tilt tilt;
    public Boundary bound;
    public FireDelay fireDelay;
    public BulletPrefab bulletPrefab;


    private Rigidbody2D rigidbody2D;
    private Vector2 velocity;
    private float coolDown;
    private GameObject currentBullet;
    private float curentBulletDelay;
    // Use this for initialization
    void Start () {
        // prevent caching
        rigidbody2D = GetComponent<Rigidbody2D>();
        coolDown = 0.0f;
        currentBullet = bulletPrefab.gameObjects[0];
        curentBulletDelay = fireDelay.delay[0];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (coolDown > 0) coolDown -= Time.deltaTime;
        velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        // get key input
        float kHorizontal = Input.GetAxis("Horizontal");
        float kVertical = Input.GetAxis("Vertical");
        bool mouseClick = Input.GetMouseButton(0);

        if(mouseClick && coolDown <0) {
            //   Instantiate(currentBullet,)
            Debug.Log("click");
        }

        // add new velocity
        velocity = new Vector3(kHorizontal, kVertical) * speed * Time.deltaTime;
        
        // just move if it inside limit
        Vector2 checkClamp = new Vector2
          (
            Mathf.Clamp(rigidbody2D.position.x + velocity.x * Time.deltaTime, bound.x1, bound.x2),
            Mathf.Clamp(rigidbody2D.position.y + velocity.y * Time.deltaTime, bound.y1, bound.y2)
          );
        // apply new velocity
        rigidbody2D.position = checkClamp;

        // just make tilt to beautiful
        gameObject.transform.rotation = Quaternion.Euler(velocity.y * tilt.vTilt, velocity.x * tilt.hTilt, 0);
    }
}
