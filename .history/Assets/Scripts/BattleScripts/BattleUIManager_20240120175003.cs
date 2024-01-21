using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUIManager : MonoBehaviour
{
    public void loadMapScene()
    {
        AudioSource musicSource = MenusMusicController.Instance.musicSource;

        MenusMusicController.Instance.musicSource.volume = 0;
        MenusMusicController.Instance.musicSource.UnPause();

        StartCoroutine(FadeInVolume(musicSource, 2.0f, SettingsManager.Instance.MusicVolume));

        SceneManager.LoadScene("MapScene");
    }

    IEnumerator FadeInVolume(AudioSource audioSource, float duration, float targetVolume)
    {
        yield return new WaitForSeconds(0.5f);

        audioSource.UnPause();

        float currentTime = 0;
        float startVolume = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    public void winBattle(){
        GameManager.Instance.AllConqueredCityNames.Add(GameManager.Instance.CurrentEnemyName);
        GameManager.Instance.balance += 300;

        SceneManager.LoadScene("MapScene");
    }
}
