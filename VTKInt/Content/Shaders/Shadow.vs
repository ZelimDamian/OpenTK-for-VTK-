#version 120

uniform mat4 projection_matrix;
uniform mat4 modelview_matrix;
uniform mat4 model_matrix;
uniform mat4 rotation_matrix;
uniform mat4 mesh_matrix;
uniform mat4 light_matrix;
uniform mat4 light_view;
uniform mat4 light_proj;

uniform vec3 in_light;

attribute vec3 in_normal;
attribute vec3 in_position;
attribute vec2 in_texture;
attribute vec3 in_tangent;

varying vec4 g_pos;

void main(void)
{
	g_pos = model_matrix * mesh_matrix * vec4(in_position, 1);
	
	gl_Position = light_view * g_pos;
	g_pos = gl_Position;
	
	gl_Position = light_proj * g_pos;
	
	g_pos = light_proj * g_pos;
}