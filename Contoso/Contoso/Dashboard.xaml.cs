using Microsoft.Services.Store.Engagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Contoso
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dashboard : Page
    {
        private Compositor _compositor;

        public Dashboard()
        {
            this.InitializeComponent();
            this.Loaded += Dashboard_Loaded;
        }

        #region Methods
        private void Dashboard_Loaded(object sender, RoutedEventArgs e)
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

            var menu = ElementCompositionPreview.GetElementVisual(this.Menu);

            var opacityAnimation = _compositor.CreateScalarKeyFrameAnimation();
            opacityAnimation.Duration = TimeSpan.FromMilliseconds(2000);
            opacityAnimation.InsertKeyFrame(0f, 0f);
            opacityAnimation.InsertKeyFrame(1f, 1f);
            menu.StartAnimation("Opacity", opacityAnimation);

            this.SetupTooltip();
            this.HideAll();
        }

        private void MessageButton_Click(object sender, RoutedEventArgs e)
        {
            this.MessagesControl.Visibility = Visibility.Visible;
            this.ReportControl.Visibility = Visibility.Collapsed;
        }

        private void HideAll()
        {
            this.MessagesControl.Visibility = Visibility.Collapsed;
            this.ReportControl.Visibility = Visibility.Collapsed;
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            this.MessagesControl.Visibility = Visibility.Collapsed;
            this.ReportControl.Visibility = Visibility.Visible;
        }
        #endregion

        private async void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            if (StoreServicesFeedbackLauncher.IsSupported())
            {
                await StoreServicesFeedbackLauncher.GetDefault().LaunchAsync();
            }
        }

        private ToolTip messageButtonTooltip, reportButtonTooltip, selfieButtonTooltip, feedbackButtonTooltip;

        private void SetupTooltip()
        {
            messageButtonTooltip = new ToolTip { Content = this.MessageButton.AccessKey };
            reportButtonTooltip = new ToolTip { Content = this.ReportButton.AccessKey };
            selfieButtonTooltip = new ToolTip { Content = this.SelfieButton.AccessKey };
            feedbackButtonTooltip = new ToolTip { Content = this.FeedbackButton.AccessKey };

            ToolTipService.SetPlacement(this.MessageButton, PlacementMode.Right);
            ToolTipService.SetPlacement(this.ReportButton, PlacementMode.Right);
            ToolTipService.SetPlacement(this.SelfieButton, PlacementMode.Right);
            ToolTipService.SetPlacement(this.FeedbackButton, PlacementMode.Right);

            ToolTipService.SetToolTip(this.MessageButton, messageButtonTooltip);
            ToolTipService.SetToolTip(this.ReportButton, reportButtonTooltip);
            ToolTipService.SetToolTip(this.SelfieButton, selfieButtonTooltip);
            ToolTipService.SetToolTip(this.FeedbackButton, feedbackButtonTooltip);
        }

        private void MenuButton_AccessKeyDisplayRequested(UIElement sender, AccessKeyDisplayRequestedEventArgs args)
        {
            messageButtonTooltip.IsOpen = true;
            reportButtonTooltip.IsOpen = true;
            selfieButtonTooltip.IsOpen = true;
            feedbackButtonTooltip.IsOpen = true;
        }

        private void MenuButton_AccessKeyDisplayDismissed(UIElement sender, AccessKeyDisplayDismissedEventArgs args)
        {
            messageButtonTooltip.IsOpen = false;
            reportButtonTooltip.IsOpen = false;
            selfieButtonTooltip.IsOpen = false;
            feedbackButtonTooltip.IsOpen = false;
        }
    }
}
