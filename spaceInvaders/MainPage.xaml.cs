using System;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace spaceInvaders
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer dispatcherTimer;
        private Player player;
        private bool playerIsMovingLeft;
        private bool playerIsMovingRight;

        public MainPage()
        {
            InitializeComponent();            
    
            player = new Player();
            canvas.Children.Add(player.turret);
            playerIsMovingLeft = playerIsMovingRight = false;

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Game;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(15);
            dispatcherTimer.Start();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        private void Game(object sender, object e)
        {
            if (playerIsMovingLeft) player.moveLeft();
            if (playerIsMovingRight) player.moveRight();
        }

        void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            if(e.VirtualKey == Windows.System.VirtualKey.Left)
            {
                playerIsMovingRight = false;
                playerIsMovingLeft = true;
            } else if(e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                playerIsMovingLeft = false;
                playerIsMovingRight = true;
            }
        }

        void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs e)
        {
            if(e.VirtualKey == Windows.System.VirtualKey.Left || e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                playerIsMovingLeft = playerIsMovingRight = false;
            }
        }
    }
}
