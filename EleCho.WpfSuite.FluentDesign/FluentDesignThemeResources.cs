using System;
using System.Windows;

namespace EleCho.WpfSuite.FluentDesign
{
    public class FluentDesignThemeResources : ResourceDictionary
    {
        private readonly ResourceDictionary _lightThemeResources = new();
        private readonly ResourceDictionary _darkThemeResources = new();

        public FluentDesignThemeResources()
        {
            _lightThemeResources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.FluentDesign;component/Themes/Light.xaml") };
            _darkThemeResources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.FluentDesign;component/Themes/Dark.xaml") };

            MergedDictionaries.Add(_lightThemeResources);
        }

        public bool IsDarkMode
        {
            get => MergedDictionaries.Contains(_darkThemeResources);
            set
            {
                MergedDictionaries.Clear();

                if (value)
                {
                    MergedDictionaries.Add(_darkThemeResources);
                }
                else
                {
                    MergedDictionaries.Add(_lightThemeResources);
                }
            }
        }

        // color keys
        public static readonly FluentDesignResourceKey TextColorKey = new FluentDesignResourceKey(nameof(TextColorKey));

        public static readonly FluentDesignResourceKey ControlBackgroundColorKey = new FluentDesignResourceKey(nameof(ControlBackgroundColorKey));
        public static readonly FluentDesignResourceKey ControlHoverBackgroundColorKey = new FluentDesignResourceKey(nameof(ControlHoverBackgroundColorKey));
        public static readonly FluentDesignResourceKey ControlPressedBackgroundColorKey = new FluentDesignResourceKey(nameof(ControlPressedBackgroundColorKey));

        public static readonly FluentDesignResourceKey TextBoxBackgroundColorKey = new FluentDesignResourceKey(nameof(TextBoxBackgroundColorKey));
        public static readonly FluentDesignResourceKey TextBoxHoverBackgroundColorKey = new FluentDesignResourceKey(nameof(TextBoxHoverBackgroundColorKey));
        public static readonly FluentDesignResourceKey TextBoxFocusedBackgroundColorKey = new FluentDesignResourceKey(nameof(TextBoxFocusedBackgroundColorKey));


        // brush keys
        public static readonly FluentDesignResourceKey TextBrushKey = new FluentDesignResourceKey(nameof(TextBrushKey));

        public static readonly FluentDesignResourceKey ControlBackgroundBrushKey = new FluentDesignResourceKey(nameof(ControlBackgroundBrushKey));
        public static readonly FluentDesignResourceKey ControlHoverBackgroundBrushKey = new FluentDesignResourceKey(nameof(ControlHoverBackgroundBrushKey));
        public static readonly FluentDesignResourceKey ControlPressedBackgroundBrushKey = new FluentDesignResourceKey(nameof(ControlPressedBackgroundBrushKey));
        public static readonly FluentDesignResourceKey ControlBorderBrushKey = new FluentDesignResourceKey(nameof(ControlBorderBrushKey));

        public static readonly FluentDesignResourceKey TextBoxBackgroundBrushKey = new FluentDesignResourceKey(nameof(TextBoxBackgroundBrushKey));
        public static readonly FluentDesignResourceKey TextBoxHoverBackgroundBrushKey = new FluentDesignResourceKey(nameof(TextBoxHoverBackgroundBrushKey));
        public static readonly FluentDesignResourceKey TextBoxFocusedBackgroundBrushKey = new FluentDesignResourceKey(nameof(TextBoxFocusedBackgroundBrushKey));
        public static readonly FluentDesignResourceKey TextBoxBorderBrushKey = new FluentDesignResourceKey(nameof(TextBoxBorderBrushKey));
    }
}
