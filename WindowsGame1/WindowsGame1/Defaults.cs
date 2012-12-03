﻿using System;
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
        static public float player_speed = 3f;
        static public int player_health = 100;
        static public int player_width = 32;
        static public int player_height = 32;

        static public float player_shoot_interval = 200f;
        static public float entity_movement_interval = 20f;
        static public float explosion_animation_interval = 20f;
        static public float blood_animation_interval = 40f;
        static public float animation_movement_interval = 100f;

        static public int window_size_x = 1024;
        static public int window_size_y = 768;
        static public int stage_square_nb_x = 32;
        static public int stage_square_nb_y = 20;
        static public int stage_square_size = 32;

        static public string getSheepRandomTexture()
        {
            Random r = new Random();
            if (r.Next() % 2 == 0)
                return @"Images/white_sheep";
            return @"Images/black_sheep";
        }
        static public string sheep_texture_path = @"Images/white_sheep";
        static public string human_texture_path = @"Images/hero2";
        static public string cloud_texture_path = @"Images/cloud";
        static public string explosion_texture_path = @"Images/explosion";
        static public string world_texture_path = @"Images/world_elements";
        static public string blood_texture_path = @"Images/blood";

        static public float sheep_speed = 2f;
        static public int sheep_health = 10;
        static public int cloud_damages = 2;
        static public float cloud_speed = 4f;

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
