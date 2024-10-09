using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    public GameObject launchPoint;
    public GameObject rotationPoint;
    private GameObject ballInst;
    public float velocityMult = 5f;
    private bool shootable = true;

 
    private void Update()
    {
        HandleGunShooting();
    }
    private void HandleGunShooting()
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = new Vector3(
            mousePosition.x - launchPoint.transform.position.x, mousePosition.y - launchPoint.transform.position.y,0);

        if (Input.GetMouseButtonUp(0) && shootable == true)
        {
            ballInst = Instantiate(ball, launchPoint.transform.position, launchPoint.transform.rotation);
            Rigidbody ballRB = ballInst.GetComponent<Rigidbody>();
            ballRB.velocity =  direction * velocityMult;
            //decrease shot count in manager
            //(in manager) if shot count = 0 you lose
            
            //shootable = false;
            //StartCoroutine(ShotDelay(2f));
        }
    }
    private IEnumerator ShotDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        shootable = true;
    }
}