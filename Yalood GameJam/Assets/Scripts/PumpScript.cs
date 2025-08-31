using UnityEngine;

public class PumpScript : MonoBehaviour
{
    [SerializeField] BalloonScript balloon;
    [SerializeField] GameManagerScript gameManager;
    [SerializeField] float pumpAmount = 5f;

    private GameControls gameControl;
    private Animator pumpAnimator;

    private void Awake()
    {
        pumpAnimator = GetComponent<Animator>();
        gameControl = new GameControls();
        gameControl.Gameplay.Pump.performed += ctx => OnPump();
        gameControl.Gameplay.BankBalloon.performed += ctx => BankBalloon();
        gameControl.Gameplay.PauseButton.performed += ctx => PauseGame();
    }

    private void OnEnable()
    {
        gameControl.Gameplay.Enable();
    }

    private void OnDisable()
    {
        gameControl.Gameplay.Disable();
    }

    private void OnPump()
    {
        balloon.Inflate(pumpAmount);

        if(pumpAnimator != null)
        {
            pumpAnimator.SetTrigger("Pump");
        }
    }

    private void BankBalloon()
    {
        gameManager.BankPoints();
    }

    private void PauseGame()
    {

    }
}
