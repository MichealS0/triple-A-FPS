using UnityEngine;

public class GunEquip : MonoBehaviour
{
    public GunScript gunscript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickupRange, dropForward, dropBackward;

    public bool equipped;
    public static bool slotFull;

    void Start()
    {
        if (!equipped)
        {
            gunscript.enabled = false;
            rb.isKinematic = false;
        }

        if(equipped)
        {
            gunscript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    public void Update()
    {
        Vector3  distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFull) Pickup();

        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    void Pickup()
    {
        equipped = true;
        slotFull = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;

        gunscript.enabled = true;
    }

    void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity =  player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropForward, ForceMode.Impulse);
        rb.AddForce(fpsCam.forward * dropBackward, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10f);

        gunscript.enabled = false;
    }

    // -- Back At It Again
}
