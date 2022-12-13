using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaving : MonoBehaviour
{
    [SerializeField]
    private float AutoSaveDuration;
    [SerializeField]
    private GameObject text;

    private PlayerMovement player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        InvokeRepeating("AutoSave", 0.0f, AutoSaveDuration);
    }

    public void AutoSave()
    {
        if (player != null)
        {
            StartCoroutine(ShowText());
        }
    }

    private IEnumerator ShowText()
    {
        text.SetActive(true);
        PlayerPrefs.SetInt("Health", player.currentHP);
        PlayerPrefs.SetInt("Mana", player.currentMP);
        PlayerPrefs.SetFloat("X", player.transform.position.x);
        PlayerPrefs.SetFloat("Y", player.transform.position.y);
        PlayerPrefs.SetFloat("Z", player.transform.position.z);
        yield return new WaitForSeconds(1.0f);
        text.SetActive(false);
    }
}
