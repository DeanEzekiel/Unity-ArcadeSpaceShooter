using UnityEngine;

public class ControllerList : MonoBehaviour
{
    public EnemyController Enemy;
    public ShopController Shop;
    public PlayerController Player;
    public TimerController Timer;
    public RandomNoteController RandomNote;

    public AudioController Audio => AudioController.Instance;
}
