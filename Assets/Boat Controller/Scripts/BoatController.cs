using System.Collections;
using TouchControlsKit;
using UnityEngine;

public class BoatController : MonoBehaviour
{

    public bool canControl = true;
    //Engine sound
    public AudioClip engineSound;
    public GameObject crashSound;
    //Particle system used for foam from the boat's propeller
    public GameObject engineSpume;
    public GameObject engineSpumeTrans;
    //Boat Mass
    public float mass = 3000.0f;
    //Boat motor force
    public float motorForce = 10000.0f;
    //Rudder sensivity
    public int rudderSensivity = 45;
    //Angular drag coefficient
    public float angularDrag = 0.8f;
    //Center of mass offset
    public float cogY = -0.5f;
    //Volume of boat in liters (the higher the volume, the higher the boat will floar)
    public int volume = 9000;
    //Max width, height and length of the boat (used for water dynamics)
    public Vector3 size = new Vector3(3, 3, 6);

    public float motor;
    public float steer;
    public static bool mute;

    //Drag coefficients along x,y and z directions
    private Vector3 drag = new Vector3(6.0f, 4.0f, 0.2f);
    private float rpmPitch = 0.0f;
    public WaterSurface waterSurface;
    public bool isUsingSteering;
    // Use this for initialization
    void Start()
    {
        //Setup rigidbody
        if (!GetComponent<Rigidbody>())
        {
            gameObject.AddComponent<Rigidbody>();
        }
        GetComponent<Rigidbody>().mass = mass;
        GetComponent<Rigidbody>().drag = 0.1f;
        GetComponent<Rigidbody>().angularDrag = angularDrag;
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, cogY, 0);
        GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

        //start engine noise
        if (!GetComponent<AudioSource>())
        {
            gameObject.AddComponent<AudioSource>();
        }
        GetComponent<AudioSource>().clip = engineSound;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();

