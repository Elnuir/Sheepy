using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WallsDetector : MonoBehaviour
{
    public bool obstructionDetected;

    [SerializeField] GameObject followTransform;
    List<GameObject> obstructions = new List<GameObject>();
    Sheep sheep;
    public Vector3 direction, wallADirection, wallBDirection;
    float timeleft;
    [SerializeField] float startTime;
    Rigidbody rb;
    public bool isCornered;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sheep = GetComponent<Sheep>();
        timeleft = startTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Sheep"))
        {
            timeleft -= Time.deltaTime;
            if (timeleft <= 0)
            {
                obstructionDetected = true;
                print("In charge");
                if (!obstructions.Contains(other.gameObject))
                    obstructions.Add(other.gameObject);
                timeleft = startTime;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Sheep")))
        {
            print("Not in  charge");
            obstructionDetected = false;
            obstructions.Remove(other.gameObject);
        }

    }

    void Update()
    {

        if (obstructionDetected)
        {
            if (obstructions.Count == 1)
            {
                direction = (obstructions[0].transform.position - transform.position);
                followTransform.transform.rotation = Quaternion.LookRotation(-new Vector3(direction.x, 0, direction.z));
                isCornered = false;

            }
            else if (obstructions.Count == 2 && !sheep.isSpooked)
            {
                wallADirection = (obstructions[0].transform.position - transform.position);
                wallBDirection = (obstructions[1].transform.position - transform.position);
                direction = new Vector3(wallADirection.x, 0, wallADirection.z) + new Vector3(wallBDirection.x, 0, wallBDirection.z);
                followTransform.transform.rotation = Quaternion.LookRotation(direction);
                isCornered = false;
            }
            else if (obstructions.Count == 2 && sheep.isSpooked)
            {
                rb.velocity = Vector2.zero;
                direction = Vector2.zero;
                isCornered = true;
                
            }   
            sheep.direction = followTransform.transform.forward;
        }

    }
    


}