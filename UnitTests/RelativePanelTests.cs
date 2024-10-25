using System.Windows;
using System.Windows.Shapes;
using EleCho.WpfSuite;
using EleCho.WpfSuite.Layouts;

namespace UnitTests
{

    public class RelativePanelTests
    {
        [Test]
        [TestCase(100, 100)]
        public void RelativePanelMeasuresCorrectly1(double availableWidth, double availableHeight)
        {
            CrossThreadTestRunner runner = new CrossThreadTestRunner();
            runner.RunInSTA(() =>
            {
                var rect1 = new Rectangle { Height = 20, Width = 20 };
                var rect2 = new Rectangle { Height = 20, Width = 20 };

                var target = new RelativePanel
                {
                    Children =
                    {
                        rect1, rect2
                    }
                };

                RelativePanel.SetBelow(rect2, rect1);
                target.Measure(new Size(availableWidth, availableHeight));
                target.Arrange(new Rect(0, 0, availableWidth, availableHeight));

                Assert.AreEqual(target.DesiredSize.Width, 20.0);
                Assert.AreEqual(target.DesiredSize.Height, 40.0);
            });
        }

        [Test]
        [TestCase(100, 100)]
        public void RelativePanelMeasuresCorrectly2(double availableWidth, double availableHeight)
        {
            CrossThreadTestRunner runner = new CrossThreadTestRunner();
            runner.RunInSTA(() =>
            {
                var rect1 = new Rectangle { Height = 20, Width = 20 };
                var rect2 = new Rectangle { Height = 20, Width = 20 };

                var target = new RelativePanel
                {
                    Children =
                    {
                        rect1, rect2
                    }
                };

                RelativePanel.SetRightOf(rect2, rect1);
                target.Measure(new Size(availableWidth, availableHeight));
                target.Arrange(new Rect(0, 0, availableWidth, availableHeight));

                Assert.AreEqual(target.DesiredSize.Width, 40.0);
                Assert.AreEqual(target.DesiredSize.Height, 20.0);
            });
        }

        [Test]
        public void RelativePanelMeasuresCorrectly3()
        {
            CrossThreadTestRunner runner = new CrossThreadTestRunner();
            runner.RunInSTA(() =>
            {
                var rect1 = new Rectangle { Height = 20, Width = 20 };
                var rect2 = new Rectangle { Height = 20, Width = 20 };
                var rect3 = new Rectangle { Height = 20, Width = 20 };

                var target = new RelativePanel
                {
                    Children =
                    {
                        rect1, rect2, rect3
                    }
                };

                RelativePanel.SetAlignHorizontalCenterWithPanel(rect1, true);
                RelativePanel.SetRightOf(rect2, rect1);
                RelativePanel.SetRightOf(rect3, rect2);

                target.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                target.Arrange(new Rect(0, 0, target.DesiredSize.Width, target.DesiredSize.Height));

                Assert.AreEqual(100.0, target.ActualWidth);
                Assert.AreEqual(20, target.ActualHeight);
            });
        }

    }
}