using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float rate, rotRate;
    private float deg = 0, height=0.2f;
    [SerializeField]
    Transform hip;
    public int shoot = 0;
    public GameObject ragdoll, normal, ragRoot, normalRoot;
    [SerializeField]
    GameObject uc_prefab;
    private GameObject tempUc;

    public static Player instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (shoot > 0)
        {
            shoot--;
            if (shoot == 0)
            {
                //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                deg = 0;
                transform.position = new Vector3(0, 0, -4);
                hip.eulerAngles = Vector3.zero;
                normal.SetActive(true);
                normalRoot.SetActive(true);
                Destroy(tempUc);
            }
        }
        else
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            transform.position += new Vector3(x * rate, 0, 0);
            var r = Input.GetAxis("Rotation");
            if (Mathf.Abs(deg + r * rotRate) < 70)
            {
                hip.Rotate(0, 0, r * rotRate);
                deg += r * rotRate;
            }
            if (Mathf.Abs(height + y / 20) < 0.2) {
                hip.position += new Vector3(0, y/20, 0);
                height += y / 20;
            }
        }
	}

    public void Shoot(int point)
    {
        tempUc = Instantiate(uc_prefab, transform.position, transform.rotation);
        var rig = tempUc.GetComponent<Rigidbody>();
        //rig.constraints = RigidbodyConstraints.None;       
        normal.SetActive(false);       
        normalRoot.SetActive(false);
        shoot = 100;
        rig.AddForce(Vector3.back * point / 10);
    }
}
