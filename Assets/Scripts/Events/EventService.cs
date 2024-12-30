using UnityEngine;

public class EventService
{
    public EventController OnPlayerDeath;
    public EventController OnNewGameButtonClicked;
    public EventController<Collider> OnTakingDamage;

    public EventService()
    {
        OnNewGameButtonClicked = new EventController();
        OnPlayerDeath = new EventController();
        OnTakingDamage = new EventController<Collider>();
    }
}