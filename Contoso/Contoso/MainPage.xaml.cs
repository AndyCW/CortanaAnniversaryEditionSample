using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Contoso
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Compositor _compositor;
        private GaussianBlurEffect _blurEffect;
        private CompositionEffectBrush _blurBrush;
        private Visual _logo;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the compositor for the application to hook into the Visual Layer
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

            // Create the blur effect
            _blurEffect = new GaussianBlurEffect
            {
                Name = "Blur",
                BlurAmount = 0f,
                Optimization = EffectOptimization.Balanced,
                BorderMode = EffectBorderMode.Hard,
                Source = new CompositionEffectSourceParameter("source")
            };

            // Create a brush to which I want to apply. I also have noted that BlurAmount should be left out of the compiled shader.
            _blurBrush = _compositor.CreateEffectFactory(_blurEffect, new[] { "Blur.BlurAmount" }).CreateBrush();

            // By default composition eases in, I don't want that.
            var linearEasing = _compositor.CreateLinearEasingFunction();

            // Create an animation to change the blur amount over time
            var blurAnimation = _compositor.CreateScalarKeyFrameAnimation();
            blurAnimation.InsertKeyFrame(1f, 100.0f, linearEasing);
            blurAnimation.Duration = TimeSpan.FromSeconds(6);
            _blurBrush.StartAnimation("Blur.BlurAmount", blurAnimation);

            // Set the source of the blur as a backdrop brush
            _blurBrush.SetSourceParameter("source", _compositor.CreateBackdropBrush());

            // Create a Sprite to add back into the visual layer with the blur brush.
            var blurSprite = _compositor.CreateSpriteVisual();
            blurSprite.Size = new System.Numerics.Vector2((float)Window.Current.Bounds.Width, (float)Window.Current.Bounds.Height);
            blurSprite.Brush = _blurBrush;

            ElementCompositionPreview.SetElementChildVisual(this.BackgroundImage, blurSprite);

            // Animate opacity
            var backgroundVisual = ElementCompositionPreview.GetElementVisual(this.BackgroundImage);
            var opacityAnimation = _compositor.CreateScalarKeyFrameAnimation();
            opacityAnimation.InsertKeyFrame(0.0f, 0.0f);
            opacityAnimation.InsertKeyFrame(1f, 1.0f, linearEasing);
            opacityAnimation.Duration = TimeSpan.FromSeconds(3);
            backgroundVisual.StartAnimation("Opacity", opacityAnimation);

            _logo = ElementCompositionPreview.GetElementVisual(this.Logo);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var batch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
            batch.Completed += (s, args) => Frame.Navigate(typeof(Dashboard));
            
            var usernameStackPanel = ElementCompositionPreview.GetElementVisual(this.UsernameStackPanel);
            var passwordStackPanel = ElementCompositionPreview.GetElementVisual(this.PasswordStackPanel);
            var logo = ElementCompositionPreview.GetElementVisual(this.Logo);
            var loginButton = ElementCompositionPreview.GetElementVisual(this.LoginButton);

            var logoOffsetAnimation = _compositor.CreateVector3KeyFrameAnimation();
            logoOffsetAnimation.InsertKeyFrame(1f, new System.Numerics.Vector3(5f, 10f, 0f));
            logoOffsetAnimation.Duration = TimeSpan.FromSeconds(1);
            _logo.StartAnimation("Offset", logoOffsetAnimation);

            var logoSizeAnimation = _compositor.CreateVector3KeyFrameAnimation();
            logoSizeAnimation.InsertKeyFrame(1f, new System.Numerics.Vector3(0.5f, 0.5f, 1f));
            logoSizeAnimation.Duration = TimeSpan.FromSeconds(1);
            _logo.StartAnimation("scale", logoSizeAnimation);

            var opacityAnimation = _compositor.CreateScalarKeyFrameAnimation();
            opacityAnimation.InsertKeyFrame(0.0f, 0.0f);
            opacityAnimation.Duration = TimeSpan.FromSeconds(1);

            usernameStackPanel.StartAnimation("Opacity", opacityAnimation);
            passwordStackPanel.StartAnimation("Opacity", opacityAnimation);
            loginButton.StartAnimation("Opacity", opacityAnimation);

            var blurAnimation = _compositor.CreateScalarKeyFrameAnimation();
            blurAnimation.InsertKeyFrame(1f, 0.0f);
            blurAnimation.Duration = TimeSpan.FromMilliseconds(500);
            _blurBrush.StartAnimation("Blur.BlurAmount", blurAnimation);

            batch.End();
        }
    }
}
