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
                    new('ḃ', 'Ḃ'),
                    new('ḅ', 'Ḅ'),
                    new('ƀ', 'Ƀ'),
                    new('ɓ', 'Ɓ'),
                    new('ꞗ', 'Ꞗ'),
                    new('ᵬ', null),
                    new('ᶀ', null),
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
                    new('ȼ', 'Ȼ'),
                    new('ꞓ', 'Ꞓ'),
                    new('ꞔ', 'Ꞔ'),
                    new('ƈ', 'Ƈ'),
                    new('ɕ', null),
                };
            case LetterKey.D:
                return new AccentPair[]
                {
                    new('ð', 'Ð'),
                    new('ḏ', 'Ḏ'),
                    new('ď', 'Ď'),
                    new('ḋ', 'Ḋ'),
                    new('ḑ', 'Ḑ'),
                    new('d̦', 'D̦'),
                    new('ḍ', 'Ḍ'),
                    new('ḓ', 'Ḓ'),
                    new('đ', 'Đ'),
                    new('ɖ', 'Ɖ'),
                    new('ꟈ', 'Ꟈ'),
                    new('ɗ', 'Ɗ'),
                    new('ᵭ', null),
                    new('ᶁ', null),
                    new('ᶑ', null),
                    new('ȡ', null),
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
                    new('ĕ', 'Ĕ'),
                    new('ế', 'Ế'),
                    new('ề', 'Ề'),
                    new('ễ', 'Ễ'),
                    new('ể', 'Ể'),
                    new('ê̄', 'Ê̄'),
                    new('ê̌', 'Ê̌'),
                    new('ě', 'Ě'),
                    new('ẽ', 'Ẽ'),
                    new('ė́', 'Ė́'),
                    new('ė̃', 'Ė̃'),
                    new('ȩ', 'Ȩ'),
                    new('ḝ', 'Ḝ'),
                    new('ę', 'Ę'),
                    new('ę́', 'Ę́'),
                    new('ę̃', 'Ę̃'),
                    new('ḗ', 'Ḗ'),
                    new('ḕ', 'Ḕ'),
                    new('ẻ', 'Ẻ'),
                    new('ȅ', 'Ȅ'),
                    new('e̋', 'E̋'),
                    new('ȇ', 'Ȇ'),
                    new('ẹ', 'Ẹ'),
                    new('ệ', 'Ệ'),
                    new('ḙ', 'Ḙ'),
                    new('ḛ', 'Ḛ'),
                    new('ɇ', 'Ɇ'),
                    new('e̩', 'E̩'),
                    new('è̩', 'È̩'),
                    new('é̩', 'É̩'),
                    new('ᶒ', null),
                    new('ⱸ', null),
                    new('ꬴ', null),
                    new('ꬳ', null),
                    new('€', '€'),
                };
            case LetterKey.F:
                return new AccentPair[]
                {
                    new('ḟ', 'Ḟ'),
                    new('ƒ', 'Ƒ'),
                    new('ꞙ', 'Ꞙ'),
                    new('ᵮ', null),
                    new('ᶂ', null),
                    new('₣', null),
                };
            case LetterKey.G:
                return new AccentPair[]
                {
                    new('ğ', 'Ğ'),
                    new('ǧ', 'Ǧ'),
                    new('ĝ', 'Ĝ'),
                    new('ḡ', 'Ḡ'),
                    new('ǵ', 'Ǵ'),
                    new('ǥ', 'Ǥ'),
                    new('ģ', 'Ģ'),
                    new('ɠ', 'Ɠ'),
                    new('ġ', 'Ġ'),
                    new('ꞡ', 'Ꞡ'),
                    new('g̃', 'G̃'),
                };
            case LetterKey.H:
                return new AccentPair[]
                {
                    new('ḧ', 'Ḧ'),
                    new('ḫ', 'Ḫ'),
                    new('ḥ', 'Ḥ'),
                    new('ĥ', 'Ĥ'),
                    new('ȟ', 'Ȟ'),
                    new('ḣ', 'Ḣ'),
                    new('ḩ', 'Ḩ'),
                    new('ẖ', 'H̱'),
                    new('ħ', 'Ħ'),
                    new('ⱨ', 'Ⱨ'),
                    new('ɦ', 'Ɦ'),
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
                    new('ḭ', 'Ḭ'),
                    new('ỉ', 'Ỉ'),
                    new('i̇́', 'İ́'),
                    new('i̇̀', 'İ̀'),
                    new('ǐ', 'Ǐ'),
                    new('ḯ', 'Ḯ'),
                    new('ĩ', 'Ĩ'),
                    new('i̇̃', 'İ̃'),
                    new('į', 'Į'),
                    new('į̇́', 'Į́'),
                    new('į̇̃', 'Į̃'),
                    new('ī̀', 'Ī̀'),
                    new('ȉ', 'Ȉ'),
                    new('i̋', 'I̋'),
                    new('ȋ', 'Ȋ'),
                    new('ị', 'Ị'),
                    new('ɨ', 'Ɨ'),
                    new(null, 'İ'),
                    new('ᶖ', null),
                };
            case LetterKey.J:
                return new AccentPair[]
                {
                    new('ĵ', 'Ĵ'),
                    new('ɉ', 'Ɉ'),
                    new('ǰ', 'J̌'),
                    new('ʝ', 'Ʝ'),
                    new('j̇̃', 'J̃'),
                    new('j̃', null)
                    new('ɟ', null),
                    new('ʄ', null),
                    new('ȷ', null),
                };
            case LetterKey.K:
                return new AccentPair[]
                {
                    new('ḵ', 'Ḵ'),
                    new('ḳ', 'Ḳ'),
                    new('ǩ', 'Ǩ'),
                    new('ḱ', 'Ḱ'),
                    new('ķ', 'Ķ'),
                    new('ƙ', 'Ƙ'),
                    new('ⱪ', 'Ⱪ'),
                    new('ꝁ', 'Ꝁ'),
                    new('ꝃ', 'Ꝃ'),
                    new('ꝅ', 'Ꝅ'),
                    new('ꞣ', 'Ꞣ'),
                    new('ᶄ', null),
                };
            case LetterKey.L:
                return new AccentPair[]
                {
                    new('ɫ', 'Ɫ'),
                    new('ł', 'Ł'),
                    new('ḻ', 'Ḻ'),
                    new('ĺ', 'Ĺ'),
                    new('ľ', 'Ľ'),
                    new('ļ', 'Ļ'),
                    new('ḷ', 'Ḷ'),
                    new('ḹ', 'Ḹ'),
                    new('l̃', 'L̃'),
                    new('ḽ', 'Ḽ'),
                    new('ŀ', 'Ŀ'),
                    new('ƚ', 'Ƚ'),
                    new('ꝉ', 'Ꝉ'),
                    new('ⱡ', 'Ⱡ'),
                    new('ɬ', 'Ɬ'),
                    new('ꝇ', 'Ꝇ'),
                    new('ꞎ', null),
                    new('ꬷ', null),
                    new('ꬸ', null),
                    new('ꬹ', null),
                    new('ᶅ', null),
                    new('ɭ', null),
                    new('ȴ', null),
                    new('£', '£'),
                    new('₤', '₤'),
                };
            case LetterKey.M:
                return new AccentPair[]
                {
                    new('ḿ', 'Ḿ'),
                    new('m̋', 'M̋'),
                    new('ṁ', 'Ṁ'),
                    new('ṃ', 'Ṃ'),
                    new('m̃', 'M̃'),
                    new('ɱ', 'Ɱ'),
                    new('ᵯ', null),
                    new('ᶆ', null),
                    new('ꬺ', null),
                    new('ɰ', null),
                    new('ℳ', 'ℳ'),
                };
            case LetterKey.N:
                return new AccentPair[]
                { 
                    new('ñ', 'Ñ'),
                    new('ń', 'Ń'),
                    new('ṉ', 'Ṉ'),
                    new('ŋ', 'Ŋ'),
                    new('ǹ', 'Ǹ'),
                    new('ň', 'Ň'),
                    new('ṅ', 'Ṅ'),
                    new('ņ', 'Ņ'),
                    new('ṇ', 'Ṇ'),
                    new('ṋ', 'Ṋ'),
                    new('n̈', 'N̈'),
                    new('ɲ', 'Ɲ'),
                    new('ƞ', 'Ƞ'),
                    new('ꞑ', 'Ꞑ'),
                    new('ꞥ', 'Ꞥ'),
                    new(ᵰ, null),
                    new(ᶇ, null),
                    new(ɳ, null),
                    new(ȵ, null),
                    new(ꬻ, null),
                    new(ꬼ, null),
                    new('₦', '₦'),
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
                    new('ŏ', 'Ŏ'),
                    new('ố', 'Ố'),
                    new('ồ', 'Ồ'),
                    new('ỗ', 'Ỗ'),
                    new('ổ', 'Ổ'),
                    new('ǒ', 'Ǒ'),
                    new('ȫ', 'Ȫ'),
                    new('ő', 'Ő'),
                    new('ṍ', 'Ṍ'),
                    new('ṏ', 'Ṏ'),
                    new('ȭ', 'Ȭ'),
                    new('ȯ', 'Ȯ'),
                    new('o͘', 'O͘'),
                    new('ȱ', 'Ȱ'),
                    new('ǿ', 'Ǿ'),
                    new('ǫ', 'Ǫ'),
                    new('ǭ', 'Ǭ'),
                    new('ṓ', 'Ṓ'),
                    new('ṑ', 'Ṑ'),
                    new('ỏ', 'Ỏ'),
                    new('ȍ', 'Ȍ'),
                    new('ȏ', 'Ȏ'),
                    new('ơ', 'Ơ'),
                    new('ớ', 'Ớ'),
                    new('ờ', 'Ờ'),
                    new('ỡ', 'Ỡ'),
                    new('ở', 'Ở'),
                    new('ợ', 'Ợ'),
                    new('ọ', 'Ọ'),
                    new('ộ', 'Ộ'),
                    new('o̩', 'O̩'),
                    new('ò̩', 'Ò̩'),
                    new('ó̩', 'Ó̩'),
                    new('ɵ', 'Ɵ'),
                    new('ꝋ', 'Ꝋ'),
                    new('ꝍ', 'Ꝍ'),
                    new('o͍', 'O͍'),
                    new('œ', 'Œ'),
                    new('ꭀ', null),
                    new('ꭁ', null),
                    new('ꭂ', null),
                    new('ⱺ', null),
                };
            case LetterKey.P:
                return new AccentPair[]
                {
                    new('ṕ', 'Ṕ'),
                    new('ṗ', 'Ṗ'),
                    new('ᵽ', 'Ᵽ'),
                    new('ƥ', 'Ƥ'),
                    new('p̃', 'P̃'),
                    new('ꝑ', 'Ꝑ'),
                    new('ꝓ', 'Ꝓ'),
                    new('ꝕ', 'Ꝕ')
                    new('ᵱ', null),
                    new('ᶈ', null),
                    new('₱', '₱'),
                    new('℗', '℗'),
                };
            case LetterKey.Q:
                return new AccentPair[]
                {
                    new('ꝗ', 'Ꝗ'),
                    new('ꝙ', 'Ꝙ'),
                    new('ɋ', 'Ɋ'),
                    new('q̃', 'Q̃'),
                    new('ʠ', null),
                };
            case LetterKey.R:
                return new AccentPair[]
                {
                    new('ř', 'Ř'),
                    new('ṛ', 'Ṛ'),
                    new('ṟ', 'Ṟ'),
                    new('ṝ', 'Ṝ'),
                    new('ŕ', 'Ŕ'),
                    new('ṙ', 'Ṙ'),
                    new('ŗ', 'Ŗ'),
                    new('ȑ', 'Ȑ'),
                    new('ȓ', 'Ȓ'),
                    new('r̃', 'R̃'),
                    new('ɍ', 'Ɍ'),
                    new('ꞧ', 'Ꞧ'),
                    new('ɽ', 'Ɽ'),
                    new('ꝵ', 'ꝶ'),
                    new('ꝛ', 'Ꝛ'),
                    new('ꝝ', 'Ꝝ'),
                    new('ᵲ', null),
                    new('ꭈ', null),
                    new('ꭉ', null),
                    new('ꭊ', null),
                    new('ꭨ', null),
                    new(null, 'ꭆ'),
                    new('ꭇ', null),
                    new(null, '℟'),
                    new(null, '℞'),
                    new('ɼ', null),
                    new('ᵳ', null),
                    new('ᶉ', null),
                    new('₹', '₹'),
                    new('₨', '₨'),
                    new('®', '®'),
                };
            case LetterKey.S:
                return new AccentPair[]
                { 
                    new('š', 'Š'),
                    new('ß', 'ẞ'),
                    new('ś', 'Ś'),
                    new('ş', 'Ş'),
                    new('ṣ', 'Ṣ'),
                    new('ṥ', 'Ṥ'),
                    new('ŝ', 'Ŝ'),
                    new('ṧ', 'Ṧ'),
                    new('s̈', 'S̈'),
                    new('ṡ', 'Ṡ'),
                    new('ṩ', 'Ṩ'),
                    new('ș', 'Ș'),
                    new('s̩', 'S̩'),
                    new('ṡ̃', 'Ṡ̃'),
                    new('ꞅ', 'Ꞅ'),
                    new('ꞩ', 'Ꞩ'),
                    new('ꟊ', 'Ꟊ'),
                    new('ȿ', 'Ȿ'),
                    new('ʂ', 'Ʂ'),
                    new('ƨ', 'Ƨ'),
                    new('ꟙ', 'Ꟙ'),
                    new('ᶊ', null),
                    new('ᵴ', null),
                    new('ſ', null),
                    new('ẜ', null),
                    new('ẛ', null),
                    new('$', '$'),
                    new('₷', '₷'),
                    new('§', '§'),
                };
            case LetterKey.T:
                return new AccentPair[]
                {
                    new('ṭ', 'Ṭ'),
                    new('ť', 'Ť'),
                    new('ṫ', 'Ṫ'),
                    new('ţ', 'Ţ'),
                    new('ț', 'Ț'),
                    new('ṱ', 'Ṱ'),
                    new('ṯ', 'Ṯ'),
                    new('ŧ', 'Ŧ'),
                    new('ⱦ', 'Ⱦ'),
                    new('ƭ', 'Ƭ'),
                    new('ʈ', 'Ʈ'),
                    new('ẗ', 'T̈'),
                    new('ꞇ', 'Ꞇ'),
                    new('ʇ', 'Ʇ')
                    new('ȶ', null),
                    new('ᵵ', null),
                    new('ƫ', null),
                    new('₮', '₮'),
                    new('₸', '₸'),
                    new('৳', '৳'),
                    new('™', '™'),
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
