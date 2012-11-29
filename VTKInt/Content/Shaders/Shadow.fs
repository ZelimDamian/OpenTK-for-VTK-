#version 120

uniform sampler2D baseTexture;
uniform sampler2D normalTexture;
uniform sampler2D envMapTexture;
uniform sampler2D lightTexture;
uniform float in_far;

varying vec4 g_pos;

void main(void)
{
	gl_FragColor = vec4(vec3(g_pos.z / g_pos.w), 1.0);
	//gl_FragColor = vec4(g_pos.xyz / in_far, 1.0);
}