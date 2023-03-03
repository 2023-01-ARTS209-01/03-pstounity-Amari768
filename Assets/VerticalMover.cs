using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Vertical Mover moves from green location to red location
/// taking <para>timeToTravel</para> seconds to complete the 
/// journey.
/// 
/// Upon reaching the red location it flips its sprite and returns
/// to the green location taking the same amount of time to travel
/// </summary>
public class VerticalMover : MonoBehaviour
{
    [Header("Set to control speed and direction")]
    [Tooltip ("Will take this many seconds to move from point to point")]
    public float timeToTravel = 5;
    [Tooltip ("Will move from Greet to Red when checked, red to green when unchecked")]
    public bool movingDownward = true;
  
    
    private GameObject m_Mover;
    private Transform startPoint;
    private Transform endPoint;
    private SpriteRenderer sRend;
    private float distanceAlongPath;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = this.transform.GetChild(0); //first Child must be start point
        AlignWithParentVertical(startPoint);
        endPoint = this.transform.GetChild(1);   //second Child must be end Point
        AlignWithParentVertical(endPoint);
        m_Mover = this.transform.GetChild(2).gameObject; //thirdChild is the mover
        m_Mover.transform.position = startPoint.position;
        sRend = m_Mover.GetComponent<SpriteRenderer>();
        if (movingDownward) sRend.flipX = false; else sRend.flipX = true;

        distanceAlongPath = 0;
    }

    private void AlignWithParentVertical(Transform t)
    {
        Vector3 pos = t.localPosition;
        pos.x = 0;
        t.localPosition = pos;
    }

    // Update is called once per frame
    void Update()
    {
        distanceAlongPath += Time.deltaTime/timeToTravel;
        if (movingDownward)
        {
            m_Mover.transform.position = Vector3.Lerp(startPoint.position, endPoint.position,
                distanceAlongPath);
        } else
        {
            m_Mover.transform.position = Vector3.Lerp(endPoint.position, startPoint.position,
                           distanceAlongPath);
        }

        if(distanceAlongPath > 1.0f)
        {
            movingDownward = !movingDownward; //flip our direction
            sRend.flipX = !sRend.flipX;
            distanceAlongPath = 0;
        }
    }
}
