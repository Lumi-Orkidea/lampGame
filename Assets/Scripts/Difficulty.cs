using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Difficulty //static class to set the difficulty of the game
{
    public static int lampCount { get; set; } //how many lamps will be spawned
    public static bool customLamp { get; set; } //are we using a lamp that's not the default
    public static Material lampMat { get; set; } //lamp material
}
