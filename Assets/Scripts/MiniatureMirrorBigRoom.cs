using UnityEngine;


public class MiniatureMirrorBigRoom : MonoBehaviour
{
    public Transform bigRoomObjects;
    public bool usePhysicsMove = true;
    public MiniatureMapper mapper;

    Rigidbody realRigidbody;
    Vector3 lastMiniObjPosition;
    Quaternion lastMiniObjRotation;

    void Awake()
    {
        if (mapper == null)
        {
            mapper = FindFirstObjectByType<MiniatureMapper>();
            if (mapper == null)
            {
                Debug.LogError("MiniatureMapper not found in the scene. Please assign it in the inspector.");
            }
        }
        if (bigRoomObjects != null)
        {
            realRigidbody = bigRoomObjects.GetComponent<Rigidbody>();
        }

        lastMiniObjPosition = transform.position;
        lastMiniObjRotation = transform.rotation;
    }

    void Start()
    {
        SyncInstant();
    }

    void FixedUpdate()
    {
        if (transform.position != lastMiniObjPosition || transform.rotation != lastMiniObjRotation)
        {
            ApplyMiniObjToBigRoomObj();
            lastMiniObjPosition = transform.position;
            lastMiniObjRotation = transform.rotation;
        }
    }

    void ApplyMiniObjToBigRoomObj()
    {
        if (mapper == null || bigRoomObjects == null)
        {
            return;
        }

        Vector3 targetPosition = mapper.MapMiniatureToBigRoomPosition(transform.position);
        Quaternion targetRotation = mapper.MapMiniatureToBigRoomRotation(transform.rotation);

        if (usePhysicsMove && realRigidbody != null)
        {
            realRigidbody.MovePosition(targetPosition);
            realRigidbody.MoveRotation(targetRotation);
        }
        else
        {
            bigRoomObjects.position = targetPosition;
            bigRoomObjects.rotation = targetRotation;
        }
    }

    public void SyncInstant()
    {
        if (mapper == null || bigRoomObjects == null)
        {
            return;
        }

        Vector3 position = mapper.MapMiniatureToBigRoomPosition(transform.position);
        Quaternion rotation = mapper.MapMiniatureToBigRoomRotation(transform.rotation);

        if (realRigidbody != null && usePhysicsMove)
        {
            realRigidbody.position = position;
            realRigidbody.rotation = rotation;
        }
        else
        {
            bigRoomObjects.position = position;
            bigRoomObjects.rotation = rotation;
        }
    }
}
