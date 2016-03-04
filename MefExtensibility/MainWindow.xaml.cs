using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace MefExtensibility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CompositionContainer Container { get; set; }
        public AggregateCatalog Catalog { get; set; }
        public DirectoryCatalog DirectoryCatalog { get; set; }
        [ImportMany(typeof(ICustomButton), AllowRecomposition = true)]
        public ICustomButton[] mefButtons { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Catalog = new AggregateCatalog();           
            Catalog.Catalogs.Add(new AssemblyCatalog(typeof(MainWindow).Assembly));
            DirectoryCatalog = new DirectoryCatalog(@"c:\");
            DirectoryCatalog.Changed += (e, a) => { ButtonPanel.Children.Clear(); InsertButtons(); };
            Catalog.Catalogs.Add(DirectoryCatalog);
            Container = new CompositionContainer(Catalog);

            try
            {
                this.Container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }

            InsertButtons();
            var timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 5) };
            timer.Tick += (a, e) =>
            {
                DirectoryCatalog.Refresh();
            };
            timer.Start();
        }

        private void InsertButtons()
        {
            mefButtons.ToList().ForEach(x =>
            {
                x.ClickAction = new Action(() =>
                {
                    ColorList.Items.Add(x.GetColor());
                });
                ButtonPanel.Children.Add(x as UIElement);
            });
        }

       

       
    }
}
