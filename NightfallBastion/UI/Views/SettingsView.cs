using System;
using NightfallBastion.Core;

namespace NightfallBastion.UI
{
    public class SettingsView(NightfallBastionGame game) : View(game)
    {
        public event Action OnBackButtonClicked;

        public override void BuildUI() { }
    }
}
