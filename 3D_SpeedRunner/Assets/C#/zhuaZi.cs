using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zhuaZi : MonoBehaviour
{
    [Header("References")]
    //private PL_move pm;
    public GameObject PL_po;
    public Transform pl;
    public Transform gunTip;
    public LayerMask whatisGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDis;
    public float grappleDelayTime;

    static public Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    public bool grappling;
    // Start is called before the first frame update
    void Start()
    {
        //pm = GetComponent<PL_move>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(PL_po.transform.position.x, PL_po.transform.position.y, PL_po.transform.position.z);

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;
    }
    private void LateUpdate()
    {
        if (grappling)
        {
            lr.SetPosition(0, gunTip.position);
        }
    }
    public void StarGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grappling = true;

        RaycastHit hit;
        if (Physics.Raycast(pl.position, pl.forward, out hit, maxGrappleDis))
        {
            grapplePoint = hit.point;

            Excutegrapple();
        }
        else
        {
            grapplePoint = pl.position + pl.forward * maxGrappleDis;
            Invoke(nameof(Stopgrapple), grappleDelayTime);
        }
        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }
    public void Excutegrapple()
    {
        GameObject.Find("FPScontroller").GetComponent<PL_move>().grappling();
        PL_move.grapple = true;
        print(1);
        Invoke(nameof(ResetGrapple), 0.3f);
    }
    public void Stopgrapple()
    {
        grappling = false;

        print(0);
        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
    }
    public void ResetGrapple()
    {
        PL_move.grapple = false;
        PL_move.grapplingSpeed = 0;
        grappling = false;

        print(0);
        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
        GameObject.Find("FPScontroller").GetComponent<PL_move>().EndGrapple();
        PL_move.ToolScore = PL_move.ToolScore + 150;
    }
}
