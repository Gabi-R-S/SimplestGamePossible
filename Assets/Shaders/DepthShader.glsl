#version 130

uniform float depth;
uniform sampler2D currentTexture;

void main( void ) {	

	vec4 pixel = texture2D(currentTexture, gl_TexCoord[0].xy);

	gl_FragColor = pixel+vec4(0.5f,0.4f,0.2f,0)*(1-depth);
	
}