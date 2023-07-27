using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSelector : MonoBehaviour
{
    [SerializeField] private bool _active = false;

    public void ChangeLocale(int localID)
    {
        if (_active == true)
            return;

        StartCoroutine(SetLocale(localID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        _active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        _active = false;
    }
}
