using UnityEngine;
using System.Collections;

public class Positions : MonoBehaviour
{
    public Transform PlayerOne;
    public Transform PlayerTwo;
    public Transform VaultOne;
    public Transform VaultTwo;
    public Transform VaultThree;
    public Transform Deck;
    public Transform Jail;
    public Transform Evidence;
    public Transform Disrupt;

    private static Positions positions;
    public static Positions instance
    {
        get
        {
            if (!positions)
            {
                positions = FindObjectOfType(typeof(Positions)) as Positions;
            }
            if (!positions)
            {
                Debug.LogError("No active Positions managers loaded in scene");
            }
            return positions;
        }
    }
    public static Vector3 getPlayerOnePos()
    {
        return instance.PlayerOne.position;
    }
    public static Vector3 getPlayerTwoPos()
    {
        return instance.PlayerTwo.position;
    }
    public static Vector3 getVaultOnePos()
    {
        return instance.VaultOne.position;
    }
    public static Vector3 getVaultTwoPos()
    {
        return instance.VaultTwo.position;
    }
    public static Vector3 getVaultThreePos()
    {
        return instance.VaultThree.position;
    }
    public static Vector3 getDeckPos()
    {
        return instance.Deck.position;
    }
    public static Vector3 getJailPos()
    {
        return instance.Jail.position;
    }
    public static Vector3 getEvidencePos()
    {
        return instance.Evidence.position;
    }
    public static Vector3 getDisruptPos()
    {
        return instance.Disrupt.position;
    }
}
