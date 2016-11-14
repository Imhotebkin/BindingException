using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphTesting.Views
{
    /// <summary>
    /// Interaction logic for TouchView.xaml
    /// </summary>
    public partial class TouchView : Window
    {
        private TransformGroup transformGroup;
        TranslateTransform translation;
        ScaleTransform scale;
        RotateTransform rotation;

        public TouchView()
        {
            InitializeComponent();
            this.ManipulationStarting += this.TouchableThing_ManipulationStarting;
            this.ManipulationDelta += this.TouchableThing_ManipulationDelta;
            this.ManipulationInertiaStarting += this.TouchableThing_ManipulationInertiaStarting;

            this.transformGroup = new TransformGroup();

            this.translation = new TranslateTransform(0, 0);
            this.scale = new ScaleTransform(1, 1);
            this.rotation = new RotateTransform(0);

            this.transformGroup.Children.Add(this.rotation);
            this.transformGroup.Children.Add(this.scale);
            this.transformGroup.Children.Add(this.translation);

            this.BasicRect.RenderTransform = this.transformGroup;
        }

        void TouchableThing_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this;
        }

        void TouchableThing_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            Rect containerBounds = new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);
            Rect objectBounds = this.transformGroup.TransformBounds(new Rect(this.BasicRect.RenderSize));

            if (e.IsInertial && !containerBounds.Contains(objectBounds))
            {
                e.Complete();
            }

            // the center never changes in this sample, although we always compute it.
            Point center = new Point(this.BasicRect.RenderSize.Width / 2.0, this.BasicRect.RenderSize.Height / 2.0);

            // apply the rotation at the center of the rectangle if it has changed
            this.rotation.CenterX = center.X;
            this.rotation.CenterY = center.Y;
            this.rotation.Angle += e.DeltaManipulation.Rotation;


            // Scale is always uniform, by definition, so the x and y will always have the same magnitude if it has changed

            this.scale.CenterX = center.X;
            this.scale.CenterY = center.Y;
            this.scale.ScaleX *= e.DeltaManipulation.Scale.X;
            this.scale.ScaleY *= e.DeltaManipulation.Scale.Y;

            // apply translation 

            this.translation.X += e.DeltaManipulation.Translation.X;
            this.translation.Y += e.DeltaManipulation.Translation.Y;            
        }
        
        void TouchableThing_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior = new InertiaTranslationBehavior();
            e.TranslationBehavior.InitialVelocity = e.InitialVelocities.LinearVelocity;
            // 10 inches per second squared
            e.TranslationBehavior.DesiredDeceleration = 10 * 96 / (1000 * 1000);
            

            e.ExpansionBehavior = new InertiaExpansionBehavior();
            e.ExpansionBehavior.InitialVelocity = e.InitialVelocities.ExpansionVelocity;
            // .1 inches per second squared.
            e.ExpansionBehavior.DesiredDeceleration = 0.1 * 96 / 1000.0 * 1000.0;

            e.RotationBehavior = new InertiaRotationBehavior();
            e.RotationBehavior.InitialVelocity = e.InitialVelocities.AngularVelocity;
            // 720 degrees per second squared.
            e.RotationBehavior.DesiredDeceleration = 720 / (1000.0 * 1000.0);            
        }
    }
}
