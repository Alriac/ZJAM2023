using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DisableOnPlatform : MonoBehaviour
{
    public EnumSupportedPlatforms DisableOnPlatforms;
    public bool DeleteInsteadDisabling;
    [HideInInspector]
    public bool IsDisabled { get; private set; }
    private void Awake()
    {
        bool disable = false;

#if UNITY_WEBGL
        if ((DisableOnPlatforms & EnumSupportedPlatforms.Web) > 0) disable = true;
#endif

#if UNITY_STANDALONE
        if ((DisableOnPlatforms & EnumSupportedPlatforms.Desktop) > 0) disable = true;
#endif

#if UNITY_ANDROID || UNITY_IOS
        if ((DisableOnPlatforms & EnumSupportedPlatforms.Mobile) > 0) disable = true;
#endif

        if (disable)
        {
            IsDisabled = true;
            if (DeleteInsteadDisabling) Destroy(gameObject);
            else gameObject.SetActive(false);
        }
    }
}
