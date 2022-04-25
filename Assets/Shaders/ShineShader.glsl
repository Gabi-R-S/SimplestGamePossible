#version 130

uniform float time;
uniform sampler2D currentTexture;
const vec4 shineColor=vec4(77.0/255.0,43.0/255.0,50.0/255.0,1.0);
const float timeSpeedUp=2.0;
void main( void ) {	

	vec4 pixel = texture2D(currentTexture, gl_TexCoord[0].xy);

	gl_FragColor = pixel+(shineColor-pixel)*clamp(sin(time*timeSpeedUp)+1,0,1);
	
}