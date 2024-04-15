using UnityEngine;

public class actionsDisplay : MonoBehaviour
{
    public string aName;
    public string description;
    public bool isDone = false;
    public ActionTemplate originalAction;
    public ClientTemplate client;

    private void Awake()
    {
        aName = originalAction.aName;
        description = originalAction.description;
        client = originalAction.client;
        isDone= originalAction.isDone;
        fillOutDisplay();
    }

    public virtual void fillOutDisplay() {
    
    }
    public virtual void displayUpdate()
    {
        return;
    }

    public void updateActions()
    {
        Event_manager.instance.updateDay();
    }

    public virtual bool isFinnished()
    {
        return isDone;
    }

    public virtual void end() {
    
    }
}
