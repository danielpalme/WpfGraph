using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Palmmedia.WpfGraph.Common;
using Palmmedia.WpfGraph.UI.Interaction;
using Palmmedia.WpfGraph.UI.ViewModels;

namespace Palmmedia.WpfGraph.UI.Elements3D
{
    /// <summary>
    /// Base class for visual graph elements.
    /// </summary>
    public abstract class GraphUIElement : UIElement3D
    {
        /// <summary>
        /// The radius of a node.
        /// </summary>
        protected const double NODERADIUS = 5;

        /// <summary>
        /// The duration of one flash during blink animation.
        /// </summary>
        private const double BLINKDURATION = 200;

        /// <summary>
        /// The blink color.
        /// </summary>
        private static readonly Color BLINKCOLOR = Colors.Orange;
        
        /// <summary>
        /// Observer to receive PropertyChanged events.
        /// This field is required to prevent garbage collection of the observer.
        /// </summary>
        private readonly PropertyObserver<GraphDataBase> observer;

        /// <summary>
        /// The model.
        /// </summary>
        private static readonly DependencyProperty ModelProperty = DependencyProperty.Register(
            "Model",
            typeof(Model3D),
            typeof(GraphUIElement),
            new PropertyMetadata(ModelPropertyChanged));

        /// <summary>
        /// The color.
        /// </summary>
        private static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color",
            typeof(Color),
            typeof(GraphUIElement),
            new PropertyMetadata(ColorPropertyChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphUIElement"/> class.
        /// </summary>
        /// <param name="graphProvider">The <see cref="IGraphProvider"/>.</param>
        /// <param name="graphData">The graph data.</param>
        public GraphUIElement(IGraphProvider graphProvider, GraphDataBase graphData)
        {
            this.GraphProvider = graphProvider;

            var colorBinding = new Binding();
            colorBinding.Mode = BindingMode.OneWay;
            colorBinding.Source = graphData;
            colorBinding.Path = new PropertyPath("Color");
            BindingOperations.SetBinding(this, GraphUIElement.ColorProperty, colorBinding);

            graphData.Blinking += new System.EventHandler<AnimationEventArgs>(this.Blinking);

            this.observer = new PropertyObserver<GraphDataBase>(graphData)
                               .RegisterHandler(g => g.Marked, g => this.InvalidateModel());
        }

        /// <summary>
        /// Gets the IGraphProvider.
        /// </summary>
        protected IGraphProvider GraphProvider { get; private set; }

        /// <summary>
        /// Gets or sets the model. This is a dependency property.
        /// </summary>
        protected Model3D Model
        {
            get
            {
                return (Model3D)this.GetValue(ModelProperty);
            }

            set
            {
                this.SetValue(ModelProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color. This is a dependency property.
        /// </summary>
        protected Color Color
        {
            get
            {
                return (Color)this.GetValue(ColorProperty);
            }

            set
            {
                this.SetValue(ColorProperty, value);
            }
        }

        /// <summary>
        /// Flashed the <see cref="GraphUIElement"/>.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Palmmedia.WpfGraph.UI.ViewModels.AnimationEventArgs"/> instance containing the event data.</param>
        protected virtual void Blinking(object sender, AnimationEventArgs e)
        {
            double repetitions = Math.Round(Math.Max(e.Duration, BLINKDURATION) / BLINKDURATION);

            var colorAnimation = new ColorAnimation();
            colorAnimation.Duration = TimeSpan.FromMilliseconds(BLINKDURATION);
            colorAnimation.From = this.Color;
            colorAnimation.To = BLINKCOLOR;
            colorAnimation.AutoReverse = true;
            colorAnimation.RepeatBehavior = new RepeatBehavior(repetitions);
            colorAnimation.FillBehavior = FillBehavior.Stop;

            if (e.Callback != null)
            {
                colorAnimation.Completed += new EventHandler((s, a) => e.Callback());
            }

            this.BeginAnimation(GraphUIElement.ColorProperty, colorAnimation);
        }

        /// <summary>
        /// Executed when the model has changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> .</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (GraphUIElement)d;
            element.Visual3DModel = (Model3D)e.NewValue;
        }

        /// <summary>
        /// Executed when the color has changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> .</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((GraphUIElement)d).InvalidateModel();
        }
    }
}