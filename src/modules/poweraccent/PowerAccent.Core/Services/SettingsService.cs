// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace PowerAccent.Core.Services;

using Microsoft.PowerToys.Settings.UI.Library;
using Microsoft.PowerToys.Settings.UI.Library.Enumerations;
using Microsoft.PowerToys.Settings.UI.Library.Utilities;
using System.IO.Abstractions;
using System.Text.Json;

public class SettingsService
{
    private const string PowerAccentModuleName = "QuickAccent";
    private readonly ISettingsUtils _settingsUtils;
    private readonly IFileSystemWatcher _watcher;
    private readonly object _loadingSettingsLock = new object();

    public SettingsService()
    {
        _settingsUtils = new SettingsUtils();
        ReadSettings();
        _watcher = Helper.GetFileWatcher(PowerAccentModuleName, "settings.json", () => { ReadSettings(); });
    }

    private void ReadSettings()
    {
        // TODO this IO call should by Async, update GetFileWatcher helper to support async
        lock (_loadingSettingsLock)
        {
            {
                try
                {
                    if (!_settingsUtils.SettingsExists(PowerAccentModuleName))
                    {
                        Logger.LogInfo("QuickAccent settings.json was missing, creating a new one");
                        var defaultSettings = new PowerAccentSettings();
                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true,
                        };

                        _settingsUtils.SaveSettings(JsonSerializer.Serialize(this, options), PowerAccentModuleName);
                    }

                    var settings = _settingsUtils.GetSettingsOrDefault<PowerAccentSettings>(PowerAccentModuleName);
                    if (settings != null)
                    {
                        ActivationKey = settings.Properties.ActivationKey;
                        InputTime = settings.Properties.InputTime.Value;
                        switch (settings.Properties.ToolbarPosition.Value)
                        {
                            case "Top center":
                                Position = Position.Top;
                                break;
                            case "Bottom center":
                                Position = Position.Bottom;
                                break;
                            case "Left":
                                Position = Position.Left;
                                break;
                            case "Right":
                                Position = Position.Right;
                                break;
                            case "Top right corner":
                                Position = Position.TopRight;
                                break;
                            case "Top left corner":
                                Position = Position.TopLeft;
                                break;
                            case "Bottom right corner":
                                Position = Position.BottomRight;
                                break;
                            case "Bottom left corner":
                                Position = Position.BottomLeft;
                                break;
                            case "Center":
                                Position = Position.Center;
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Failed to read changed settings", ex);
                }
            }
        }
    }

    private PowerAccentActivationKey _activationKey = PowerAccentActivationKey.Both;

    public PowerAccentActivationKey ActivationKey
    {
        get
        {
            return _activationKey;
        }

        set
        {
            _activationKey = value;
        }
    }

    private Position _position = Position.Top;

    public Position Position
    {
        get
        {
            return _position;
        }

        set
        {
            _position = value;
        }
    }

    private int _inputTime = 200;

    public int InputTime
    {
        get
        {
            return _inputTime;
        }

        set
        {
            _inputTime = value;
        }
    }

    public AccentPair[] GetLetterKey(LetterKey letter)
    {
        return GetDefaultLetterKey(letter);
    }

    public static AccentPair[] GetDefaultLetterKey(LetterKey letter)
    {
        switch (letter)
        {
            case LetterKey.A:
                return new AccentPair[]
                { 
                    new('à', 'À'),
                    new('â', 'Â'),
                    new('á', 'Á'),
                    new('ä', 'Ä'),
                    new('ã', 'Ã'),
                    new('å', 'Å'),
                    new('ą', 'Ą'),
                    new('ą́', 'Ą́'),
                    new('ą̃', 'Ą̃'),
                    new('ă', 'Ă'),
                    new('ắ', 'Ắ'),
                    new('ằ', 'Ằ'),
                    new('ẵ', 'Ẵ'),
                    new('ẳ', 'Ẳ'),
                    new('ấ', 'Ấ'),
                    new('ầ', 'Ầ'),
                    new('ẫ', 'Ẫ'),
                    new('ẩ', 'Ẩ'),
                    new('ǎ', 'Ǎ'),
                    new('ǻ', 'Ǻ'),
                    new('ǟ', 'Ǟ'),
                    new('ȧ', 'Ȧ'),
                    new('ǡ', 'Ǡ'),
                    new('ā', 'Ā'),
                    new('ā̀', 'Ā̀'),
                    new('ả', 'Ả'),
                    new('ȁ', 'Ȁ'),
                    new('a̋', 'A̋'),
                    new('ȃ', 'Ȃ'),
                    new('ạ', 'Ạ'),
                    new('ặ', 'Ặ'),
                    new('ậ', 'Ậ'),
                    new('ḁ', 'Ḁ'),
                    new('ⱥ', 'Ⱥ'),
                    new('æ', 'Æ'),
                    new('ǣ', 'Ǣ'),
                    new('ǽ', 'Ǽ'),
                    new('æ̀', 'Æ̀'),
                };
            case LetterKey.B:
                return new AccentPair[]
                {
                    new('ḇ', 'Ḇ'),
                };
            case LetterKey.C:
                return new AccentPair[]
                { 
                    new('ć', 'Ć'),
                    new('ĉ', 'Ĉ'),
                    new('č', 'Č'),
                    new('ċ', 'Ċ'),
                    new('ç', 'Ç'),
                    new('ḉ', 'Ḉ'),
                };
            case LetterKey.D:
                return new AccentPair[]
                {
                    new('ð', 'Ð'),
                    new('ḏ', 'Ḏ'),
                };
            case LetterKey.E:
                return new AccentPair[]
                { 
                    new('é', 'É'),
                    new('è', 'È'),
                    new('ê', 'Ê'),
                    new('ë', 'Ë'),
                    new('ē', 'Ē'),
                    new('ė', 'Ė'),
                    new('ę', 'Ę'),
                    new('€', '€'),
                };
            case LetterKey.G:
                return new AccentPair[]
                {
                    new('ğ', 'Ğ'),
                    new('ǧ', 'Ǧ'),
                    new('ĝ', 'Ĝ'),
                    new('ḡ', 'Ḡ'),
                };
            case LetterKey.H:
                return new AccentPair[]
                {
                    new('ḧ', 'Ḧ'),
                    new('ḫ', 'Ḫ'),
                    new('ḥ', 'Ḥ'),
                };
            case LetterKey.I:
                return new AccentPair[]
                { 
                    new('î', 'Î'),
                    new('ï', 'Ï'),
                    new('í', 'Í'),
                    new('ì', 'Ì'),
                    new('ī', 'Ī'),
                    new('ı', null),
                    new('ĭ', 'Ĭ'),
                    new('ĭ', 'Ĭ'),
                    new('ḭ', 'Ḭ'),
                    new('ỉ', 'Ỉ'),
                };
            case LetterKey.K:
                return new AccentPair[]
                {
                    new('ḵ', 'Ḵ'),
                    new('ḳ', 'Ḳ'),
                    new('ǩ', 'Ǩ'),
                };
            case LetterKey.L:
                return new AccentPair[]
                {
                    new('ɫ', 'Ɫ'),
                    new('ł', 'Ł'),
                    new('ḻ', 'Ḻ'),
                };
            
            case LetterKey.N:
                return new AccentPair[]
                { 
                    new('ñ', 'Ñ'),
                    new('ń', 'Ń'),
                    new('ṉ', 'Ṉ'),
                    new('ŋ', 'Ŋ'),
                };
            case LetterKey.O:
                return new AccentPair[]
                { 
                    new('ô', 'Ô'),
                    new('ö', 'Ö'),
                    new('ó', 'Ó'),
                    new('ò', 'Ò'),
                    new('õ', 'Õ'),
                    new('ō', 'Ō'),
                    new('ø', 'Ø'),
                    new('œ', 'Œ'),
                };
            case LetterKey.R:
                return new AccentPair[]
                {
                    new('ř', 'Ř'),
                    new('ṛ', 'Ṛ'),
                    new('ṟ', 'Ṟ'),
                    new('ṝ', 'Ṝ'),
                };
            case LetterKey.S:
                return new AccentPair[]
                { 
                    new('š', 'Š'),
                    new('ß', 'ẞ'),
                    new('ś', 'Ś'),
                    new('ş', 'Ş'),
                    new('ṣ', 'Ṣ'),
                };
            case LetterKey.T:
                return new AccentPair[]
                {
                    new('ṭ', 'Ṭ'),
                    new('ť', 'Ť'),
                };
            case LetterKey.U:
                return new AccentPair[]
                { 
                    new('û', 'Û'),
                    new('ù', 'Ù'),
                    new('ü', 'Ü'),
                    new('ú', 'Ú'),
                    new('ū', 'Ū'),
                    new('ṷ', 'Ṷ'),
                };
            case LetterKey.W:
                return new AccentPair[]
                {
                    new('ŵ', 'Ŵ'),
                };
            case LetterKey.X:
                return new AccentPair[]
                {
                    new('ẍ', 'Ẍ'),
                };
            case LetterKey.Y:
                return new AccentPair[]
                { 
                    new('ÿ', 'Ÿ'),
                    new('ý', 'Ý'),
                    new('ȳ', 'Ȳ'),
                    new('ŷ', 'Ŷ'),
                };
            case LetterKey.Z:
                return new AccentPair[]
                {
                    new('ž', 'Ž'),
                    new('ẕ', 'Ẕ'),
                    new('ż', 'Ż'),
                    new('ź', 'Ź'),
                };
        }

        throw new ArgumentException("Letter {0} is missing", letter.ToString());
    }
}

public record AccentPair(char? lower, char? upper);

public enum Position
{
    Top,
    Bottom,
    Left,
    Right,
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
    Center,
}
