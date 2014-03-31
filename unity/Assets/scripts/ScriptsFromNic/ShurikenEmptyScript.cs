using UnityEngine;
using System.Collections;

public class ShurikenEmptyScript : MonoBehaviour
{
    public float emptyPullBackTmr;
    private float elapsedTime;
    public GameObject player;
    public Vector3 velocity;
    public Vector3 acceleration;

    //Variables for throwing the three shurikens
    public float throwStrength = 0;
    public float throwRotationStrength = 0;
    public GameObject shurikenPrefab;
    public int randMin, randMax;

    public float startingVelocity;
    bool throwShuriken = false;

    float lerpTime = 0;
    // Use this for initialization
    void Awake()
    {
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        ThrowShuriken();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= emptyPullBackTmr)
        {
            lerpTime += (Time.deltaTime);
            transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, player.transform.position.x, lerpTime),
                                             Mathf.Lerp(this.transform.position.y, player.transform.position.y, lerpTime),
                                             Mathf.Lerp(this.transform.position.z, player.transform.position.z, lerpTime));
        }
        else
        {
            velocity += acceleration * Time.deltaTime;
            transform.Translate(velocity * Time.deltaTime + (0.5f * acceleration * Time.deltaTime * Time.deltaTime), Space.World);
            acceleration = Vector3.zero;
        }
    }

    void ThrowShuriken()
    {
        //create the three shurikens
        GameObject s1 = Instantiate(shurikenPrefab, transform.position, Quaternion.Euler(transform.forward)) as GameObject;
        GameObject s2 = Instantiate(shurikenPrefab, transform.position, Quaternion.Euler(transform.forward)) as GameObject;
        GameObject s3 = Instantiate(shurikenPrefab, transform.position, Quaternion.Euler(transform.forward)) as GameObject;
        GameObject s4 = Instantiate(shurikenPrefab, transform.position, Quaternion.Euler(transform.forward)) as GameObject;
        //set bool for power Shurikens
        //s1.GetComponent<ShurikenScript> ().powerAttack = true;
        //s2.GetComponent<ShurikenScript> ().powerAttack = true;
        //s3.GetComponent<ShurikenScript> ().powerAttack = true;
        //s4.GetComponent<ShurikenScript> ().powerAttack = true;
        //move the first shuriken
        s1.GetComponent<ShurikenScript>().addForce(Random.Range(randMin, randMax), new Vector3(transform.forward.x - 1, transform.forward.y, transform.forward.z));
        s1.GetComponent<ShurikenScript>().addRotationalForce(throwRotationStrength, 16);
        //move the second shuriken
        s2.GetComponent<ShurikenScript>().addForce(Random.Range(randMin, randMax), new Vector3(transform.forward.x - 0.5f, transform.forward.y, transform.forward.z));
        s2.GetComponent<ShurikenScript>().addRotationalForce(throwRotationStrength, 16);
        //move the third shuriken
        s3.GetComponent<ShurikenScript>().addForce(Random.Range(randMin, randMax), new Vector3(transform.forward.x + 0.5f, transform.forward.y, transform.forward.z));
        s3.GetComponent<ShurikenScript>().addRotationalForce(throwRotationStrength, 16);
        //move the fourth shuriken
        s4.GetComponent<ShurikenScript>().addForce(Random.Range(randMin, randMax), new Vector3(transform.forward.x + 1, transform.forward.y, transform.forward.z));
        s4.GetComponent<ShurikenScript>().addRotationalForce(throwRotationStrength, 16);
        //pass this object to the shurikens so they know what to follow
        s1.GetComponent<ShurikenScript>().player = this.gameObject;
        s2.GetComponent<ShurikenScript>().player = this.gameObject;
        s3.GetComponent<ShurikenScript>().player = this.gameObject;
        s4.GetComponent<ShurikenScript>().player = this.gameObject;
    }
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("hit");
        //Destroy when collides with player that threw the empty
        //if (collider.gameObject == player && !(elapsedTime <= pullBackTmr))
        //Destroy (this.gameObject);
    }

    public void addForce(float _force, Vector3 _direction)
    {
        acceleration += _direction.normalized * _force;
    }
}
