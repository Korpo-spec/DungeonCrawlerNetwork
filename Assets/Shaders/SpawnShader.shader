Shader "Unlit/SpawnShader"
{
    // The properties block of the Unity shader. In this example this block is empty
        // because the output color is predefined in the fragment shader code.
        Properties
        { 
            [MainTexture] _MainTex("Base texture", 2D) = "white" {}
            _varying ("float", Range(0, 1)) = 0
            
        }
    
        // The SubShader block containing the Shader code. 
        SubShader
        {
            // SubShader Tags define when and under which conditions a SubShader block or
            // a pass is executed.
            Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
    
            Pass
            {
                // The HLSL code block. Unity SRP uses the HLSL language.
                HLSLPROGRAM
                // This line defines the name of the vertex shader. 
                #pragma vertex vert

                #pragma  geometry geom
                // This line defines the name of the fragment shader. 
                #pragma fragment frag

                

                //#pragma geometry geom
    
                // The Core.hlsl file contains definitions of frequently used HLSL
                // macros and functions, and also contains #include references to other
                // HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            
    
                // The structure definition defines which variables it contains.
                // This example uses the Attributes structure as an input structure in
                // the vertex shader.
                
                struct Attributes
                {
                    float4 positionOS   : POSITION;
                    // The uv variable contains the UV coordinate on the texture for the
                    // given vertex.
                    float2 uv           : TEXCOORD0;
                    float4 normal         : NORMAL0; 
                };

                
    
                struct Varyings
                {
                    // The positions in this struct must have the SV_POSITION semantic.
                    float4 positionHCS  : SV_POSITION;
                    float4 normalWS         : POSITION4; 
                    float clip : FOG;
                    float clip2 : PSIZE;
                    
                    float2 uv           : TEXCOORD0;
                };

                struct GeomData
                {
                    float4 positionCS         : SV_POSITION;
                    float3 positionWS         : TEXCOORD0;  
                    float4 normalWS         : POSITION4; 
                    float4 normal        : NORMAL0; 
                    float clip : PSIZE;
                };

                Texture2D _MainTex;

                SAMPLER(sampler_MainTex);
                CBUFFER_START(UnityPerMaterial)
                // The following line declares the _BaseMap_ST variable, so that you
                // can use the _BaseMap variable in the fragment shader. The _ST
                // suffix is necessary for the tiling and offset function to work.
                float4 _MainTex_ST;
                CBUFFER_END
            
                float _varying;
                // The vertex shader definition with properties defined in the Varyings 
                // structure. The type of the vert function must match the type (struct)
                // that it returns.
                Varyings vert(Attributes IN)
                {
                    // Declaring the output object (OUT) with the Varyings struct.
                    Varyings OUT;

                    
                    
                    
                    // The TransformObjectToHClip function transforms vertex positions
                    // from object space to homogenous space
                    OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                    OUT.positionHCS = TransformObjectToWorld(IN.positionOS.xyz).xyzz;
                    
                    OUT.normalWS = IN.normal;
                    
                    float4 norm = normalize(IN.positionOS);
                    float ypos = norm.y;
                    ypos = 1-ypos;
                    max(ypos, 1-_varying);
                    ypos = ypos-(1-_varying)+0.2f;
                    //clip(ypos);
                    OUT.clip = ypos;
                    OUT.clip2 = 0;
                    OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                    OUT.positionHCS.w = ypos;
                    // Returning the output.
                    return OUT;
                }
                [maxvertexcount(3)]
                void geom(triangle GeomData input[3], inout TriangleStream<GeomData> triStr)
                {
                   
                    float3 edge1 = input[1].positionCS -input[0].positionCS;
                    float3 edge2 = input[2].positionCS -input[0].positionCS;
                    float3 normal = cross(edge1, edge2);
                    normal = normalize(normal);

                    for (int i = 0; i < 3; ++i)
                    {
                        GeomData vert0 = input[i];
                        
                        //vert0.normalWS = vert0.normal;
                        //vert0.normalWS = (normalize(vert0.normalWS));
                        vert0.positionCS = TransformWorldToHClip(vert0.positionCS + (normal*(max(1-(vert0.positionCS.w +0.7f),0)) * 0.1f) );
                        
                        triStr.Append(vert0);
                    }
                    
                    
                    triStr.RestartStrip();
                }

                
                // The fragment shader definition.            
                half4 frag(Varyings var, float2 uv : TEXCOORD0) : SV_Target
                {
                    // Defining the color variable and returning it.
                    float4 norm = normalize(var.positionHCS);
                    float ypos = norm.y;
                    
                    max(ypos, 0.4f);
                    ypos = ypos-0.4f;
                    clip(var.clip);
                    half4 customColor;
                    customColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                    customColor *= half4(var.clip, var.clip, var.clip, 1);
                    return customColor;
                }
                ENDHLSL
            }
        }
}
