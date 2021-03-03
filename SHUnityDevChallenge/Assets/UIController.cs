using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField] private EscapeMenu escapeMenu;

    // Start is called before the first frame update
    void Start()
    {
        // turn off escape menu
        escapeMenu.Close();
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.ESC, onEscapeMenu);
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ESC, onEscapeMenu);
    }

    void onEscapeMenu()
    {
        escapeMenu.ToggleOpenClose();
    }
}
