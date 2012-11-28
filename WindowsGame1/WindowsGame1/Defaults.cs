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
        static public float player_speed_x = 3f;
        static public float player_speed_y = 3f;
        static public int player_health = 100;
        static public int player_width = 32;
        static public int player_height = 32;

        static public float animation_movement_interval = 100f;

        static public int window_size_x = 1024;
        static public int window_size_y = 768;

        static public string sheep_texture_path = @"Images/machoc";
        static public string human_texture_path = @"Images/human";

        static public float sheep_speed_x = 2f;
        static public float sheep_speed_y = 2f;
        static public int sheep_health = 10;

        static public int cloud_damages = 2;
        static public string cloud_texture = @"Images/cloud";
    }
}
