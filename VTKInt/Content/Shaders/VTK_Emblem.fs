#version 120

uniform sampler2D baseTexture;
uniform sampler2D base2Texture;
uniform sampler2D normalTexture;
uniform sampler2D envMapTexture;

varying vec4 g_pos;
varying vec3 v_eyedirection;
varying vec3 v_normal;
varying vec2 v_texture;
varying vec3 v_tangent;
varying vec3 v_bnormal;
varying vec3 light;
varying float v_depth;

const vec3 Xunitvec = vec3 (1.0, 0.0, 0.0);
const vec3 Yunitvec = vec3 (0.0, 1.0, 0.0);

void main (void)
{
	vec3 reflection = normalize(reflect(-v_eyedirection, v_normal));
	
	float diffuse = clamp(dot(normalize(v_normal), light), 0.0, 1.0);
	float specular = pow(clamp(dot(reflection, light), 0.0, 1.0), 40);
	
	vec2 index;
	index.y = reflection.y;
	
	reflection.y = 0.0;
	index.x = normalize(reflection).x * 0.5; //dot(normalize(reflection), Xunitvec) * 0.5;

	if (reflection.z >= 0.0)
		index = (index + 1.0) * 0.5;
	else
	{
		index.y = (index.y + 1.0) * 0.5;
		index.x = (-index.x) * 0.5 + 1.0;
	}

	vec3 envColor = vec3 (texture2D(envMapTexture, index));
	vec3 texture = texture2D(baseTexture, v_texture).xyz * diffuse + vec3(1.0) * specular;
	
	gl_FragColor = vec4 (mix(envColor, texture, 0.75), 1.0);
	
	//gl_FragColor = texture2D(base2Texture, v_texture);
}