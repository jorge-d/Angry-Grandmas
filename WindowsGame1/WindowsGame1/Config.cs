using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public enum Direction { NONE, LEFT, UP, DOWN, RIGHT};
    public enum EntityType { WALL, PLAYER, BULLET, BONUS, EXPLOSION, BLOOD }
    public enum MapElements { GRASS = 0, WALL, SPAWN, TREE }

    public class Defaults
    {
        static public float player_speed = 2f;
        static public int player_health = 100;
        static public int player_width = 32;
        static public int player_height = 32;

        static public float cloud_shoot_interval = 200f;
        static public float fireball_shoot_interval = 1500f;
        static public float entity_movement_interval = 15f;
        static public float explosion_animation_interval = 20f;
        static public float blood_animation_interval = 40f;
        static public float animation_movement_interval = 100f;
        static public float sheep_generation_interval = 2500f;

        static public int window_size_x = 1024;
        static public int window_size_y = 688;
        static public int stage_square_nb_x = 32;
        static public int stage_square_nb_y = 20;
        static public int stage_square_size = 32;
        static private Random r = new Random();

        static public string getSheepRandomTexture()
        {
            if (r.Next() % 2 == 0)
                return @"Images/white_sheep";
            return @"Images/black_sheep";
        }
        static public string humanTexturePath(int nb)
        {
            if (nb == 1)
                return @"Images/hero2";
            return @"Images/hero";
        }

        static public float time_before_recovering_from_bleeding = 3000f;

        static public string sheep_texture_path = @"Images/white_sheep";
        static public string cloud_texture_path = @"Images/cloud";
        static public string fireball_texture_path = @"Images/fireball";
        static public string explosion_texture_path = @"Images/explosion";
        static public string world_texture_path = @"Images/world_elements";
        static public string blood_texture_path = @"Images/blood";

        static public string explosion_sound = @"Sounds/smooth_explosion";
        static public string player_hit_sound = @"Sounds/monsterkill";
        static public string sheep_death_sound = @"Sounds/cut_sheep";

        static public int timer_seconds_number = 60;
        static public int timer_minutes_number = 0;

        static public float sheep_speed = 1f;
        static public int sheep_health = 10;
        static public int cloud_damages = 2;
        static public int fireball_damages = 20;
        static public float cloud_speed = 2f;
        static public float fireball_speed = 4f;

        static public int SCORE_ON_HIT = 1;
        static public int SCORE_ON_DEATH = 3;

        static public int MAX_SHEEP_NUMBER = 10;

        public static int MOUVEMENT_DIRECTION_DOWN = 0;
        public static int MOUVEMENT_DIRECTION_LEFT = 1;
        public static int MOUVEMENT_DIRECTION_RIGHT = 2;
        public static int MOUVEMENT_DIRECTION_UP= 3;

        public static int MOUVEMENT_PHASE_BEGIN = 0;
        public static int MOUVEMENT_PHASE_MIDDLE = 1;
        public static int MOUVEMENT_PHASE_END = 2;

        public static int tree_numbers = 50;
    }
}
