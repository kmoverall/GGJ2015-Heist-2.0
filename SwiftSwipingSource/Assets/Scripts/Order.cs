using UnityEngine;
using System.Collections;

public class Order {
    public Order(Commands c, Vector3 loc) {
        command = c; location = loc;
    }

    public enum Commands { MOVE, STEAL, WAIT };
    public Commands command;
    public Vector3 location;
}
