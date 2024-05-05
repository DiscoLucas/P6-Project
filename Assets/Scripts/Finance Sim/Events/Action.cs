using System;

[Serializable]
public class Action
{
    public string aName;
    public string description;
    public bool isDone = false;
    public ActionTemplate originalAction;
    public actionsDisplay aDisplay;
    public ClientTemplate client;
    public virtual void startAction()
    {
        aName = originalAction.aName;
        description = originalAction.description;
        isDone = originalAction.isDone;
        aDisplay = Event_manager.instance.instiate_interface(originalAction.eInterfacePrefab);
        client = originalAction.client;
        originalAction.start();
    }
    public virtual void end()
    {
        aDisplay.end();
    }
    public virtual bool isFinnished()
    {
        return isDone;
    }
    public virtual void update()
    {

    }

    public virtual void VisualUpdate()
    {

    }


}
