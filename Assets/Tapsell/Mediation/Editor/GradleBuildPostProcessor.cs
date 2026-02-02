using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Android;

namespace Tapsell.Mediation.Editor
{
    public class GradleBuildPostProcessor : IPostGenerateGradleAndroidProject
    {
        private const string GradlePropertiesFile = "gradle.properties";
        private const string JetifierIgnorePropertyKey = "android.jetifier.ignorelist";
        private const string MoshiPackage = "com.squareup.moshi";

        public int callbackOrder => 10;

        public void OnPostGenerateGradleAndroidProject(string path)
        {
            var gradlePropertiesFile = Path.GetFullPath(Path.Combine(path, "..", GradlePropertiesFile));

            // Only proceed if file exists; don't create a new file.
            if (!File.Exists(gradlePropertiesFile)) return;

            var lines = File.ReadAllLines(gradlePropertiesFile).ToList();

            // Try to find existing key line (ignoring leading spaces, but not commented-out lines)
            var index = -1;
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var trimmedStart = line.TrimStart();
                if (trimmedStart.StartsWith("#")) continue; // skip commented lines
                if (!trimmedStart.StartsWith(JetifierIgnorePropertyKey + "=")) continue;
                index = i;
                break;
            }

            if (index >= 0)
            {
                // Append Moshi package to the existing value if not present
                var line = lines[index].Trim();
                var valuePart = line.Substring(JetifierIgnorePropertyKey.Length + 1); // after '='

                // Build new value, avoid duplicates
                var parts = valuePart
                    .Split(',')
                    .Select(p => p.Trim())
                    .ToList();

                if (parts.Contains(MoshiPackage)) return; // moshi is present -> no change

                lines[index] = "# " + line + " --- Tapsell Mediation Properties Will Be Handled!";
                AppendTapsellPropertiesBlock(lines, parts.Append(MoshiPackage).ToList());
            }
            else
            {
                AppendTapsellPropertiesBlock(lines, new List<string> { MoshiPackage });
            }

            // Write without trailing newline to match test expectations
            var content = string.Join(Environment.NewLine, lines);
            File.WriteAllText(gradlePropertiesFile, content);
        }

        private void AppendTapsellPropertiesBlock(List<string> lines, List<string> ignoreList)
        {
            // # Tapsell Mediation Properties Start
            // key=value
            // # Tapsell Mediation Properties End
            lines.Add("# Tapsell Mediation Properties Start");
            lines.Add(JetifierIgnorePropertyKey + "=" + string.Join(",", ignoreList));
            lines.Add("# Tapsell Mediation Properties End");
        }
    }
}