using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour /*, IPointerDownHandler, IBeginDragHandler, IEndDragHandler*/
{
    [SerializeField]private Color hoverColor;
    [SerializeField]private Color normalColor;
    [SerializeField] private GameObject turrent;
    private Renderer rend;
    private Vector3 positionOffset = new Vector3(0f,0.5f,0f);

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        normalColor = rend.material.color;
        hoverColor = normalColor;
        hoverColor.g = 0;

        buildManager = BuildManager.instance;
    }

    private void OnMouseDown()
    {
        if (buildManager.GetTurrentToBuild() == null)
            return;

        if(turrent!= null)
        {
            Debug.Log("Slot cheio");
            return;
        }

        BuildTorrent();
    }

    private void OnMouseEnter()
    {
        if (buildManager.GetTurrentToBuild() == null)
            return;

        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color= normalColor;
    }

    private void OnMouseDrag()
    {
        Debug.Log("begin");
    }

    private void BuildTorrent()
    {
        //Build a turrent
        GameObject turretToBuild = BuildManager.instance.GetTurrentToBuild();
        turrent = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, Quaternion.identity);
    }
}
