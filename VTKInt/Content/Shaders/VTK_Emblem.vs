#version 120

uniform mat4 projection_matrix;
uniform mat4 modelview_matrix;
uniform mat4 model_matrix;
uniform mat4 rotation_matrix;
uniform mat4 mesh_matrix;

uniform vec3 in_light;
uniform vec3 in_eyepos;

attribute vec3 in_normal;
attribute vec3 in_position;
attribute vec2 in_texture;
attribute vec3 in_tangent;

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
	g_pos = model_matrix * mesh_matrix * vec4(in_position, 1);
	
	gl_Position = projection_matrix * modelview_matrix * g_pos;
	
	v_texture = in_texture;
	
	v_eyedirection = normalize(g_pos.xyz - in_eyepos);
	
	v_normal = normalize((vec4(in_normal, 0)).xyz);
	v_tangent = normalize((vec4(in_tangent, 0)).xyz);
	v_bnormal = normalize(cross(v_normal, v_tangent));
}