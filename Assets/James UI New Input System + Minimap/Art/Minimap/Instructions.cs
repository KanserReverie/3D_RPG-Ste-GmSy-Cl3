using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("INSTRUCTIONS:");

            print("Package manager - Install 2d sprite\n" +
                "Make Material\n" +
                "Make Material Unlit/Transperent\n" +
                "Drag in material\n" +
                "Make 3d quad 2 scale 10 y above and in player folder");

            print("Make sprite in corner\n" +
                "Drag boarder image\n" +
                "Make minimask with Knob as Source Image, Add Mask and turn off Mask Graphic\n" +
                "Add raw image\n" +
                "Add 2 minimaps as childs in side");

            print("Make Camera make 20 above make orthotopic and cull out the player layer");

            print("Make a Render Texture\n" +
                "To note Filtermode 'point' is needed for pixel games\n" +
                "Drag texture to cammera 'Target texture'\n" +
                "Drag Texture into Minimap Display");

            print("GLHF ^_^ <3!!");

            print("PART 2");

            print("Package manager - Install Post Processing\n" +
                "Make post processing profile\n" +
                "Make Empty game object\n" +
                "Add Post-processing volume\n" +
                "Make global and add made profile\n" +
                "Add post processing layer\n" +
                "Add profile made\n" +
                "Add Overrides");

            print("Chromatic Aberration - Intensity - makes it slightly grainy\n" +
                "Lens distortion - Intensity - 30 - makes it slightly fish eye\n" +
                "Colour Grading - Channel Mixer - Blue 0 - Green 100 - Red - 0 (All checked in each tab) to make more green");
        }
    }
}
