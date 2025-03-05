using UnityEngine;
namespace Game_Managers
{
  public class playerCounter : MonoBehaviour
  {
    public int playersIngame;
  
    void OnPlayerJoined()
    {
      playersIngame++;
    }
  }
}
