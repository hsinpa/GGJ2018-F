using System.Collections;
using System.Collections.Generic;

public class VehecleComponent : I_Component {
    public float speed, spawn_time;

    public VehecleComponent(string p_id, float p_speed, float p_spawn_time) {
        spawn_time = p_spawn_time;
        speed = p_speed;
    }
}
