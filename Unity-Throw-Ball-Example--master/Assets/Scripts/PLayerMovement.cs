using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PLayerMovement : MonoBehaviour
{
    public List<Transform> poses;
/*    public Transform Pos1;
    public Transform Pos2;*/
    public float step;
    private float progress;
    public bool ture;
    public Transform superPos;
    public UnityEvent superCast;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (ture)
        {
           
            StartCoroutine(MovePlayerToPos());
            ture = false;
        }*/
    }

    private void Move() 
    {
        
    }

    public IEnumerator MovePlayerToPos()
    {
        Vector3 startPos = transform.position;
        while (transform.position != poses[0].position)
        {
            transform.position = Vector3.Lerp(startPos, poses[0].position, progress);
            progress += step;
            yield return null;

        }
        poses.Add(poses[0]);
        poses.Remove(poses[0]);
        progress = 0;
    }

    public IEnumerator MovePlayerToSuperPos()
    {
        Vector3 startPos = transform.position;
        while (transform.position != superPos.position)
        {
            transform.position = Vector3.Lerp(startPos, superPos.position, progress);
            progress += step;
            yield return null;

        }
        superCast.Invoke();

    }

 
}
