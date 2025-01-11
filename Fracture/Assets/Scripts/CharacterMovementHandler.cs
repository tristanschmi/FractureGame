using UnityEngine;

public class CharacterMovementHandler : MonoBehaviour
{
    public CharacterController characterController;
    public CharacterMoveAndRotate3d moveAndRotateAsset;

    void Update()
    {
        if (moveAndRotateAsset != null && characterController != null)
        {
            moveAndRotateAsset.Move(characterController);
        }
    }
}
