using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Undo"))
        {
            // Undo
            Messenger.Broadcast(GameEvent.UNDO);
        }
        else if (Input.GetButtonDown("Redo"))
        {
            // Redo
            Messenger.Broadcast(GameEvent.REDO);
        }

        else if (Input.GetButtonDown("Jump"))
        {
            // Redo
            Messenger.Broadcast(GameEvent.UPDATE_HISTORY);
        }
    }
}
