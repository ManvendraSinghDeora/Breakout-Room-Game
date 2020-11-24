using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    string DefaultName;
    private readonly string KeyName = "NickName";
    public InputField _PlayerName;
    public GameObject LobbyPanel;
    private void Start()
    {
        if (PlayerPrefs.HasKey(KeyName))
        {
            DefaultName = PlayerPrefs.GetString(KeyName, "Player" + Random.Range(1000, 10000).ToString());
        }
        else
        {
            DefaultName = "Player" + Random.Range(1000, 10000).ToString();
            PlayerPrefs.SetString(KeyName, DefaultName);
        }

        Photon.Pun.PhotonNetwork.NickName = _PlayerName.text = DefaultName;

    }


    public void SetName(string value)
    {
        PlayerPrefs.SetString(KeyName, value);
        Photon.Pun.PhotonNetwork.NickName = value;
    }


    public void PlayButton()
    {
        if (!string.IsNullOrEmpty(_PlayerName.text))
        {
            LobbyPanel.SetActive(true);
            this.gameObject.SetActive(false);
            Photon.Pun.PhotonNetwork.JoinLobby();
        }
    }
}
