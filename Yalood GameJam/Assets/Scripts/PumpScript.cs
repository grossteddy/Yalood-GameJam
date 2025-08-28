using UnityEngine;

public class PumpScript : MonoBehaviour
{
    [SerializeField] BalloonScript balloon;

    private GameControls gameControl;

    private void Awake()
    {
        gameControl = new GameControls();
        gameControl.Gameplay.Pump.performed += ctx => balloon.Inflate(5f);
    }

    private void OnEnable()
    {
        gameControl.Gameplay.Enable();
    }

    private void OnDisable()
    {
        gameControl.Gameplay.Disable();
    }
}