        mute = false;
    }


    void Update()
    {   //Debug.Log(gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        if (engineSpume != null)
        {
            if (motor > 0 || motor < 0 || gameObject.GetComponent<Rigidbody>().velocity.magnitude > 3.0f)
            {
                GameObject boatFoam = (GameObject)Instantiate(engineSpume, engineSpumeTrans.transform.position, engineSpumeTrans.transform.rotation);
                boatFoam.GetComponent<ParticleSystem>().enableEmission = true;
                boatFoam.GetComponent<ParticleSystem>().playbackSpeed = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                Destroy(boatFoam, 0.8f);
            }
        }

        if (mute)
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().mute = false;
        }

        if (InputManager.GetButton("Break"))
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0.9f;
        }
    }
    // Update is called once per frame

    public float speed;
    void FixedUpdate()
    {

        //If there is no water surface we are colliding with, no boat physics	
        if (waterSurface == null)
        {
            //	Debug.Log("FIX");
            return;

        }

        motor = 0.0f;
        steer = 0.0f;

        if (canControl)
        {
            //			motor = Input.GetAxis("Vertical");
            //			steer = Input.GetAxis("Horizontal");
            motor = InputManager.GetAxis("Joystick0", "Vertical");

            switch (isUsingSteering)
            {
                case true:

                    steer = InputManager.GetAxis("SteeringWheel0", "Horizontal") * Time.deltaTime * speed;
                    break;

                case false:
                    steer = SimpleInput.GetAxis("Horizontal") * Time.deltaTime * speed;
                    break;

            }


        }

        //Get water level and percent under water
        float waterLevel = waterSurface.GetComponent<Collider>().bounds.max.y;
        float distanceFromWaterLevel = transform.position.y - waterLevel;
        float percentUnderWater = Mathf.Clamp01((-distanceFromWaterLevel + 0.5f * size.y) / size.y);


        //BUOYANCY (the force which keeps the boat floating above water)
        //_______________________________________________________________________________________________________

        //the point the buoyancy force is applied onto is calculated based 
        //on the boat's picth and roll, so it will always tilt upwards:
        Vector3 buoyancyPos = new Vector3();
        buoyancyPos = transform.TransformPoint(-new Vector3(transform.right.y * size.x * 0.5f, 0, transform.forward.y * size.z * 0.5f));

        //then it is shifted arcording to the current waves
        buoyancyPos.x += waterSurface.waveXMotion1 * Mathf.Sin(waterSurface.waveFreq1 * Time.time)
                    + waterSurface.waveXMotion2 * Mathf.Sin(waterSurface.waveFreq2 * Time.time)
                    + waterSurface.waveXMotion3 * Mathf.Sin(waterSurface.waveFreq3 * Time.time);
        buoyancyPos.z += waterSurface.waveYMotion1 * Mathf.Sin(waterSurface.waveFreq1 * Time.time)
                    + waterSurface.waveYMotion2 * Mathf.Sin(waterSurface.waveFreq2 * Time.time)
                    + waterSurface.waveYMotion3 * Mathf.Sin(waterSurface.waveFreq3 * Time.time);

        //apply the force
        GetComponent<Rigidbody>().AddForceAtPosition(-volume * percentUnderWater * Physics.gravity, buoyancyPos);

        //ENGINE
        //_______________________________________________________________________________________________________

        //calculate propeller position
        Vector3 propellerPos = new Vector3(0, -size.y * 0.5f, -size.z * 0.5f);
        Vector3 propellerPosGlobal = transform.TransformPoint(propellerPos);

        //apply force only if propeller is under water
        if (propellerPosGlobal.y < waterLevel)
        {
            //direction propeller force is pointing to.
            //mostly forward, rotated a bit according to steering angle
            float steeringAngle = steer; //* rudderSensivity / 10 * Mathf.Deg2Rad;
            Vector3 propellerDir = transform.forward * Mathf.Cos(steeringAngle) - transform.right * Mathf.Sin(steeringAngle);
            //Debug.Log("StreeringAngle" + steeringAngle);
            //apply propeller force
            GetComponent<Rigidbody>().AddForceAtPosition(propellerDir * motorForce * motor, propellerPosGlobal);

            //create particles for propeller
            //			if(engineSpume!=null)
            //			{
            //				engineSpume.position = propellerPosGlobal;
            //				engineSpume.position.y = waterLevel-0.5f;
            //				engineSpume.particleEmitter.worldVelocity = rigidbody.velocity*0.5f-propellerDir*10*motor+Vector3.up*3*Mathf.Clamp01(motor);
            //				engineSpume.GetComponent<ParticleEmitter>().minEmission = Mathf.Abs(motor);
            //				engineSpume.GetComponent<ParticleEmitter>().maxEmission = Mathf.Abs(motor);
            //				engineSpume.GetComponent<ParticleEmitter>().Emit();	
            //
            //			}
        }

        //DRAG
        //_______________________________________________________________________________________________________

        //calculate drag force
        Vector3 dragDirection = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
        Vector3 dragForces = -Vector3.Scale(dragDirection, drag);

        //depth of the boat under water (used to find attack point for drag force)
        float depth = Mathf.Abs(transform.forward.y) * size.z * 0.5f + Mathf.Abs(transform.up.y) * size.y * 0.5f;

        //apply force
        Vector3 dragAttackPosition = new Vector3(transform.position.x, waterLevel - depth, transform.position.z);
        GetComponent<Rigidbody>().AddForceAtPosition(transform.TransformDirection(dragForces) * GetComponent<Rigidbody>().velocity.magnitude * (1 + percentUnderWater * (waterSurface.waterDragFactor - 1)), dragAttackPosition);

        //linear drag (linear to velocity, for low speed movement)
        GetComponent<Rigidbody>().AddForce(transform.TransformDirection(dragForces) * 500);

        //rudder torque for steering (square to velocity)
        float forwardVelo = Vector3.Dot(GetComponent<Rigidbody>().velocity, transform.forward);
        GetComponent<Rigidbody>().AddTorque(transform.up * forwardVelo * forwardVelo * rudderSensivity * steer);

        //SOUND
        //_______________________________________________________________________________________________________

        GetComponent<AudioSource>().volume = 0.3f + Mathf.Abs(motor);

        //slowly adjust pitch to power input
        rpmPitch = Mathf.Lerp(rpmPitch, Mathf.Abs(motor), Time.deltaTime * 0.4f);
        GetComponent<AudioSource>().pitch = 0.3f + 0.7f * rpmPitch;

        //reset water surface, so we have to stay in contact for boat physics.
        //		waterSurface = null; 
    }

    public bool steering;
    IEnumerator Steer()
    {
        while (steering)
        {
            yield return null;
            //if (steer >= 3) break;
            steer += 0.5f;
        }
    }



    //Check if we inside water area


    void OnTriggerStay(Collider col)
    {
        if (col.GetComponent<WaterSurface>() != null)
        {
            //			Debug.Log (1);
            waterSurface = col.GetComponent<WaterSurface>();
        }
    }


    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "Environment")
        {

            ContactPoint contact = coll.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            GameObject crashClip = (GameObject)Instantiate(crashSound, pos, rot);
            crashClip.GetComponent<AudioSource>().Play();
            gameObject.SendMessage("Damage");
            Destroy(crashClip, crashClip.GetComponent<AudioSource>().clip.length);
        }
    }
}
