#region Common definitions
#if UNITY_ANDROID && !UNITY_EDITOR
#define USE_ANDROID_NATIVE
#endif
#endregion

using System;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

#if USE_ANDROID_NATIVE
    using com.zehfernando.UnityScreenBars.android;
#endif


public class ButtonActions : MonoBehaviour {

    public Text viewFlagsText;
    public Text windowFlagsText;
    public Text heightsText;

    private int _initialViewFlags;
    private int _initialWindowFlags;

    #if !USE_ANDROID_NATIVE
        private int _viewFlags = 0;
        private int _windowFlags = 0;
    #endif

    private uint[] backgroundColorExamples = { 0xff000000, 0xffff0000, 0x6600ff00 };

    public void Start() {
        _initialViewFlags = getViewFlags();
        _initialWindowFlags = getWindowFlags();
        ReadFlags();
    }

    public void Update() {
        ReadFlags();
    }

    public void ReadFlags() {
        if (viewFlagsText) {
            int value = getViewFlags();
            int value2 = getStackedViewFlags();
            viewFlagsText.text = $"Current View..systemUiVisibility value:\n{value} / {value2}\n({Convert.ToString(value, 2)} / {Convert.ToString(value2, 2)})";
        }

        if (windowFlagsText) {
            int value = getWindowFlags();
            windowFlagsText.text = $"Current Window..flags value:\n{value}\n({Convert.ToString(value, 2)})";
        }

        if (heightsText) {
            heightsText.text = $"Current statusBarHeight: {ScreenBars.statusBarHeight}\nCurrent navigationBarHeight: {ScreenBars.navigationBarHeight} (available: {(ScreenBars.navigationBarAvailable ? "true" : "false")})";
        }

        updateButtonText("ButtonView1", getViewFlag(1));
        updateButtonText("ButtonView2", getViewFlag(2));
        updateButtonText("ButtonView4", getViewFlag(4));
        updateButtonText("ButtonView16", getViewFlag(16));
        updateButtonText("ButtonView256", getViewFlag(256));
        updateButtonText("ButtonView512", getViewFlag(512));
        updateButtonText("ButtonView1024", getViewFlag(1024));
        updateButtonText("ButtonView2048", getViewFlag(2048));
        updateButtonText("ButtonView4096", getViewFlag(4096));
        updateButtonText("ButtonView8192", getViewFlag(8192));

        updateButtonText("ButtonWindow256", getWindowFlag(256));
        updateButtonText("ButtonWindow512", getWindowFlag(512));
        updateButtonText("ButtonWindow1024", getWindowFlag(1024));
        updateButtonText("ButtonWindow2048", getWindowFlag(2048));
        updateButtonText("ButtonWindow65536", getWindowFlag(65536));
        updateButtonText("ButtonWindow67108864", getWindowFlag(67108864));
        updateButtonText("ButtonWindow134217728", getWindowFlag(0x08000000));
        updateButtonText("ButtonWindowBG", getWindowFlag(-2147483648));

        updateButtonText("ButtonLowProfile", ScreenBars.lowProfile);
        updateButtonText("ButtonStatusBarVisible", ScreenBars.statusBarVisible);
        updateButtonText("ButtonStatusBarTranslucent", ScreenBars.statusBarTranslucent);
        updateButtonText("ButtonStatusBarForegroundDark", ScreenBars.statusBarForegroundDark);
        updateButtonText("ButtonStatusBarBackgroundColor", ScreenBars.statusBarBackgroundColor);
        updateButtonText("ButtonNavigationBarVisible", ScreenBars.navigationBarVisible);
        updateButtonText("ButtonNavigationBarTranslucent", ScreenBars.navigationBarTranslucent);
        updateButtonText("ButtonNavigationBarOverlay", ScreenBars.navigationBarOverlay);
        updateButtonText("ButtonNavigationBarForegroundDark", ScreenBars.navigationBarForegroundDark);
        updateButtonText("ButtonNavigationBarBackgroundColor", ScreenBars.navigationBarBackgroundColor);

        updateButtonText("ButtonUnityFullScreen", Screen.fullScreen);
    }

