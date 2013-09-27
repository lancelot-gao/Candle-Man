using UnityEngine;
using System.Collections;
using System;

public class KeyboardIndexInput : GenericInputSender
{
    #region Fields

    public KeyCode[] indexKeys;
    #endregion

    #region Properties

    #endregion

    #region Functions
    void Update()
    {
        if (indexKeys.Length > 0)
        {
            for (uint i = 0; i < indexKeys.Length; i++)
            {
                if (Input.GetKeyDown(indexKeys[i])) reciever.OnIndexInput(i);
            }
        }
    }
    #endregion
}
