using System;
[Serializable]
public class Action
{
    public string aName;
    public string description;
    public bool isDone = false;
    public ActionTemplate originalAction;
    public EventInterface eInterface;

    public virtual void startEvent()
    {
        aName = originalAction.aName;
        description = originalAction.description;
        isDone = originalAction.isDone;



        eInterface = Event_manager.instance.instiate_interface(originalAction.eInterfacePrefab);
        eInterface.yeEvent = this;
    }
    public virtual void endEvent()
    {
        eInterface.endInterface();
    }
    public virtual bool isEventFinnished()
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
