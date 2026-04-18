using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterChangedEvent
{
    public string ChapterID;
    public ChapterChangedEvent(string chapterID)
    {
        ChapterID = chapterID;
    }
}