    public void ToggleScreenFullScreen() {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ToggleViewFlag(int bitMask) {
        var value = getViewFlag(bitMask);
        setViewFlag(bitMask, !value);
    }

    public void ToggleWindowFlag(int bitMask) {
        var value = getWindowFlag(bitMask);
        setWindowFlag(bitMask, !value);
    }

    public void ToggleLowProfile() {
        ScreenBars.lowProfile = !ScreenBars.lowProfile;
    }

    public void ToggleStatusBarVisible() {
        ScreenBars.statusBarVisible = !ScreenBars.statusBarVisible;
    }

    public void ToggleStatusBarTranslucent() {
        ScreenBars.statusBarTranslucent = !ScreenBars.statusBarTranslucent;
    }

    public void ToggleStatusBarForegroundDark() {
        ScreenBars.statusBarForegroundDark = !ScreenBars.statusBarForegroundDark;
    }

    public void ToggleStatusBarBackgroundColor() {
        var idx = Array.IndexOf(backgroundColorExamples, ScreenBars.statusBarBackgroundColor);
        ScreenBars.statusBarBackgroundColor = backgroundColorExamples[(idx + 1) % backgroundColorExamples.Length];
    }

    public void ToggleNavigationBarVisible() {
        ScreenBars.navigationBarVisible = !ScreenBars.navigationBarVisible;
    }

    public void ToggleNavigationBarTranslucent() {
        ScreenBars.navigationBarTranslucent = !ScreenBars.navigationBarTranslucent;
    }

    public void ToggleNavigationBarOverlay() {
        ScreenBars.navigationBarOverlay = !ScreenBars.navigationBarOverlay;
    }

    public void ToggleNavigationBarForegroundDark() {
        ScreenBars.navigationBarForegroundDark = !ScreenBars.navigationBarForegroundDark;
    }

    public void ToggleNavigationBarBackgroundColor() {
        var idx = Array.IndexOf(backgroundColorExamples, ScreenBars.navigationBarBackgroundColor);
        ScreenBars.navigationBarBackgroundColor = backgroundColorExamples[(idx + 1) % backgroundColorExamples.Length];
    }

    public void ResetFlags() {
        setViewFlags(_initialViewFlags);
        setWindowFlags(_initialWindowFlags);
    }

    private void updateButtonText(string buttonName, bool value) {
        updateButtonText(buttonName, value ? "1" : "0");
    }

    private void updateButtonText(string buttonName, uint value) {
        updateButtonText(buttonName, $"0x{Convert.ToString(value, 16)}");
    }

    private void updateButtonText(string buttonName, string value) {
        var textLabel = GameObject.Find($"{buttonName}/Text");
        if (textLabel != null) {
            var textComponent = textLabel.GetComponent<Text>();
            if (textComponent != null) {
                string oldText = textComponent.text;
                string prefix = " (";
                string suffix = ")";
                string valueLabel = prefix + value + suffix;
                if (oldText.Contains(prefix)) {
                    textComponent.text = oldText.Substring(0, oldText.IndexOf(prefix)) + valueLabel;
                } else {
                    textComponent.text = oldText + valueLabel;
                }
            }
        }
    }

    private bool getViewFlag(int bitMask) {
        #if USE_ANDROID_NATIVE
            return Utils.getViewFlag(bitMask);
        #else
            return (getViewFlags() & bitMask) != 0;
        #endif
    }

    private void setViewFlag(int bitMask, bool value) {
        #if USE_ANDROID_NATIVE
            Utils.setViewFlag(bitMask, value);
        #else
            var flags = getViewFlags();
            var newBit = value ? bitMask : 0;
            setViewFlags((flags & ~bitMask) | newBit);
        #endif
    }

    private bool getWindowFlag(int bitMask) {
        #if USE_ANDROID_NATIVE
            return Utils.getWindowFlag(bitMask);
        #else
            return (getWindowFlags() & bitMask) != 0;
        #endif
    }

    private void setWindowFlag(int bitMask, bool value) {
         #if USE_ANDROID_NATIVE
            Utils.setViewFlag(bitMask, value);
        #else
            var flags = getWindowFlags();
            var newBit = value ? bitMask : 0;
            setWindowFlags((flags & ~bitMask) | newBit);
        #endif
    }

    private int getViewFlags() {
        #if USE_ANDROID_NATIVE
            return Utils.getViewFlags();
        #else
            return _viewFlags;
        #endif
    }

     private int getStackedViewFlags() {
        #if USE_ANDROID_NATIVE
            return Utils.getStackedViewFlags();
        #else
            return getViewFlags();
        #endif
    }

    private void setViewFlags(int value) {
        #if USE_ANDROID_NATIVE
            Utils.setViewFlags(value);
        #else
            _viewFlags = value;
        #endif
    }

    private int getWindowFlags() {
        #if USE_ANDROID_NATIVE
            return Utils.getWindowFlags();
        #else
            return _windowFlags;
        #endif
    }

    private void setWindowFlags(int value) {
        #if USE_ANDROID_NATIVE
            Utils.setWindowFlags(value);
        #else
            _windowFlags = value;
        #endif
    }
}
