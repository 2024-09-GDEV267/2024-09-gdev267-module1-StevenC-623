using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static float bottomY = -15f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Brick"))
        {
            StartCoroutine(BrickRemovalDelay(2f,collidedWith));
        }
        if(collidedWith.CompareTag("PinkBrick"))
        {
            StartCoroutine(BrickRemovalDelay(2f, collidedWith));
            //Tell manager score +100
            //(in manager) - test level to see if it has anymore pinkbricks
            // if not- load a new level/ "you win"
        }
    }
    private IEnumerator BrickRemovalDelay(float delay,GameObject brick)
    {
        yield return new WaitForSeconds(delay);
        Destroy(brick);

    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedWith = other.gameObject;
        if (collidedWith.CompareTag("Hoop"))
        {
            Debug.Log("extraBall");
        }
    }
}
