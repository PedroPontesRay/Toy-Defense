using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawnLine : MonoBehaviour
{

    public Transform[] waypoints;

    private void OnDrawGizmos()
    {
        for (int i = 1;i < waypoints.Length; i++)
        {
            Debug.DrawLine(waypoints[i-1].position, waypoints[i].position, Color.red);
        }
    }
}
