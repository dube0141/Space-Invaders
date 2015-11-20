using System;
using System.Collections;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace spaceInvaders
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer dispatcherTimer;

        private Player player;
        private Invaders invaders;

        private ArrayList invaderGrid;

        private bool playerIsMovingLeft;
        private bool playerIsMovingRight;

        private bool invadersAreMovingLeft;

        private int count = 0;
        private double sizeRatio = 0.80;

        public MainPage()
        {
            InitializeComponent();

            player = new Player(sizeRatio);
            canvas.Children.Add(player.getPlayer());
            playerIsMovingLeft = playerIsMovingRight = invadersAreMovingLeft = false;

            invaders = new Invaders(sizeRatio);
            invaderGrid = invaders.invaderGrid;

            foreach (Image i in invaderGrid)
            {
                canvas.Children.Add(i);
            }

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Game;
            dispatcherTimer.Interval = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
            dispatcherTimer.Start();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        private void Game(object sender, object e)
        {
            if (playerIsMovingLeft) player.moveLeft();
            if (playerIsMovingRight) player.moveRight();
            invaders.toggleSprite(count);

            if (invaders.collision())
            {
                invaders.moveDown();

                if (invadersAreMovingLeft)
                {
                    invadersAreMovingLeft = false;
                    invaders.moveRight();
                }
                else
                {
                    invadersAreMovingLeft = true;
                    invaders.moveLeft();
                }
            }

            if (invadersAreMovingLeft) invaders.moveLeft();
            else invaders.moveRight();

            count++;
        }

        void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.Left)
            {
                playerIsMovingRight = false;
                playerIsMovingLeft = true;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                playerIsMovingLeft = false;
                playerIsMovingRight = true;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.Space)
            {
                //player.shoot();
            }
        }

        void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.Left)
            {
                playerIsMovingLeft = false;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                playerIsMovingRight = false;
            }
        }
    }
}
