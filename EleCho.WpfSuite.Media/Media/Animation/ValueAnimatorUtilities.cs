using System;

namespace EleCho.WpfSuite.Media.Animation
{
    public static class ValueAnimatorUtilities
    {
        public static IValueAnimator CreateFromType(Type type)
        {
            var instance = typeof(ValueAnimator<>)
                .MakeGenericType(type)
                .GetConstructor(Type.EmptyTypes)?
                .Invoke(null) as IValueAnimator;

            return instance!;
        }
    }


}
