using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    bool coroutineSwitch, isDirectionSet;
    public bool isSpooked;

    Rigidbody rb;
    public Vector3 direction;
    [SerializeField] float speed, minTimer, maxTimer, minSpeed, maxSpeed, lerpToDirectionSpeed, playerDetectionRadius, calmDownDistance;
    float currentTimer;
    public GameObject followTransform;
    Transform player;
    WallsDetector wallsDetector;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentTimer = Random.Range(minTimer, maxTimer);
        speed = Random.Range(minSpeed, maxSpeed);
        GetComponent<SphereCollider>().radius = playerDetectionRadius;
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
        wallsDetector = GetComponentInChildren<WallsDetector>();
        if(playerDetectionRadius > calmDownDistance)
        {
            Debug.LogWarning("Player detection radius is more than calm down distance. It may cause sheep jittering");
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        CloseToDistanationCheck();
        CalmDownCheck();
        isSpookedCheck();
    }
    void Move()
    {
        if(!isSpooked && !coroutineSwitch && !isDirectionSet && !wallsDetector.obstructionDetected)
        {
            followTransform.transform.rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
            direction = followTransform.transform.forward;
            coroutineSwitch = true;
            isDirectionSet = true;
        }
        else if(isSpooked && !wallsDetector.obstructionDetected)
        {
            Vector3 a = transform.position - player.transform.position;
            followTransform.transform.rotation = Quaternion.LookRotation(new Vector3(a.x, 0, a.z));
            direction = followTransform.transform.forward;
        }
        rb.velocity = direction.normalized * speed;
        transform.rotation = Quaternion.Lerp(transform.rotation, followTransform.transform.rotation, lerpToDirectionSpeed);
    }
    void CloseToDistanationCheck()
    {
        if(coroutineSwitch)
        {
            StartCoroutine(PatroolCountdown());
            coroutineSwitch = false;
        }
    }
    private IEnumerator PatroolCountdown()
    {
        while (!isSpooked && currentTimer >= 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            currentTimer -= Time.deltaTime;
        }
        currentTimer = Random.Range(minTimer, maxTimer);
        speed = Random.Range(minSpeed, maxSpeed);
        isDirectionSet = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isSpooked = true;
        }
    }
    void isSpookedCheck()
    {
        if (isSpooked)
        {
            coroutineSwitch = false;
            isDirectionSet = false;
            currentTimer = Random.Range(minTimer, maxTimer);
            speed = maxSpeed;
        }
    }
    void CalmDownCheck()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (isSpooked && distance >= calmDownDistance)
        {
            isSpooked = false;
        }
    }
    


}
