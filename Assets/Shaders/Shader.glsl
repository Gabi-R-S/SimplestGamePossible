#ifdef GL_ES
precision mediump float;
#endif

uniform float depth;
uniform sampler2D texture;


void main( void ) {	

	vec4 pixel = texture2D(texture, gl_TexCoord[0].xy);

	gl_FragColor = pixel+vec4(0.5f,0.4f,0.2f,0)*depth;
	
}