shader_type canvas_item;

void fragment() {
	if(UV.x>0.6){
		COLOR = vec4(1.0,0.0,0.0,1.0);
		if(UV.y > .6){
			COLOR = vec4(0.0,0.0,1.0,1.0);
		}
	}
}
