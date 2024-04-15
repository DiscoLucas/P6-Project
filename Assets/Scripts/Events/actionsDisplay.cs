using UnityEngine;

public class actionsDisplay : MonoBehaviour
{
    public string aName;
    public string description;
    public bool isDone = false;
    public ActionTemplate originalAction;
    public int clientIndex = -1;
    public ClientData actionClient;

    private void Awake()
    {
        aName = originalAction.aName;
        description = originalAction.description;
        isDone= originalAction.isDone;
    }

    public void setClient(int index) {
        clientIndex = index;
        Debug.Log(index);
        actionClient = Event_manager.instance.clientManager.getClient(clientIndex);
    }
    public virtual void fillOutDisplay() 
    {
        
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

    public virtual void end() 
    {
        Destroy(gameObject);
    }
}
