using UnityEngine;

[CreateAssetMenu(fileName = "InfoBlock", menuName = "Data/PlayerInfo", order = 0)]
public class PlayerInfoBlock : ScriptableObject {
    public int health;
    public float stamina;
    public int clankBlock;
    public int hazardBlock;

}