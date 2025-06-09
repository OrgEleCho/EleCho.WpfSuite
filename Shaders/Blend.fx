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
  
  float a1 = 1 - OverlayColor.w;
  float a2 = OverlayColor.w;
  float4 color2 = float4(OverlayColor.xyz, 1);
  float4 finalColor = textureColor * a1 + color2 * a2;

  finalColor.x += diff;
  finalColor.y += diff;
  finalColor.z += diff;
  
	return finalColor; 
}