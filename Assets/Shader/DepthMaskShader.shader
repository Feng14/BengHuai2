Shader "miHoYo/Depth Mask" {
SubShader { 
 Tags { "QUEUE"="Transparent+10" }
 Pass {
  Tags { "QUEUE"="Transparent+10" }
  Cull Off
  ColorMask 0
 }
}
}