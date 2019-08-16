using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//https://anchan828.github.io/editor-manual/web/property_drawer.html

public class PopupAttribute : PropertyAttribute
{
    public object[] list;

    public PopupAttribute(params object[] list)
    {
        this.list = list;
    }
}