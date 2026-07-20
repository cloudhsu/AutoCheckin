using System;
using System.Drawing;

namespace AutoCheckIn
{
    internal sealed class UiTheme
    {
        private static readonly UiTheme[] Themes =
        {
            new UiTheme("Graphite Teal", Color.FromArgb(35, 48, 56), Color.FromArgb(43, 58, 66), Color.FromArgb(38, 166, 154), Color.FromArgb(236, 245, 244), Color.FromArgb(181, 199, 200)),
            new UiTheme("Fluent Blue", Color.FromArgb(241, 245, 249), Color.White, Color.FromArgb(37, 99, 235), Color.FromArgb(30, 41, 59), Color.FromArgb(71, 85, 105)),
            new UiTheme("Amber Console", Color.FromArgb(39, 39, 42), Color.FromArgb(45, 45, 49), Color.FromArgb(245, 158, 11), Color.FromArgb(244, 244, 245), Color.FromArgb(161, 161, 170))
        };

        private UiTheme(string name, Color window, Color surface, Color accent, Color text, Color mutedText)
        {
            Name = name; Window = window; Surface = surface; Accent = accent; Text = text; MutedText = mutedText;
        }

        public string Name { get; }
        public Color Window { get; }
        public Color Surface { get; }
        public Color Accent { get; }
        public Color Text { get; }
        public Color MutedText { get; }
        public static UiTheme Amber => Themes[2];
        public static UiTheme Random() => Themes[new Random().Next(Themes.Length)];
    }
}
