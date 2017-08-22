using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class CollisionInfo
{
    public List<LayerEnum> LayerList;
    public BehaviourController BehaviourController;
}


public class CollisionController : MMGameSceneBehaviour
{
    public bool IsDebugEnabled;

    public bool IsControllerActive { get; set; }

    public List<CollisionInfo> OnEnter;
    public List<CollisionInfo> OnStay;
    public List<CollisionInfo> OnExit;

    public GameObject CollidedGameOject { get; private set; }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();

        IsControllerActive = true;
    }

    // Sent when another object enters a trigger collider attached to this object (2D physics only).
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsControllerActive)
            return;

        if (IsDebugEnabled)
            Debug.Log(gameObject.name + " collided with object: " + other.gameObject);


        CollidedGameOject = other.gameObject;

        CollisionInfo ci = GetCollisionInfo(OnEnter, (LayerEnum)other.gameObject.layer);

        if (ci == null)
            return;

        ExecuteBehaviourControllers(ci, other.gameObject);
    }

    // Sent each frame where another object is within a trigger collider attached to this object (2D physics only).
    protected void OnTriggerStay2D(Collider2D other)
    {
        if (!IsControllerActive)
            return;

        CollidedGameOject = other.gameObject;

        CollisionInfo ci = GetCollisionInfo(OnStay, (LayerEnum)other.gameObject.layer);

        if (ci == null)
            return;

        ExecuteBehaviourControllers(ci, other.gameObject);
    }

    // Sent when another object leaves a trigger collider attached to this object (2D physics only).
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (!IsControllerActive)
            return;

        CollidedGameOject = other.gameObject;

        CollisionInfo ci = GetCollisionInfo(OnExit, (LayerEnum)other.gameObject.layer);

        if (ci == null)
            return;

        ExecuteBehaviourControllers(ci, other.gameObject);
    }

    CollisionInfo GetCollisionInfo(List<CollisionInfo> targetList, LayerEnum targetLayer)
    {
        try
        {
            return targetList.First(val => val.LayerList.Contains(targetLayer));
        }
        catch
        {
            return null;
        }
    }

    void ExecuteBehaviourControllers(CollisionInfo ci, GameObject go)
    {
        if (ci.BehaviourController != null)
            ci.BehaviourController.Execute();
    }

}
