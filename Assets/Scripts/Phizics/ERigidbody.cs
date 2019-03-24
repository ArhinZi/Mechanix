using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum Material
{
    Wood,
    CastIron,
    Cuprum,
    Rubber,
    Glass,
    Zinc
}





public class ERigidbody : MonoBehaviour
{

    public Rigidbody2D rb;
    private GameObject prefForce;
    public Dictionary<string, Force> forces = new Dictionary<string, Force>();
    private bool display_forces = false;
    private List<LineRenderer> forcesGO = new List<LineRenderer>();

    public Timer timer;
    public myTime myTime;
    public GameObject timerColl = null;


    /*Parameters for simulation*/
    Material material = Material.Wood;//Material for simulate the friction
    public PhysicsMaterial2D phizMaterial = null;
    public bool simulated = true;//Simulation object's phisics
    public float mass = 1;
    public float grav_scale = 1;
    private bool _freezeX;//axisX
    private bool _freezeY;//axisY
    private bool _freezeZ;//axisZ
    /*###*/


    /*Noname parameters*/
    private Vector2 lastVelocity = Vector2.zero;
    private Vector2 acceleration = Vector2.zero;
    /*###*/


    /*Built-In Functions*/
    void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        myTime = GameObject.Find("System/Time").GetComponent<myTime>();
        prefForce = (GameObject)Resources.Load("Prefs/Force", typeof(GameObject));
        phizMaterial = new PhysicsMaterial2D("DefMat");
        phizMaterial.friction = 0;
        phizMaterial.bounciness = 0;
        rb.gravityScale = 0;
        PhizMaterial = phizMaterial;



    }

    void FixedUpdate()
    {
        forces = new Dictionary<string, Force>();
        AddGravity();
        AddFriction();
        foreach (KeyValuePair<string, Force> item in forces)
        {
            if (!item.Value.isfict)
            {
                rb.AddForceAtPosition(item.Value.force_value, item.Value.position);
            }
        }

    }

    void Update()
    {

    }

    private void OnEnable()
    {
        try
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            Simulated = simulated;
            PhizMaterial = phizMaterial;
            Mass = mass;
            GravScale = grav_scale;
        }
        catch { }
    }

    private void OnDisable()
    {
        Destroy(rb);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (timerColl != null && coll.gameObject == timerColl.gameObject) myTime.SetPause(true);
    }
    /*###*/


    /*Public management methods*/
    public float PosX
    {
        set
        {
            rb.transform.position = new Vector3(value, rb.transform.position.y, rb.transform.position.z);
        }
        get
        {
            return rb.transform.position.x;
        }
    }
    public float PosY
    {
        set
        {
            rb.transform.position = new Vector3(rb.transform.position.x, value, rb.transform.position.z);
        }
        get
        {
            return rb.transform.position.y;
        }
    }
    public bool Simulated
    {
        set
        {
            simulated = value;
            rb.simulated = value;
        }
        get
        {
            return simulated;
        }
    }
    public bool DisplayForces
    {
        set
        {
            display_forces = value;
            if (value) SetDispForces();
            else UnsetDispForces();
        }
        get
        {
            return display_forces;
        }
    }
    public float Mass
    {
        set
        {
            mass = value;
        }
        get
        {
            return mass;
        }
    }
    public Vector2 Velocity
    {
        set
        {
            rb.velocity = value;
        }
        get
        {
            return rb.velocity;
        }
    }
    public float AngVelocity
    {
        set
        {
            rb.angularVelocity = value;
        }
        get
        {
            return rb.angularVelocity;
        }
    }

    public PhysicsMaterial2D PhizMaterial
    {
        set
        {
            phizMaterial = value;
            rb.sharedMaterial = value;
        }
        get
        {
            return phizMaterial;
        }
    }
    public float GravScale
    {
        set
        {
            grav_scale = value;
        }
        get
        {
            return grav_scale;
        }
    }
    public bool freezeX
    {
        set
        {
            _freezeX = value;
            if (_freezeX)
                rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
            else
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;

            if (freezeX && freezeY && freezeZ)
                rb.bodyType = RigidbodyType2D.Static;
            else
                rb.bodyType = RigidbodyType2D.Dynamic;
        }
        get
        {
            return _freezeX;
        }
    }
    public bool freezeY
    {
        set
        {
            _freezeY = value;
            if (_freezeY)
                rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
            else
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

            if (freezeX && freezeY && freezeZ)
                rb.bodyType = RigidbodyType2D.Static;
            else
                rb.bodyType = RigidbodyType2D.Dynamic;
        }
        get
        {
            return _freezeY;
        }
    }
    public bool freezeZ
    {
        set
        {
            _freezeZ = value;
            if (_freezeZ)
                rb.constraints |= RigidbodyConstraints2D.FreezeRotation;
            else
                rb.constraints &= ~RigidbodyConstraints2D.FreezeRotation;

            if (freezeX && freezeY && freezeZ)
                rb.bodyType = RigidbodyType2D.Static;
            else
                rb.bodyType = RigidbodyType2D.Dynamic;
        }
        get
        {
            return _freezeZ;
        }
    }

    public void CollisionDetection(CollisionDetectionMode2D a)
    {
        rb.collisionDetectionMode = a;
    }
    /*public void AddForce(Vector2 Force, ForceMode2D mode, string f_name = "UserForce")
    {
        rb.AddForce(Force, mode);
    }*/
    public void AddForceAtPosition(Vector2 Force, Vector2 Position, ForceMode2D mode, string f_name = "UserForceAtPos")
    {
        rb.AddForceAtPosition(Force, Position, mode);
        forces.Add("AddForce", new Force("AddForce", this.gameObject, Position, Force, true, new Color(1, 1, 0)));
        //Forces.mForces.Add(new Force(f_name, this.gameObject, Position, Force));
    }
    public void AddConstForceAtPosition(Vector2 Force, Vector2 Position, ForceMode2D mode, string f_name = "UserConsForceAtPos")
    {
        //Forces.cForces.Add(new Force(f_name, this.gameObject, Position, Force));
        //Debug.Log("AddForce-"+ this.gameObject.name+": "+Force.magnitude);
    }
    /*###*/


    private void AddGravity()
    {
        string name = "Gravity";
        Vector2 value = Vector2.down * PhizSettings.WorldGrav * PhizSettings.scale * GravScale * 10 * mass;
        Vector2 position = rb.gameObject.transform.position;
        forces.Add(name, new Force(name, this.gameObject, position, value, false, new Color(0, 0, 1)));
    }
    private void AddFriction()
    {
        Vector2 vel = Velocity;
        Vector2[] planar_contacts = new Vector2[10];
        Vector2[] pointer_contacts = new Vector2[10];
        ContactPoint2D[] contactPoints = new ContactPoint2D[10];
        float frictStatic_k = 1;
        float frictSlide_k = 0.5f;
        float frictRolling_k = 1;
        int k = rb.GetContacts(contactPoints);
        int z = 1000;
        for (int i = 0; i < k; i++)
        {
            if (contactPoints[i].rigidbody == contactPoints[i + 1].rigidbody)
            {
                Vector2 point = Vector2.Lerp(contactPoints[i].point, contactPoints[i + 1].point, 0.5f);
                Vector2 normal = contactPoints[i].normal;
                Vector2 N = normal * (contactPoints[i].normalImpulse + contactPoints[i + 1].normalImpulse) * mass * z;
                forces.Add("N" + i, new Force("N", this.gameObject, point, N, true, new Color(0, 1, 0)));
                Vector2 frict_norm = contactPoints[i].relativeVelocity.normalized;
                if (frict_norm != Vector2.zero)
                {
                    float VelOnNormal = Vector2.Dot(vel, frict_norm);
                    Vector2 Frict = N.magnitude * frict_norm * frictSlide_k;
                    //Debug.Log(Frict.magnitude);
                    //Debug.Log(VelOnNormal);
                    if (Frict.magnitude > Mathf.Abs(VelOnNormal)*1000) 
                    {
                        Frict = VelOnNormal * frict_norm* -1000;
                    }
                    forces.Add("Friction" + i, new Force("Friction", this.gameObject, point, Frict, false, new Color(1, 0, 0)));
                }
                i++;
            }
            else if (((myPrimitive)rb.GetComponent<BodyCtrl>().myBody).bt == myPrimitive.BodyType.Rectangle)
            {
                Vector2 point = contactPoints[i].point;
                Vector2 normal = contactPoints[i].normal;
                Vector2 N = normal * (contactPoints[i].normalImpulse) * z;
                forces.Add("N" + i, new Force("N", this.gameObject, point, N, true, new Color(0, 1, 0)));
                Vector2 frict_norm = contactPoints[i].relativeVelocity.normalized;
                if (frict_norm != Vector2.zero)
                {
                    float VelOnNormal = Vector2.Dot(vel, frict_norm);
                    Vector2 Frict = N.magnitude * frict_norm * frictSlide_k;

                    if (Frict.magnitude > Mathf.Abs(VelOnNormal)*1000) 
                    {
                        Frict = VelOnNormal * frict_norm* -1000;
                    }
                    forces.Add("Friction" + i, new Force("Friction", this.gameObject, point, Frict, false, new Color(1, 0, 0)));
                }
            }
            else
            {
                float radius = rb.transform.localScale.x / 2;
                float f_tk = frictRolling_k / radius;
                Vector2 point = contactPoints[i].point;
                Vector2 normal = contactPoints[i].normal;
                Vector2 N = normal * (contactPoints[i].normalImpulse) * z;
                forces.Add("N" + i, new Force("N", this.gameObject, point, N, true, new Color(0, 1, 0)));
                Vector2 frict_norm = contactPoints[i].relativeVelocity.normalized;
                if (frict_norm != Vector2.zero)
                {
                    float VelOnNormal = Vector2.Dot(vel, frict_norm);
                    Vector2 Frict = N.magnitude * frict_norm * f_tk;

                    if (Frict.magnitude > VelOnNormal*10) Frict = VelOnNormal * frict_norm*10;
                    forces.Add("Friction" + i, new Force("Friction", this.gameObject, point, Frict, false, new Color(1, 0, 0)));
                }
            }
        }
    }
    public void SetDispForces()
    {
        if (display_forces)
        {
            if (forcesGO.Count != forces.Count)
            {
                for (int i = 0; i < forcesGO.Count; i++)
                {
                    Destroy(forcesGO[i].gameObject);
                }
                forcesGO = new List<LineRenderer>();
                for (int i = 0; i < forces.Count; i++)
                {
                    forcesGO.Add(Instantiate(prefForce).GetComponent<LineRenderer>());
                }
                return;
            }
            else
            {

            }

        }
    }
    public List<string> DispForces()
    {
        if (display_forces)
        {
            List<string> ss = new List<string>();
            int i = 0;
            if (forcesGO.Count == forces.Count)
                foreach (KeyValuePair<string, Force> item in forces)
                {
                    if (rb.bodyType == RigidbodyType2D.Static) break;
                    Vector2 pos1 = item.Value.position;
                    Vector2 pos2 = pos1 + item.Value.force_value * PhizSettings.KForce;
                    Vector3[] k = { pos1, pos2 };
                    forcesGO[i].SetPositions(k);
                    forcesGO[i].SetColors(item.Value.color, item.Value.color);
                    ss.Add(item.Value.name + (Vector2)(item.Value.position) + ": " + item.Value.force_value + " mH");
                    i++;
                }
            return ss;
        }
        return null;
    }
    public void UnsetDispForces()
    {
        if (!display_forces)
        {
            for (int i = 0; i < forcesGO.Count; i++)
            {
                Destroy(forcesGO[i].gameObject);
            }
            forcesGO = new List<LineRenderer>();
        }
    }


}
