using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWorld : MonoBehaviour
{
    public WorldType.Type chooseType;
    public PickWorld pick;
    public float moveSpeed = 3f;
    public Transform pos;
    [HideInInspector] public bool completed;
    private SpatialInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
    }
    private void Start()
    {
        interactable.onInteractEvent.unityEvent.AddListener(TryPlaceObject);
    }
    public void TryPlaceObject()
    {
        if (pick == null)
            return;

        if (pick.currentObject != null && pick.type == chooseType)
        {
            StartCoroutine(MoveToPosition(pick.currentObject, pos));
            pick.Release();
            completed = true;
            interactable.enabled = false;
        }
    }

    private IEnumerator MoveToPosition(GameObject obj, Transform target)
    {
        while (obj != null && Vector3.Distance(obj.transform.position, target.position) > 0.05f)
        {
            obj.transform.position = Vector3.Lerp(
                obj.transform.position,
                target.position,
                Time.deltaTime * moveSpeed
            );

            yield return null;
        }

        if (obj != null)
        {
            obj.transform.position = target.position;
            obj.transform.rotation = target.rotation;
        }
    }
}
