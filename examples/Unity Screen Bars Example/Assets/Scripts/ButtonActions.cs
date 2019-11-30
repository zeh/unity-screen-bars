using System;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour {

    private uint[] backgroundColorExamples = { 0xff000000, 0xffff0000, 0x6600ff00 };

    public void Start() {
        ReadFlags();
    }

    public void Update() {
        ReadFlags();
    }

    public void ReadFlags() {
        var canvasScale = getCanvasScale();

        // Update status bar reference button, moving it up and down to match the status bar visibility
        {
            updateButtonText("ButtonStatusBarHeight", $"{ScreenBars.statusBarHeight}{(ScreenBars.statusBarHeight > 0 && !ScreenBars.statusBarVisible ? ", but invisible" : "")}");
            var button = getRectTransform("ButtonStatusBarHeight");
            if (button != null) {
                var h = button.rect.height;
                var yOffset = ScreenBars.statusBarVisible ? ScreenBars.statusBarHeight / canvasScale : 0;
                button.offsetMin = new Vector2(button.offsetMin.x, -yOffset - h);
                button.offsetMax = new Vector2(button.offsetMax.x, -yOffset);
            }
        }

        // Update navigation bar reference button, moving it up and down to match the navigation bar visibility
        if (ScreenBars.navigationBarAvailable || true) {
            updateButtonText("ButtonNavigationBarHeight", $"{ScreenBars.navigationBarHeight}{(ScreenBars.navigationBarHeight > 0 && !ScreenBars.navigationBarVisible ? ", but invisible" : "")}");
            var button = getRectTransform("ButtonNavigationBarHeight");
            if (button != null) {
                var h = button.rect.height;
                var yOffset = ScreenBars.navigationBarVisible ? ScreenBars.navigationBarHeight / canvasScale: 0;
                button.offsetMin = new Vector2(button.offsetMin.x, yOffset);
                button.offsetMax = new Vector2(button.offsetMax.x, yOffset + h);
            }
        } else {
            updateButtonText("ButtonNavigationBarHeight", $"{ScreenBars.navigationBarHeight}, but unavailable");
        }

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

    private RectTransform getRectTransform(string name) {
        var gameObject = GameObject.Find(name);
        return gameObject != null ? gameObject.GetComponent<RectTransform>() : null;
    }

    private float getCanvasScale() {
        var gameObject = GameObject.Find("Canvas");
        if (gameObject != null) {
            var scaler = gameObject.GetComponent<CanvasScaler>();
            if (scaler) {
                return scaler.scaleFactor;
            }
        }
        return 1;
    }
}
