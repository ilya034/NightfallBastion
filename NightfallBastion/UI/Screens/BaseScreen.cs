// using Microsoft.Xna.Framework;
// using NightfallBastion.UI.Views;

// namespace NightfallBastion.UI.Screens
// {
//     public abstract class BaseScreen
//     {
//         public UI UI { get; private set; }
//         public Game Game { get; private set; }
//         public BaseView View { get; private set; }

//         public BaseScreen(UI ui, Game game)
//         {
//             UI = ui;
//             Game = game;
//         }

//         public virtual void Update(GameTime gameTime) { }

//         public void Show()
//         {
//             UI.Show(this);
//         }

//         public void Hide()
//         {
//             UI.Hide(this);
//         }
//     }
// } 