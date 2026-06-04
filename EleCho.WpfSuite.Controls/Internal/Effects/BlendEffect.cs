using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows;
using System.IO;

namespace EleCho.WpfSuite.Internal.Effects
{
#if DEBUG
    /// <summary>
    /// BlendEffect 类用于应用自定义的混合效果。
    /// </summary>
    public
#else
    internal 
#endif
        class BlendEffect : ShaderEffect
    {
        /// <summary>
        /// 输入纹理的依赖属性。
        /// </summary>
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(BlendEffect), 0);
        /// <summary>
        /// 输入大小的依赖属性。
        /// </summary>
        public static readonly DependencyProperty InputSizeProperty = DependencyProperty.Register("InputSize", typeof(Size), typeof(BlendEffect), new UIPropertyMetadata(new Size(100D, 100D), PixelShaderConstantCallback(0)));
        /// <summary>
        /// 噪声强度的依赖属性。
        /// </summary>
        public static readonly DependencyProperty NoiseStrengthProperty = DependencyProperty.Register("NoiseStrength", typeof(double), typeof(BlendEffect), new UIPropertyMetadata(((double)(0.1D)), PixelShaderConstantCallback(1)));
        /// <summary>
        /// 覆盖颜色的依赖属性。
        /// </summary>
        public static readonly DependencyProperty OverlayColorProperty = DependencyProperty.Register("OverlayColor", typeof(Color), typeof(BlendEffect), new UIPropertyMetadata(Color.FromArgb(255, 0, 0, 0), PixelShaderConstantCallback(2)));
        /// <summary>
        /// 初始化 BlendEffect 类的新实例。
        /// </summary>
        public BlendEffect()
        {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = new Uri("pack://application:,,,/EleCho.WpfSuite.Controls;component/Internal/Effects/BlendEffect.ps", UriKind.Absolute);
            this.PixelShader = pixelShader;

            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(InputSizeProperty);
            this.UpdateShaderValue(NoiseStrengthProperty);
            this.UpdateShaderValue(OverlayColorProperty);
        }
        /// <summary>
        /// 获取或设置输入纹理。
        /// </summary>
        public Brush Input
        {
            get
            {
                return ((Brush)(this.GetValue(InputProperty)));
            }
            set
            {
                this.SetValue(InputProperty, value);
            }
        }
        /// <summary>
        /// 获取或设置输入大小。
        /// </summary>
        public Size InputSize
        {
            get
            {
                return ((Size)(this.GetValue(InputSizeProperty)));
            }
            set
            {
                this.SetValue(InputSizeProperty, value);
            }
        }
        /// <summary>
        /// 获取或设置噪声强度。
        /// </summary>
        public double NoiseStrength
        {
            get
            {
                return ((double)(this.GetValue(NoiseStrengthProperty)));
            }
            set
            {
                this.SetValue(NoiseStrengthProperty, value);
            }
        }
        /// <summary>
        /// 获取或设置覆盖颜色。
        /// </summary>
        public Color OverlayColor
        {
            get
            {
                return ((Color)(this.GetValue(OverlayColorProperty)));
            }
            set
            {
                this.SetValue(OverlayColorProperty, value);
            }
        }


        private const string ShaderSource =
            """
            sampler2D input : register(s0);

            // new HLSL shader
            // modify the comment parameters to reflect your shader parameters

            /// <summary>Explain the purpose of this variable.</summary>
            /// <type>Size</type>
            /// <minValue>0,0/minValue>
            /// <maxValue>1000,1000</maxValue>
            /// <defaultValue>100,100</defaultValue>
            float2 InputSize : register(C0);

            /// <summary>Explain the purpose of this variable.</summary>
            /// <minValue>0/minValue>
            /// <maxValue>1</maxValue>
            /// <defaultValue>0.1</defaultValue>
            float NoiseStrength : register(C1);

            /// <summary>Explain the purpose of this variable.</summary>
            /// <type>Color</type>
            /// <minValue>0,0,0,0/minValue>
            /// <maxValue>1,1,1,1</maxValue>
            /// <defaultValue>0,0,0,0</defaultValue>
            float4 OverlayColor : register(C2);

            float2 hash2D(float2 p) {
                p = float2(dot(p, float2(127.1, 311.7)),
                           dot(p, float2(269.5, 183.3)));
                return -1.0 + 2.0 * frac(sin(p) * 43758.5453123);
            }

            float perlinNoise(float2 p) {
                float2 i = floor(p);
                float2 f = frac(p);

                float2 g00 = hash2D(i);
                float2 g10 = hash2D(i + float2(1.0, 0.0));
                float2 g01 = hash2D(i + float2(0.0, 1.0));
                float2 g11 = hash2D(i + float2(1.0, 1.0));

                float2 u = f * f * (3.0 - 2.0 * f);

                float n00 = dot(g00, f);
                float n10 = dot(g10, f - float2(1.0, 0.0));
                float n01 = dot(g01, f - float2(0.0, 1.0));
                float n11 = dot(g11, f - float2(1.0, 1.0));

                float n0 = lerp(n00, n10, u.x);
                float n1 = lerp(n01, n11, u.x);

                return lerp(n0, n1, u.y);
            }

            float4 main(float2 uv : TEXCOORD) : COLOR 
            { 
            	float diff = perlinNoise(float2(uv.x * InputSize.x, uv.y * InputSize.y)) * NoiseStrength;
            	float4 textureColor = tex2D(input , uv.xy);

              textureColor.x += diff;
              textureColor.y += diff;
              textureColor.z += diff;

              float a1 = 1 - OverlayColor.w;
              float a2 = OverlayColor.w;
              float4 color2 = float4(OverlayColor.xyz, 1);
              float4 finalColor = textureColor * a1 + color2 * a2;

            	return finalColor; 
            }
            """;
    }
}
