using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AvatarCollisionCreator : MonoBehaviour
{ 
    [Header("Collision Settings")]

    [Tooltip("Choose whether the colliders added to the hand(s) are triggers or not")]
    public bool collidersAreTriggers = false;

    [Tooltip("Choose whether to add rigidbodies to the hands or not. Rigidbodies will be added with isKinematic set to true and useGravity set to false.")]
    public bool addRigidbodies = true;

    [Tooltip("Choose whether colliders are added to the right hand")]
    public bool addCollidersToRightHand = true;

    [Tooltip("Choose whether colliders are added to the left hand")]
    public bool addCollidersToLeftHand = true;


    private readonly FingerBone Phalanges = new FingerBone(0.01f, 0.03f);
    private readonly FingerBone Metacarpals = new FingerBone(0.01f, 0.05f);
    private List<Hand> hands;

    void Start()
    {
        //set up our list of hands
        hands = new List<Hand>();

        //if right hand is enabled let's initialize a new hand obj
        if (addCollidersToRightHand) hands.Add(new Hand(true));

        //if keft hand is enabled let's initialize a new hand obj
        if (addCollidersToLeftHand) hands.Add(new Hand(false));

        //run this repeatedly until we find the objects we need
        InvokeRepeating("Initialize", 1f, 1f);
    }

    void Initialize()
    {
        foreach(Hand h in hands)
        {
            //let's find the needed transforms
            if (!h.hasBeenFound) FindAndModifyBoneTransforms(h);
        }

        bool isCompleted = true;

        //need to check if all the hands have been found 
        foreach(Hand h in hands)
        {
            //if at least one hand hasn't been found then we need to continue searching
            if (!h.hasBeenFound) isCompleted = false;
        }

        if (isCompleted) CancelInvoke();
    }

    void FindAndModifyBoneTransforms(Hand hand)
    {
        string handString = hand.isRightHand ? "hand_right" : "hand_left";
        if (!GameObject.Find(handString)) Debug.LogWarning("Failed to find " + handString + " . Make sure LocalAvatar is in your scene and that you are wearing a headset and can see the hands in the scene.");
        else
        {
            //gather all of the bone transforms
            hand.bones = GameObject.Find(handString).transform.GetComponentsInChildren<Transform>().ToList();
            if (hand.bones.Count > 1)
            {
                foreach (Transform handBone in hand.bones)
                {
                    if (!handBone.name.Contains("ignore"))
                    {
                        //now let's create colliders on all the objects 
                        CreateCollider(handBone.gameObject);
                    }
                }
                hand.hasBeenFound = true;
            }
        }
    }

    void CreateCollider(GameObject bone)
    {
        if (bone.transform.name.Contains("thumb") ||
            bone.transform.name.Contains("index") ||
            bone.transform.name.Contains("middle") ||
            bone.transform.name.Contains("ring") ||
            bone.transform.name.Contains("pinky"))
        {
            if (!bone.transform.name.EndsWith("0"))
            {
                CapsuleCollider collider = bone.transform.gameObject.AddComponent<CapsuleCollider>();             
                collider.isTrigger = collidersAreTriggers;

                if(addRigidbodies)
                {
                    if(bone.transform.gameObject.GetComponent<Rigidbody>() == null)
                    {
                        Rigidbody rb = bone.transform.gameObject.AddComponent<Rigidbody>();
                        rb.useGravity = false;
                        rb.isKinematic = true;
                    }
                }
                if (!bone.transform.name.EndsWith("1"))
                {
                    collider.radius = Phalanges.Radius;
                    collider.height = Phalanges.Height;
                    collider.center = Phalanges.GetCenter(bone.transform.name.Contains("_l_"));
                    collider.direction = 0;
                }
                else
                {
                    collider.radius = Metacarpals.Radius;
                    collider.height = Metacarpals.Height;
                    collider.center = Metacarpals.GetCenter(bone.transform.name.Contains("_l_"));
                    collider.direction = 0;
                }
            }
        }
        else if (bone.transform.name.Contains("grip"))
        {
            SphereCollider collider = bone.transform.gameObject.AddComponent<SphereCollider>();
            collider.radius = 0.04f;
            collider.center = new Vector3(
                ((bone.transform.name.Contains("_l_")) ? -1 : 1) * 0.01f,
                0.01f, 0.02f);
            collider.isTrigger = false;
        }

    }


    private struct FingerBone
    {
        public readonly float Radius;
        public readonly float Height;

        public FingerBone(float radius, float height)
        {
            Radius = radius;
            Height = height;
        }

        public Vector3 GetCenter(bool isLeftHand)
        {
            return new Vector3(((isLeftHand) ? -1 : 1) * Height / 2.0f, 0, 0);
        }
    };

}

public class Hand
{
    public bool isRightHand;
    public bool hasBeenFound;
    public List<Transform> bones;

    public Hand(bool rHand)
    {
        isRightHand = rHand;
        hasBeenFound = false;
        bones = new List<Transform>();
    }
}