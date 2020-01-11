using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrabbableItem : MonoBehaviour
{
    private GameObject labelPrefab;
    public GameObject label;

    private Camera playerCam;
    private Collider grabbableItemCollider;
    private Renderer grabbableItemRenderer;

    private float labelOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();

        grabbableItemCollider = GetComponent<Collider>();

        var size = grabbableItemCollider.bounds.size;
        labelOffset = Mathf.Max(size.x, size.y, size.z) / 1.5f;

        labelPrefab = Resources.Load<GameObject>("Prefabs/UI/GrabbableLabel");
        label = Instantiate(labelPrefab, grabbableItemCollider.bounds.center + Vector3.up * labelOffset,
            Quaternion.identity);
        label.GetComponent<TextMeshPro>().text = name;

        grabbableItemRenderer = GetComponent<Renderer>();
        if (grabbableItemRenderer == null) grabbableItemRenderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbableItemRenderer.isVisible)
        {
            label.transform.position = grabbableItemCollider.bounds.center + Vector3.up * labelOffset;

            var rot = label.transform.rotation;
            label.transform.rotation = playerCam.transform.rotation;
        }
    }
}