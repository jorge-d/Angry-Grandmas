using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public enum Direction { NONE, LEFT, UP, DOWN, RIGHT};
    public enum EntityType { WALL, PLAYER, BULLET, BONUS }

    public class Defaults
    {
        static public float entity_movement_interval = 10f;

        static public float player_speed = 2f;
        static public int player_health = 100;
        static public int player_width = 32;
        static public int player_height = 32;
        static public float player_shoot_interval = 200f;

        static public float animation_movement_interval = 100f;

        static public int window_size_x = 1024;
        static public int window_size_y = 768;

        static public string sheep_texture_path = @"Images/white_sheep";
        static public string human_texture_path = @"Images/hero";

        static public float sheep_speed = 0.5f;
        static public int sheep_health = 10;

        static public int cloud_damages = 2;
        static public float cloud_speed = 3f;
        static public string cloud_texture = @"Images/cloud";

        public static int MOUVEMENT_DIRECTION_DOWN = 0;
        public static int MOUVEMENT_DIRECTION_LEFT = 1;
        public static int MOUVEMENT_DIRECTION_RIGHT = 2;
        public static int MOUVEMENT_DIRECTION_UP= 3;

        public static int MOUVEMENT_PHASE_BEGIN = 0;
        public static int MOUVEMENT_PHASE_MIDDLE = 1;
        public static int MOUVEMENT_PHASE_END = 2;
    }
}
