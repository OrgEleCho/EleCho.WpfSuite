using System;
using System.Windows;
using System.Windows.Markup;

namespace EleCho.WpfSuite.FluentDesign
{
    public class FluentDesignResourceExtension : MarkupExtension
    {
        private readonly DynamicResourceExtension _dynamicResourceExtension;

        public FluentDesignResourceExtension() : this(default(FluentDesignResource))
        {

        }

        public FluentDesignResourceExtension(FluentDesignResource resource) 
        {
            Resource = resource;

            _dynamicResourceExtension = new();
        }

        public FluentDesignResource Resource { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            _dynamicResourceExtension.ResourceKey = Resource;

            return _dynamicResourceExtension.ProvideValue(serviceProvider);
        }
    }
}
