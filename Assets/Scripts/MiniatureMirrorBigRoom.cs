using UnityEngine;


public class MiniatureMirrorBigRoom : MonoBehaviour
{
    // The big room's objects' transforms.
    public Transform bigRoomObjects;

    // Whether rigidbody physics should be used for movement or direct transform movement.
    public bool usePhysicsMove = true;

    // Reference to the MiniatureMapper for position and rotation mapping.
    public MiniatureMapper mapper;

    // Rigidbody of the big room objects.
    Rigidbody realRigidbody;

    // The last tracked position in the miniature objects, to track changes.
    Vector3 lastMiniObjPosition;

    // The last tracked rotation in the miniature objects, to track changes.
    Quaternion lastMiniObjRotation;


    // This Awake function is called when the script instance is being loaded.
    // It looks for the MiniatureMapper in the scene if not assigned, 
    // and initializes the last known position and rotation for the miniature objects.
    void Awake()
    {
        // If the mapper is not assigned, it tries to find it in the scene, and gives a warning text.
        if (mapper == null)
        {
            mapper = FindFirstObjectByType<MiniatureMapper>();
            if (mapper == null)
            {
                Debug.LogError("MiniatureMapper not found in the scene. Please assign it in the inspector.");
            }
        }
        
        // Gets the Rigidbody component from the big room objects if available.
        if (bigRoomObjects != null)
        {
            realRigidbody = bigRoomObjects.GetComponent<Rigidbody>();
        }

        // Initializes the last known position and rotation for the miniature objects.
        lastMiniObjPosition = transform.position;
        lastMiniObjRotation = transform.rotation;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initial synchronization of the big room objects to match the miniature objects.
        SyncInstant();
    }

    // This FixedUpdate function is called at a fixed interval and is independent of frame rate.
    // It checks for changes in the miniature objects' position or rotation,
    // and applies those changes to the big room objects accordingly.
    void FixedUpdate()
    {
        // Checks if the miniature object's position or rotation has changed since the last frame.
        if (transform.position != lastMiniObjPosition || transform.rotation != lastMiniObjRotation)
        {
            // Applies the changes to the big room objects.
            ApplyMiniObjToBigRoomObj();
            lastMiniObjPosition = transform.position;
            lastMiniObjRotation = transform.rotation;
        }
    }

    // Applies the miniature object's position and rotation to the big room objects.
    void ApplyMiniObjToBigRoomObj()
    {
        // If the mapper or big room objects are not assigned, it exits the function.
        if (mapper == null || bigRoomObjects == null)
        {
            return;
        }

        // Maps the miniature object's position and rotation to the big room's corresponding values,
        // for both position (Vector3) and rotation (Quaternion).
        Vector3 targetPosition = mapper.MapMiniatureToBigRoomPosition(transform.position);
        Quaternion targetRotation = mapper.MapMiniatureToBigRoomRotation(transform.rotation);

        // Applies the calculated position and rotation to the big room objects,
        // using rigidbody physics movement if specified, 
        // otherwise directly setting the transform without physics.
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

    // This SyncInstant function instantly synchronizes the big room objects 
    // to match the miniature objects' current position and rotation.
    public void SyncInstant()
    {
        // If the mapper or big room objects are not assigned, it exits the function.
        if (mapper == null || bigRoomObjects == null)
        {
            return;
        }

        // Maps the miniature object's position and rotation to the big room's corresponding values,
        // for both position (Vector3) and rotation (Quaternion).
        Vector3 position = mapper.MapMiniatureToBigRoomPosition(transform.position);
        Quaternion rotation = mapper.MapMiniatureToBigRoomRotation(transform.rotation);

        // Directly sets the big room objects' position and rotation,
        // using rigidbody physics if specified, otherwise directly setting the transform without physics.
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
