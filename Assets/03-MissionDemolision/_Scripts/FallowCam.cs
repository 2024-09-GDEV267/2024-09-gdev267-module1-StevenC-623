using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Apple;

public class FallowCam : MonoBehaviour
{
    static private FallowCam S;
    static public GameObject POI;
    public enum eView { none, slingshot, castle, both };

    [Header("Inscribed")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    public GameObject viewBothGO;

    [Header("Dynamic")]
    public float camZ;
    public eView nextView = eView.slingshot;
    // Start is called before the first frame update
    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 destination = Vector3.zero;
        if (POI != null)
        {
            Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
            if ((poiRigid != null) && poiRigid.IsSleeping())
            {
                POI = null;
            }
        }
        if (POI != null)
        {
            destination = POI.transform.position;
            
        }

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;

    }
    public void SwitchView(eView newView)
    {         
        if (newView == eView.none)
        {
           newView = nextView;
        }
        switch (newView)
        {                                                  
        case eView.slingshot:
            POI = null;
            nextView = eView.castle;
            break;
        case eView.castle:
            POI = MissionDemolition.GET_CASTLE();                               // h
            nextView = eView.both;
            break;
        case eView.both:
            POI = viewBothGO;
            nextView = eView.slingshot;
            break;
        }
    }
     public void SwitchView()
     {                                                 
        SwitchView(eView.none);
     }
 
     static public void SWITCH_VIEW(eView newView)
     {                   
        S.SwitchView(newView);
     }
 }
