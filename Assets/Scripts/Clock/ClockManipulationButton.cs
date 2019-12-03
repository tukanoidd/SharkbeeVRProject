using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManipulationButton : MonoBehaviour
{
    private String type;
    private bool forward;
    private float minuteDegreeStep = 6;
    private float hourDegreeStep = 30;
    private GameObject targetColliderObject;

    private GameObject targetArrowPivotObject;

    private Vector3 startingPos;

    private bool moveBack = false;

    private void Awake()
    {
        startingPos = transform.position;
    }

    void Start()
    {
        type = name.Contains("Hours") ? "Hours" : "Minutes";
        forward = name.Contains("Forward");

        targetColliderObject = GameObject.Find((forward ? "Forward" : "Backwards") + type + "Collider");
        targetArrowPivotObject = GameObject.Find(type + "ArrowPivot");
    }

    private void Update()
    {
        if (moveBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPos, 1 * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, startingPos) < 0.01) moveBack = false;
        else if (Vector3.Distance(transform.position, startingPos) > 0.5) moveBack = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Hand"))
        {
            moveBack = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject == targetColliderObject)
        {
            targetArrowPivotObject.transform.Rotate(0, 0,
                type == "Hours"
                    ? (forward ? hourDegreeStep : -hourDegreeStep)
                    : (forward ? minuteDegreeStep : -minuteDegreeStep));
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name.Contains("Hand"))
        {
            moveBack = true;
        }
    }
}