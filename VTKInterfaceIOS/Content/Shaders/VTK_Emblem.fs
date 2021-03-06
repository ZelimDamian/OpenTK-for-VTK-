#version 120

uniform sampler2D baseTexture;
uniform sampler2D normalTexture;

varying vec4 g_pos;
varying vec3 v_normal;
varying vec2 v_texture;
varying vec3 v_tangent;
varying vec3 v_bnormal;
varying vec3 light;

void main(void)
{
	//vec4 NTexValue = texture(normalTexture, v_texture) * 2.0 - 1.0;
	//vec3 N = v_normal;
	
	vec4 texture = texture2D(baseTexture, v_texture);
	gl_FragColor = texture;
}