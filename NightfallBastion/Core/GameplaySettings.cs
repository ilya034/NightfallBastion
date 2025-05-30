using System.IO;
using System.Text.Json;

namespace NightfallBastion.Core
{
    public class GameplaySettings
    {
        public int CameraSpeed { get; set; } = 400;
        public float EnemySpawnRate { get; set; } = 1.0f;
        public float EnemySpawnInterval { get; set; } = 3.0f;
        public int EnemyHealth { get; set; } = 50;
        public float EnemySpeed { get; set; } = 10.0f;
        public int EnemyDamage { get; set; } = 25;
        private static readonly string SavePath = "gameplay_settings.json";

        public void Save()
        {
            var json = JsonSerializer.Serialize(this);
            File.WriteAllText(SavePath, json);
        }

        public void Load()
        {
            if (File.Exists(SavePath))
            {
                var json = File.ReadAllText(SavePath);
                var loaded = JsonSerializer.Deserialize<GameplaySettings>(json);
                if (loaded != null)
                {
                    CameraSpeed = loaded.CameraSpeed;
                    EnemySpawnRate = loaded.EnemySpawnRate;
                    EnemySpawnInterval = loaded.EnemySpawnInterval;
                    EnemyHealth = loaded.EnemyHealth;
                    EnemySpeed = loaded.EnemySpeed;
                    EnemyDamage = loaded.EnemyDamage;
                }
            }
        }
    }
}
