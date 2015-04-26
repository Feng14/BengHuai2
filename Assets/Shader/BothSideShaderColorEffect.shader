Shader "miHoYo/Both Side Vertex Lit With Color Effect" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _Emission ("Emmisive Color", Color) = (0,0,0,0)
 _MainTex ("Base (RGB) Trans. (Alpha)", 2D) = "white" {}
}
SubShader { 
 LOD 100
 Tags { "QUEUE"="Transparent+100" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent+100" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
  Lighting On
  Material {
   Ambient [_Color]
   Emission [_Emission]
  }
  ZWrite Off
  Cull Off
  Blend SrcAlpha OneMinusSrcAlpha
  SetTexture [_MainTex] { ConstantColor [_Color] combine texture * primary double, texture alpha * constant alpha }
 }
}
}