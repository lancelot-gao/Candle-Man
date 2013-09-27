using UnityEngine;
using System.Collections;

public class KeyboardDirectionInput : GenericInputSender
{
    #region Fields
    public float strength = 1;
    public Vector3 horizontalAxis = Vector3.right;
    public Vector3 verticalAxis = Vector3.forward;
    #endregion

    #region Properties

    #endregion

    #region Functions

    void Update()
    {
        if (reciever == null) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 inputValue = horizontal * strength * horizontalAxis + vertical * strength * verticalAxis;
            reciever.OnDirectionInput(inputValue.x, inputValue.y, inputValue.z);
        }
    }
    
    #endregion
}
