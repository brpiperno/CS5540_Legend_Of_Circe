using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisualController
{
    void setAnimationTrigger(string trigger);
    // Highlights the arrow that was pressed. For direction - 0 means up, 1 means left, 2 means right, and 3 means down
    public void HighlightArrow(int direction);
    public void RemoveHighlight();
}
