using UnityEngine;

public class actionsDisplay : MonoBehaviour
{
    public Action yeEvent;

    public void Start()
    {
        fillOutDisplay();
    }
    public virtual void fillOutDisplay()
    {

    }

    public void end()
    {
        Destroy(gameObject);
    }
    public virtual void displayUpdate()
    {
        return;
    }

    public void updateActions()
    {
        Event_manager.instance.updateDay();
    }
}
