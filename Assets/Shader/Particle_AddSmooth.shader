Shader "Particles/Additive (Soft)" {
Properties {
 _MainTex ("Particle Texture", 2D) = "white" {}
 _InvFade ("Soft Particles Factor", Range(0.01,3)) = 1
}
SubShader { 
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
  ZWrite Off
  Cull Off
  Fog {
   Color (0,0,0,0)
  }
  Blend One OneMinusSrcColor
  ColorMask RGB
Program "vp" {
SubProgram "gles " {
Keywords { "SOFTPARTICLES_OFF" }
"!!GLES


#ifdef VERTEX

varying highp vec2 xlv_TEXCOORD0;
varying lowp vec4 xlv_COLOR;
uniform highp vec4 _MainTex_ST;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesColor;
attribute vec4 _glesVertex;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_COLOR = _glesColor;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD0;
varying lowp vec4 xlv_COLOR;
uniform sampler2D _MainTex;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 prev_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = (xlv_COLOR * texture2D (_MainTex, xlv_TEXCOORD0));
  prev_2 = tmpvar_3;
  prev_2.xyz = (prev_2.xyz * prev_2.w);
  tmpvar_1 = prev_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "SOFTPARTICLES_ON" }
"!!GLES


#ifdef VERTEX

varying highp vec4 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec4 xlv_COLOR;
uniform highp vec4 _MainTex_ST;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _ProjectionParams;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesColor;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_2 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 o_3;
  highp vec4 tmpvar_4;
  tmpvar_4 = (tmpvar_2 * 0.5);
  highp vec2 tmpvar_5;
  tmpvar_5.x = tmpvar_4.x;
  tmpvar_5.y = (tmpvar_4.y * _ProjectionParams.x);
  o_3.xy = (tmpvar_5 + tmpvar_4.w);
  o_3.zw = tmpvar_2.zw;
  tmpvar_1.xyw = o_3.xyw;
  tmpvar_1.z = -((glstate_matrix_modelview0 * _glesVertex).z);
  gl_Position = tmpvar_2;
  xlv_COLOR = _glesColor;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec4 xlv_COLOR;
uniform highp float _InvFade;
uniform sampler2D _CameraDepthTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _ZBufferParams;
void main ()
{
  lowp vec4 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.xyz = xlv_COLOR.xyz;
  mediump vec4 prev_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2DProj (_CameraDepthTexture, xlv_TEXCOORD1);
  highp float z_5;
  z_5 = tmpvar_4.x;
  highp float tmpvar_6;
  tmpvar_6 = (xlv_COLOR.w * clamp ((_InvFade * ((1.0/(((_ZBufferParams.z * z_5) + _ZBufferParams.w))) - xlv_TEXCOORD1.z)), 0.0, 1.0));
  tmpvar_2.w = tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = (tmpvar_2 * texture2D (_MainTex, xlv_TEXCOORD0));
  prev_3 = tmpvar_7;
  prev_3.xyz = (prev_3.xyz * prev_3.w);
  tmpvar_1 = prev_3;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "SOFTPARTICLES_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "SOFTPARTICLES_ON" }
"!!GLES"
}
}
 }
}
}