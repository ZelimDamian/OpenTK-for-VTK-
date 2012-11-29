	varying vec4 v_position;
	
	void main()
	{
			gl_TexCoord[0].st = gl_MultiTexCoord0.st;
			gl_Position = ftransform();
			v_position = gl_Position;
	}