using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHoopMovement : MonoBehaviour
{

    public List<Transform> positions;
    private float progress;
    public float step;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        //StartCoroutine(MoveGoalHoop());   
    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.position = Vector3.Lerp(startPos, positions[0].position, progress);
        progress += step;
        if(transform.position == positions[0].position)
        {
            positions.Add(positions[0]);
            positions.Remove(positions[0]);
            progress = 0;
            startPos = transform.position;
        }
        
    }
}

  /*  public IEnumerator MoveGoalHoop()
    {
       */

