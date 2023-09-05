shader_type canvas_item;
render_mode skip_vertex_transform;

float sdEgg( in vec2 p, in float ra, in float rb )
{
    const float k = sqrt(3.0);
    
    p.x = abs(p.x);
    
    float r = ra - rb;

    return ((p.y<0.0)       
		? (length(vec2(p.x,  p.y    )) - ra)//(r*2.0)
		: ((k*(p.x+r)<p.y) 
			? length(vec2(p.x,  p.y-k*r)) 
			: length(vec2(p.x+r,p.y    )) - 2.0*r) - rb);
}

void vertex() {

    //VERTEX = (MODEL_MATRIX * vec4(VERTEX, 0.0, 1.0)).xy;
}

void fragment()
{
	vec2 iResolution = (1.0 / SCREEN_PIXEL_SIZE);
	
    vec2 p = ((VERTEX.xy*1.0)-(iResolution.xy*.5))/(iResolution.y);//1.0; iResolution.y*.5 500.0
    //vec2 p = VERTEX;
	//vec2 p = UV;//+vec2(.5,.5); 
    //vec2 p = vec2(0.0,0.0);
	
    float ra = 0.15;
    float rb = ra*(0.75+0.25*cos(3.0*TIME));
	
    vec3 col = vec3(0.65,0.85,1.0);
	
    // distance
    float d = sdEgg(p+vec2(0.0,0.0), ra, rb);
	
    if(d<0.0){
    	
        
        COLOR = vec4(col, .5);
    
    }
	/*
    col = (d>0.0) ? vec3(0.9,0.6,0.3) : vec3(0.65,0.85,1.0);
	col *= 1.0 - exp(-6.0*abs(d));
	col *= 0.8 + 0.2*cos(128.0*abs(d));
	col = mix( col, vec3(1.0), 1.0-smoothstep(0.0,0.015,abs(d)) );
	*/
	//COLOR = mix( COLOR, vec4(0.325,0.425,.5, .5), 1.0-smoothstep(0.0,(ra*10.0*0.005),abs(d)) ); //border
	/*
	if(VERTEX.x+VERTEX.x*.5>0.0){
		COLOR = vec4(1.0,0.0,0.0,1.0);
	}*/
	//COLOR = vec4(col, .5);
}
    
