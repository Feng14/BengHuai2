Shader "miHoYo/Both Side Vertex Lit" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _Emission ("Emmisive Color", Color) = (0,0,0,0)
 _MainTex ("Base (RGB) Trans. (Alpha)", 2D) = "white" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent+100" }
 Pass {
  Tags { "QUEUE"="Transparent+100" }
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