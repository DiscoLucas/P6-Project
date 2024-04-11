using System;
using System.Collections.Generic;
[Serializable]
public class Turn
{
    public List<Action> actions;
    public int actionsIndex = 0;
    public void startDay()
    {
        actionsIndex = 0;

        actions[actionsIndex].startAction();
    }

    public void updateActions()
    {
        if (actions[actionsIndex].isFinnished())
        {
            actions[actionsIndex].end();
            actionsIndex++;
            if (actionsIndex >= actions.Count)
                Event_manager.instance.changeDay();
            else
            {
                actions[actionsIndex].startAction();

            }

        }
        actions[actionsIndex].update();
        actions[actionsIndex].VisualUpdate();
    }
}
