using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class VRWandInteraction : MonoBehaviour {

    public float RayLength = 2;

    public bool Highlight = true;
    public Color HighlightColor = new Color();
    public Color GrabColor = new Color();

    GameObject m_ObjectInHand = null;
    GameObject m_CurrentObject = null;

    bool m_ObjectWasKinematic = true;

    private vrButtons m_Buttons = null;

    private bool m_SearchedButtons = false;

	// Use this for initialization
	void Start () {
        GameObject ray = GameObject.Find("WandRay");

        if (ray != null)
        {
            ray.transform.localScale = new Vector3( 0.01f, RayLength / 2.0f, 0.01f );
            ray.transform.localPosition = new Vector3( 0,0, RayLength / 2.0f );
        }
	}

    private Collider GetClosestHit()
    {
        // Detect objects
        RaycastHit[] hits;
        Vector3 dir = transform.localToWorldMatrix * Vector3.forward;

        hits = Physics.RaycastAll(transform.position, dir, RayLength);

        int i = 0;
        Collider closest = null;
        float distance = Mathf.Infinity;

        while (i < hits.Length)
        {
            RaycastHit hit = hits[i];

            //print("HIT : " + i + " : " + hit.collider.name);

            if( hit.distance < distance && hit.collider.name != "VRWand" && hit.collider.GetComponent<VRActor>() != null )
            {
                distance = hit.distance;
                closest = hit.collider;
            }

            i++;
        }

        return closest;
    }
	
    private void HighlightObject( GameObject obj, bool state )
    {
        HighlightObject(obj, state, HighlightColor);
    }


    private void HighlightObject( GameObject obj, bool state, Color hCol )
    {
        if (obj != null && obj.renderer != null && Highlight)
        {
            if( state )
            {
                obj.renderer.material.color = hCol;
            }
            else
            {
                m_CurrentObject.renderer.material.color = Color.white;
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if (m_Buttons == null)
        {
            string buttonsName = MiddleVR.VRDeviceMgr.GetWandButtonsName();
            m_Buttons = MiddleVRTools.GetButtons(buttonsName);
        }

        Collider hit = GetClosestHit();

        if( hit != null )
        {
            //print("Closest : " + hit.name);

            if( m_CurrentObject != hit.gameObject &&  m_ObjectInHand == null )
            {
                //print("Enter other : " + hit.name);
                HighlightObject( m_CurrentObject, false );

                m_CurrentObject = hit.gameObject;

                HighlightObject(m_CurrentObject, true );

                //print("Current : " + m_CurrentObject.name);
            }
        }
        // Else
        else
        {
            //print("No touch ! ");

            if (m_CurrentObject != null && m_CurrentObject != m_ObjectInHand)
            {
                HighlightObject(m_CurrentObject, false, HighlightColor );
                m_CurrentObject = null;
            }
        }

        //print("Current : " + m_CurrentObject);

        if (m_Buttons != null)
        {
            uint MainButton = MiddleVR.VRDeviceMgr.GetWandButton0();

            if( m_Buttons.IsToggled(MainButton) && m_CurrentObject != null )
            {
                //print("Trying to take :" + m_CurrentObject.name);
                if (m_CurrentObject.GetComponent("VRActor"))
                {
                    //print("Take :" + m_CurrentObject.name);

                    m_ObjectInHand = m_CurrentObject;
                    m_ObjectInHand.transform.parent = transform.parent;

                    if( m_ObjectInHand.rigidbody != null )
                    {
                        m_ObjectWasKinematic = m_ObjectInHand.rigidbody.isKinematic;
                        m_ObjectInHand.rigidbody.isKinematic = true;
                    }

                    HighlightObject(m_ObjectInHand, true, GrabColor);
                }
                /*
                if (m_CurrentObject.GetComponent("SwitchScript"))
                {
                    print("Switch : " + m_CurrentObject.name);
                    m_CurrentObject.SendMessage("Action");
                }*/
            }

            if (m_Buttons.IsToggled(MainButton, false) && m_ObjectInHand != null)
            {
                //print("Release : " + m_ObjectInHand);
                m_ObjectInHand.transform.parent = null;
                
                if (m_ObjectInHand.rigidbody != null)
                {
                    if( !m_ObjectWasKinematic )
                        m_ObjectInHand.rigidbody.isKinematic = false;
                }

                m_ObjectInHand = null;

                HighlightObject(m_CurrentObject, false, HighlightColor);

                m_CurrentObject = null;


            }
        }
        else
        {
            if (m_SearchedButtons == false)
            {
                print("[X] Failed to find buttons '" + MiddleVR.VRDeviceMgr.GetWandButtonsName() + "'. Please specify a WandButtonsName in the configuration tool.");
                m_SearchedButtons = true;
            }
        }
	}
}
