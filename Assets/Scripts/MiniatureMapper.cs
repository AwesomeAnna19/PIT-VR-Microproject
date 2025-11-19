using UnityEngine;

public class MiniatureMapper : MonoBehaviour
{
    // The miniature's root transform.
    public Transform miniatureRoot;

    // The big room's root transform.
    public Transform bigRoomRoot;

    // The scale factor between the miniature and the real big room.
    public float miniatureToRealScale = 20f;


    // Here, the objects' positions that are being manipulated in the miniature room,
    // will be converted to the world position of the big room's objects.
    // To make the manipulated objects' positions from the miniature room correspond to the big room's.
    public Vector3 MapMiniatureToBigRoomPosition(Vector3 worldMiniaturePosition)
    {
        // Converts the miniature world position to local space.
        Vector3 localMiniaturePosition = miniatureRoot.InverseTransformPoint(worldMiniaturePosition);

        // Converts the world position to miniaure's local position.
        Vector3 realWorldPosition = bigRoomRoot.TransformPoint(localMiniaturePosition);

        // Returns the calculated real world position.
        return realWorldPosition;
    }

    // Here, the objects' rotations that are being manipulated in the miniature room,
    // will be converted to the world rotations of the big room's objects.
    // To make the manipulated objects' rotations from the miniature room correspond to the big room's.
    public Quaternion MapMiniatureToBigRoomRotation(Quaternion worldMiniatureRotation)
    {
        // Converts the miniature world rotation to local space.
        Quaternion localMiniatureRotation = Quaternion.Inverse(miniatureRoot.rotation) * worldMiniatureRotation;

        // Converts the local miniature rotation to world rotation of the big room.
        Quaternion realWorldRotation = bigRoomRoot.rotation * localMiniatureRotation;

        // Returns the calculated real world rotation.
        return realWorldRotation;
    }

    // Converts the local position of the miniature object to the world position of the big room object.
    public Vector3 MapLocalMiniatureToBigRoomPosition(Vector3 localMiniaturePosition)
    {
        // Upscales the miniature's local postion by the scale factor.
        Vector3 scaledPosition = localMiniaturePosition * miniatureToRealScale;

        // Returns the calculated real world position.
        return bigRoomRoot.TransformPoint(scaledPosition);
    }
}
