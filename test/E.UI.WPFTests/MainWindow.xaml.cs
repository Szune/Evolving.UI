using E.UI.WPF;
using System.Windows;
using System.Windows.Controls;
using E.UI.Fluent;

namespace E.UI.WPFTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EditorTest _objectToEdit;
        private readonly SmallerTest _smallerObject;
        private readonly PrivateTests _privateTests;

        static MainWindow()
        {
            For<TestClass>
                .Generate(generator => new TextBox
                {
                    VerticalContentAlignment = VerticalAlignment.Center,
                    IsReadOnly = generator.IsReadOnly
                })
                .WithGetter(getter =>
                {
                    var value = ((TextBox) getter.Control).Text;
                    return new TestClass(value[0], value[1]);
                })
                .WithSetter(setter =>
                    ((TextBox) setter.Control).Text = $"{((TestClass) setter.Value)?.A}{((TestClass) setter.Value)?.B}");
        }
        public MainWindow()
        {
            InitializeComponent();
            _objectToEdit = new EditorTest() { TestInt = 100, TestString = "testString" };
            _smallerObject = new SmallerTest();
            _privateTests = new PrivateTests();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            /* TODO: if an object contains a member of a type that hasn't been registered,
             * TODO: -add an expander that opens up the properties and fields of that object?
             * check for self referencing loops using ReferenceEquals(this, object)
             * or maybe check this.GetType() == other.GetType()
             * because otherwise there could be an infinitely huge editor window */
            // TODO: UIRefresh attribute should start a new thread that refreshes all the members
            // TODO: -with that attribute applied at their specified intervals.
            // TODO: handle List<T> (ComboBox with option of adding and removing items?)
            // TODO: decide how to handle backing fields when including private members 
            // (probably just show the backing field but with the displayed text set to the property's actual name)
            // TODO: decide how to handle { get; }-properties, inject a SetValue method, ignore completely or let the user choose?
            new EditorWindow(_objectToEdit).ShowDialog();
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            new EditorWindow(_smallerObject).ShowDialog();
        }

        private void ButtonBase3_OnClick(object sender, RoutedEventArgs e)
        {
            new EditorWindow(_privateTests).ShowDialog();
            // putting a breakpoint after ShowDialog and closing the window somehow keeps the window alive..
        }
    }
}
