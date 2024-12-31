using UnityEngine;

public class EventService
{
    public EventController OnPlayerDeath;
    public EventController OnNewGameButtonClicked;
    public EventController<Collider> OnPlayerContactWithObject;

    public EventService()
    {
        OnNewGameButtonClicked = new EventController();
        OnPlayerDeath = new EventController();
        OnPlayerContactWithObject = new EventController<Collider>();
    }
}