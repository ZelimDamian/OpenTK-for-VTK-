#version 120

uniform sampler2D baseTexture;
uniform sampler2D normalTexture;
uniform sampler2D envMapTexture;
uniform sampler2D lightTexture;
uniform float in_far;

varying vec4 g_pos;
varying vec3 v_eyedirection;
varying vec3 v_normal;
varying vec2 v_texture;
varying vec3 v_tangent;
varying vec3 v_bnormal;
varying vec3 light;
varying vec3 toLight;
varying float v_depth;
varying vec4 ShadowCoord;

const vec3 Xunitvec = vec3 (1.0, 0.0, 0.0);
const vec3 Yunitvec = vec3 (0.0, 1.0, 0.0);

void main (void)
{	
	float eyeNormalDot = dot(normalize(v_normal), normalize(v_eyedirection));
	if(eyeNormalDot < 0.0)
		discard;
	
////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////// REFLECTION
////////////////////////////////////////////////////////////////////////////////////////////////////////////

	vec3 reflection = normalize(reflect(v_eyedirection, v_normal));
	
	vec2 index;
	index.y = reflection.y;
	
	reflection.y = 0.0;
	index.x = normalize(reflection).x * 0.5;
	
	if (reflection.z >= 0.0)
		index = (index + 1.0) * 0.5;
	else
	{
		index.y = (index.y + 1.0) * 0.5;
		index.x = (-index.x) * 0.5 + 1.0;
	}

	vec3 envColor = vec3 (texture2D(envMapTexture, index));

////////////////////////////////////////////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////// COLOR
////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	float diffuse = clamp(dot(normalize(v_normal), light), 0.0, 1.0);
	float specular = pow(clamp(dot(-reflection, light), 0.0, 1.0), 40);
	
	vec3 texture = vec3(0.9) * diffuse + vec3(1.0) * specular;


////////////////////////////////////////////////////////////////////////////////////////////////////////////



////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////// SHADOW
////////////////////////////////////////////////////////////////////////////////////////////////////

	vec4 shadowCoordinateWdivide = ShadowCoord / ShadowCoord.w ;
	
	// Used to lower moiré pattern and self-shadowing
	shadowCoordinateWdivide.z += 0.0000005;
	
	float shadowMapValue = length(texture2D(lightTexture, shadowCoordinateWdivide.st).y); // full value from 0 to inf

 	float shadow = 0.0;
 	if (ShadowCoord.w > 0.0)
 		shadow = shadowMapValue < shadowCoordinateWdivide.z ? 0.6 : 1.0 ;

////////////////////////////////////////////////////////////////////////////////////////////////////

	gl_FragColor = vec4 (mix(envColor, texture, 0.85) * shadow, 1.0);
}