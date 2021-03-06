﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace osu_Player
{
    public class Settings
    {
        public const int VERSION = 0x01;
        private const string PATH = "settings.json";

        [JsonProperty("version")]
        public int? Version { get; set; }

        [JsonProperty("use_splash")]
        public bool UseSplashScreen { get; set; } = true;

        [JsonProperty("use_animation")]
        public bool UseAnimation { get; set; } = true;

        [JsonProperty("osu_path")]
        public string OsuPath { get; set; }

        [JsonProperty("audio_device")]
        public int AudioDevice { get; set; } = 0;

        [JsonProperty("disabled_songs")]
        public List<Song> DisabledSongs { get; set; }

        public static Settings Read()
        {
            var json = File.ReadAllText(PATH);
            return JsonConvert.DeserializeObject<Settings>(json);
        }

        public void Write()
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(PATH, json);
        }
    }
}