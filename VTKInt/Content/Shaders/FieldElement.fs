#version 120

uniform sampler2D baseTexture;
uniform sampler2D normalTexture;

varying vec4 g_pos;
varying vec3 v_eyedirection;
varying vec3 v_normal;
varying vec2 v_texture;
varying vec3 v_tangent;
varying vec3 v_bnormal;
varying vec3 light;
varying float v_depth;

void main(void)
{
	float diffuse = clamp(dot(normalize(v_normal), light), 0.0, 1.0);
	vec3 texture = vec3(1.0) * diffuse;
	
	gl_FragColor = vec4(texture, 1.0);
}