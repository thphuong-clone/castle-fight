using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string name;
    public UnityEngine.UI.Image portrait;

    public Character(string name, UnityEngine.UI.Image portrait)
    {
        this.name = name;
        this.portrait = portrait;
    }

    public override bool Equals(object obj)
    {
        if (obj.GetType() != this.GetType())
            return false;
        return this.name.Equals((obj as Character).name);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return this.name;
    }
}
