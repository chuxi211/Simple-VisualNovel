using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Character2D",menuName ="VN/CharacterActor")]

public class CharacterAssetData: ScriptableObject
{
    //只有name还不够，因为显示出的name和实际name有时会不一样
    //比如名字为“TSUKUYOMI”，但有时要显示为“？？？”
    //所以要有一个编号，用于唯一标识角色，之后可以通过这个编号来获取角色数据
    //问题是，查找时也要通过编号吗？
    //不对，显示时的名字由Dialogue直接传入，但是查找的时候不能用显示的名字
    public string CharacterName; //可以取消掉了感觉
    public string CharacterID;   //功能重复了······重复了···吗？
    public Sprite IdleSprite;
    public Sprite UnknownSprite;
    public Sprite SmileSprite;
    public Sprite TearsSprite;
    public Position Position;
    public Sprite GetSprite(State state)
    {
        return state switch
        {
            State.Idle => IdleSprite,
            State.Unknown => UnknownSprite,
            State.Smile => SmileSprite,
            State.Tears => TearsSprite,
            _ => null
        };
    }
    public Position GetPosition(Position position)
    {
        return position switch
        {
            Position.Left => Position.Left,
            Position.Middle => Position.Middle,
            Position.Right => Position.Right,
            Position.Unknown => Position.Unknown,
            _ => throw new System.NotImplementedException()
        };
    }
}
