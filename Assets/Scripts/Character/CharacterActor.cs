using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActor
{
    public CharacterAssetData Asset;
    public CharacterView view;
    public State state;
    public Position position;
    public CharacterActor(CharacterAssetData asset,CharacterView view)
    {
        this.Asset = asset;
        this.view = view;
        this.state = State.Idle;
    }
    public void SetState(State newstate)
    {
        this.state = newstate;
        Debug.Log($"actor:{state}");
        view.SetSprite(Asset.GetSprite(newstate));//从asset里拿到对应状态的图片并用view显示
    }
    public void SetPosition(Position newposition)
    {
        this.position = newposition;
        view.SetPosition(Asset.GetPosition(newposition));
        Debug.Log(position);
    }
}
